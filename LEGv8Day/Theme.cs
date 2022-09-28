using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    public class Theme
    {
        public static Theme Default => new Theme("Default");

        public string Name { get; set; }

        public Color PrimaryColor;
        public Color SecondaryColor;
        public Color TertiaryColor;

        public Color TextColor;
        public Color KeywordColor;
        public Color RegisterColor;
        public Color NumberColor;
        public Color CommentColor;
        public Color LabelColor;

        public Theme() : this("Unnamed Theme") { }

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
