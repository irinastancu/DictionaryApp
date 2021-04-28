using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp
{
    [Serializable]
    class Word
    {
        internal string name { get; set; }
        internal string category { get; set; }
        internal string description { get; set; }
        internal string imagePath { get; set; }

        public Word()
        {
            //EMPTY
        }
        public Word(string name, string description, string category, string img)
        {
            this.name = name;
            this.description = description;
            this.category = category;

            if (!img.Equals(string.Empty))
            {
                imagePath = img;
            }
            else imagePath = null;
          
        }
    }
}
