using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    /// <summary>
    /// Holds data for a color scheme, also known as a Theme.
    /// </summary>
    public class Theme
    {
        /// <summary>
        /// The default Theme that is used if no other Theme is specified.
        /// </summary>
        public static Theme Default => new Theme("Default");

        /// <summary>
        /// The name of this Theme.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The first and main color used within the application. Form backgrounds will be this color.
        /// </summary>
        public Color PrimaryColor;

        /// <summary>
        /// The second color used within the application. Elements within forms will use this color for a background.
        /// </summary>
        public Color SecondaryColor;

        /// <summary>
        /// The third color used within the application. Elements within forms may use this color as an extra accent.
        /// </summary>
        public Color TertiaryColor;


        /// <summary>
        /// The color of the default text within the text editor.
        /// </summary>
        public Color TextColor;

        /// <summary>
        /// The color of the keywords within the text editor.
        /// </summary>
        public Color KeywordColor;

        /// <summary>
        /// The color of the registers within the text editor.
        /// </summary>
        public Color RegisterColor;

        /// <summary>
        /// The color of the numbers within the text editor.
        /// </summary>
        public Color NumberColor;

        /// <summary>
        /// The color of the comments within the text editor.
        /// </summary>
        public Color CommentColor;

        /// <summary>
        /// The color of the labels within the text editor.
        /// </summary>
        public Color LabelColor;

        /// <summary>
        /// Creates an unnamed Theme based on the default Theme.
        /// </summary>
        public Theme() : this("Unnamed Theme") { }

        /// <summary>
        /// Creates a theme using the given name, based on the default Theme.
        /// </summary>
        /// <param name="name"></param>
        public Theme(string name)
        {
            Name = name;
            PrimaryColor = Color.White;
            SecondaryColor = Color.White;
            TertiaryColor = Color.White;

            TextColor = Color.FromArgb(0, 0, 0);//default black
            KeywordColor = Color.FromArgb(10, 50, 210);//keyword blue
            RegisterColor = Color.FromArgb(205, 50, 22);//register red
            NumberColor = Color.FromArgb(194, 185, 56);//number yellow
            CommentColor = Color.FromArgb(100, 185, 70);//comment green
            LabelColor = Color.FromArgb(192, 73, 222);//label purple
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
