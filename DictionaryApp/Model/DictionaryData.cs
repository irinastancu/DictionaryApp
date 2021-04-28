using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp.Model
{
    [Serializable]
    class DictionaryData
    {
        public List<Word> words;
        public List<string> categories;

        public DictionaryData(List<Word> words, List<string> categories)
        {
            this.words = words;
            this.categories = categories;
        }
    }
}
