using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp.Model
{
    class Game
    {
        private Word word;

        public string Description { get => word.description; }

        public string ImagePath { get => word.imagePath; }


        public Game(Word word)
        {
            this.word = word;
        }

        public Tuple<bool, string> HasGuessed(string guess)
        {
           return Tuple.Create(guess.Equals(word.name), word.name) ;
        }
    }
}
