using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    internal static class RtfLEGv8Formatter
    {
        [Flags]
        enum Modifiers
        {
            None = 0,
            Bold = 1,
            Italic = 1 << 1,
            Underline = 1 << 2,
            //Strikeout = 1 << 3,
        }

        class StyleGroup
        {
            public string[] Names;

            public Style Style;

            public StyleGroup(string[] names, Style style)
            {
                Names = names;
                Style = style;
            }
        }

        class Tag
        {
            private string _left;
            private string _right;

            public string Open => _left;
            public string Close => _right;

            public string Text => _left;
            public bool IsPair => !string.IsNullOrEmpty(_right);

            public Tag(string left, string right)
            {
                _left = left;
                _right = right;
            }

            public Tag(string tag)
            {
                _left = tag;
                _right = string.Empty;
            }

            public override string ToString()
            {
                return _left;
            }
        }

        class Style
        {
            public int Color;

            public Modifiers Modifiers;

            public Style(int colorIndex, Modifiers mods)
            {
                Color = colorIndex;
                Modifiers = mods;
            }

            public void Stylize(StringBuilder sb, ref Style currentStyle, string text)
            {
                //check for changes in style
                bool changedStyle = false;

                if (currentStyle.Color != Color)
                {
                    sb.Append($@"\cf{Color + 1}");
                    changedStyle = true;
                }

                Modifiers diff = currentStyle.Modifiers ^ Modifiers;

                if ((diff & Modifiers.Bold) != 0)
                {
                    sb.Append((Modifiers & Modifiers.Bold) != 0 ? BOLD.Open : BOLD.Close);
                    changedStyle = true;
                }
                if ((diff & Modifiers.Italic) != 0)
                {
                    sb.Append((Modifiers & Modifiers.Italic) != 0 ? ITALICIZE.Open : ITALICIZE.Close);
                    changedStyle = true;
                }
                if ((diff & Modifiers.Underline) != 0)
                {
                    sb.Append((Modifiers & Modifiers.Underline) != 0 ? UNDERLINE.Open : UNDERLINE.Close);
                    changedStyle = true;
                }

                //if the style was changed, add a space after, so the tag text does not get connected to the actual text
                if(changedStyle)
                {
                    sb.Append(' ');
                }

                //now add text
                sb.Append(text);

                //lastly, update new style
                currentStyle = this;
            }
        }

        #region Constants

        private static readonly Tag NEW_LINE = new Tag(@"\line");
        private static readonly Tag BOLD = new Tag(@"\b", @"\b0");
        private static readonly Tag ITALICIZE = new Tag(@"\i", @"\i0");
        private static readonly Tag UNDERLINE = new Tag(@"\ul", @"\ulnone");

        private static readonly Style DEFAULT_STYLE = new Style(0, Modifiers.None);
        private static readonly Style KEYWORD_STYLE = new Style(1, Modifiers.Bold);
        private static readonly Style REGISTER_STYLE = new Style(2, Modifiers.None);
        private static readonly Style NUMBER_STYLE = new Style(3, Modifiers.None);
        private static readonly Style COMMENT_STYLE = new Style(4, Modifiers.Italic);
        private static readonly Style LABEL_STYLE = new Style(5, Modifiers.Underline | Modifiers.Bold);

        private static readonly HashSet<char> STICKY_PUNCT_CHARS = new HashSet<char>(new char[]
        {
            '_', ':', '#', '/', '.', '-'
        });

        #endregion

        private static Theme _theme = Theme.Default;

        private readonly static Color[] _colors = new Color[]
        {
            _theme.TextColor,
            _theme.KeywordColor,
            _theme.RegisterColor,
            _theme.NumberColor,
            _theme.CommentColor,
            _theme.LabelColor
        };

        private static HashSet<string> _keywords = new HashSet<string>();

        private static string FormatColor(Color color)
        {
            return $"\\red{color.R}\\green{color.G}\\blue{color.B}";
        }

        public static void SetKeywords(string[] words)
        {
            _keywords = new HashSet<string>(words.Select(w => w.ToUpper()));
        }

        public static void SetTheme(Theme theme)
        {
            _colors[0] = theme.TextColor;
            _colors[1] = theme.KeywordColor;
            _colors[2] = theme.RegisterColor;
            _colors[3] = theme.NumberColor;
            _colors[4] = theme.CommentColor;
            _colors[5] = theme.LabelColor;
        }

        public static string FormatString(string text)
        {
            //go through the string
            //find words

            char c;
            char d;
            string word;

            int j;
            char temp;

            Style currentStyle = DEFAULT_STYLE;
            StringBuilder sb = new StringBuilder();

            //List<string> words = new List<string>();

            for (int i = 0; i < text.Length; i++)
            {
                c = text[i];

                //check for escape conditions
                if (c == '{' || c == '}')
                {
                    DEFAULT_STYLE.Stylize(sb, ref currentStyle, $"\\{c}");

                    continue;
                }

                //check for a char that will let us "skip"
                if (char.IsWhiteSpace(c) || IsIrrelevantPunctuation(c))
                {
                    //print it and continue
                    if (c == '\n')
                    {
                        DEFAULT_STYLE.Stylize(sb, ref currentStyle, $"{NEW_LINE} ");

                        //if at the end, add another one for some reason
                        if(i == text.Length - 1)
                        {
                            sb.Append($"{NEW_LINE} ");
                        }
                    } else
                    {
                        DEFAULT_STYLE.Stylize(sb, ref currentStyle, c.ToString());
                    }                    

                    continue;
                }

                //find the next "stopping point"
                for (j = i + 1; j < text.Length; j++)
                {
                    temp = text[j];

                    if (char.IsWhiteSpace(temp) || IsIrrelevantPunctuation(temp))
                    {
                        //found a breaking point
                        break;
                    }
                }

                //get that "word"
                word = text.Substring(i, j - i);

                d = word[word.Length - 1];

                //check word
                if ((c == '#' || c == '-' || char.IsNumber(c)) && char.IsNumber(d))
                {
                    NUMBER_STYLE.Stylize(sb, ref currentStyle, word);
                }
                else if (char.IsLetter(c) && d == ':')
                {
                    //stylize only the text, not the colon
                    LABEL_STYLE.Stylize(sb, ref currentStyle, word.Substring(0, word.Length - 1));
                    DEFAULT_STYLE.Stylize(sb, ref currentStyle, ":");
                }
                else if (c == '/' && i + 1 < text.Length && text[i + 1] == '/')
                {
                    //comment until the end of the line
                    for (; j < text.Length; j++)
                    {
                        temp = text[j];

                        if (temp == '\n' || temp == '\r')
                        {
                            break;
                        }
                    }

                    //clamp
                    j = Math.Min(j, text.Length);

                    //get new "word"
                    word = text.Substring(i, j - i);

                    //stylize with comment
                    COMMENT_STYLE.Stylize(sb, ref currentStyle, word);
                }
                else if (_keywords.Contains(word.ToUpper()))
                {
                    KEYWORD_STYLE.Stylize(sb, ref currentStyle, word);
                }
                else if (IsRegister(word))
                {
                    REGISTER_STYLE.Stylize(sb, ref currentStyle, word);
                } else
                {
                    //no style ig
                    DEFAULT_STYLE.Stylize(sb, ref currentStyle, word);
                }

                //words.Add(word);

                //advance i
                i = j - 1;
            }

            //MessageBox.Show($"\"{string.Join("\"--\"", words)}\"");

            //now build the string and return it
            return BuildString(sb.ToString());
        }

        private static bool IsRegister(string word)
        {
            switch (word.ToUpper())
            {
                case "IP0": return true;
                case "IP1": return true;
                case "SP": return true;
                case "FP": return true;
                case "LR": return true;
                case "XZR": return true;
            }

            //not a shortcut, must be a number
            return word[0] == 'X' && int.TryParse(word.Substring(1), out int x) && x < Emulation.REGISTER_COUNT;
        }

        private static bool IsIrrelevantPunctuation(char c)
        {
            return char.IsPunctuation(c) && !STICKY_PUNCT_CHARS.Contains(c);
        }

        private static string BuildString(string formattedStr)
        {
            //open, add header info, then text, then close

            //start with {
            StringBuilder sb = new StringBuilder("{");

            //rtf stuff
            sb.Append(@"\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033\deflangfe1033");

            //font
            sb.AppendLine(@"{\fonttbl{\f0\fnil\fcharset0 Consolas;}}");

            //colors
            sb.AppendLine($@"{{\colortbl ;{string.Join(';', _colors.Select(c => FormatColor(c)))};}}");

            //generator?
            sb.Append(@"{\*\generator Riched20 10.0.22621}");

            //formatting
            sb.Append(@"{\*\mmathPr\mdispDef1\mwrapIndent1440 }");

            //view
            sb.AppendLine(@"\viewkind4\uc1 ");

            //header stuff
            sb.Append(@"\pard\nowidctlpar\sa200\sl240\slmult1\f0\fs22\lang9 ");

            //formatted text
            sb.AppendLine(formattedStr);

            //close with }
            sb.AppendLine("}");

            //done
            return sb.ToString();
        }
    }
}
