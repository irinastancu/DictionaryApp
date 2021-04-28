using DictionaryApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp
{
    class Dictionary
    {
        public static List<string> Categories { get; set; }
        public static List<Word> Words { get; set; }

        public static readonly string path = "words.dat";

        public Dictionary()
        {
            LoadWord();
        }

        public static Word GetWord(string name)
        {
            foreach (Word word in Words)
            {
                if (word.name.Equals(name))
                {
                    return word;
                }
            }

            return null;
        }

        public static string AddWord(string name, string description, string category, string imgPath)
        {
            string message = "";

            name = name.ToLower();

            if (name.Equals(string.Empty))
            {

                message += "Please insert a name for your word\n";
            }

            if (description.Equals(string.Empty))
            {
                message += "Please insert a description for your word\n";
            }

            if (category.Equals(string.Empty))
            {
                message += "Please select a category for your word\n";
            }

            if(imgPath==null)
            {
                imgPath = "";
            }

            Word word = new Word(name, description, category, imgPath);

            //add the word to the list of words
            if (message.Equals(string.Empty) && !ContainsWord(name))
            {
                Words.Add(word);
                AddCategory(word.category);
                SaveWord();

                message += "Your word has been added\n";
            }
            else
            {
                message += "Word already in the dictionary";
            }

            return message;
        }


        private static bool ContainsWord(string wordName)
        {
            var query = (from word in Words
                         where word.name == wordName
                         select word).ToList();

            return query.Count > 0; //returns bool, true if the number of appeareances of the word in the dictionary else, false
        }


        public static bool UpdateWord(string initialName, string name, string description, string category, string imgPath)
        {
            name = name.ToLower();

            foreach (var word in Words)
            {
                if (word.name.Equals(name))
                {
                    word.name = name;
                    word.description = description;
                    word.category = category;

                    SaveWord();

                    return true;
                }
            }

            return false;

        }

        public static bool DeleteWord(string name)
        {
            name = name.ToLower();

            foreach (Word word in Words)
            {
                if (word.name.Equals(name))
                {
                    Words.Remove(word);
                    SaveWord();

                    return true;
                }
            }

            return false;
        }
        public static void AddCategory(string category)
        {
            if (!Categories.Contains(category))
            {
                Categories.Add(category);
                SaveWord();
            }
        }

        public static List<string> ReadCategories()
        {
            List<string> cat = new List<string>();
            string[] lines = System.IO.File.ReadAllLines("Resources/Categories.txt");

            foreach (string element in lines)
            {
                cat.Add(element);
            }

            return cat;

        }

        public static List<string> GetAllWordNames()
        {
            List<string> tmp = new List<string>();

            foreach (Word w in Words)
            {
                tmp.Add(w.name);
            }

            return tmp;
        }

        public static void SaveWord()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Create);

            DictionaryData dictionaryData = new DictionaryData(Words, Categories);

            binaryFormatter.Serialize(stream, dictionaryData);

            stream.Close();
        }

        public static void LoadWord()
        {
            DictionaryData dictionaryData = null;

            if (File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                dictionaryData = binaryFormatter.Deserialize(stream) as DictionaryData;

                Words = dictionaryData.words;
                Categories = dictionaryData.categories;

                stream.Close();
            }
            else
            {
                Words = new List<Word>();
                Categories = new List<string>();
            }
        }


        public static List<string> GetPredictiveList(string w)
        {
            return (from word in Words
                    where word.name.StartsWith(w)
                    select word.name).ToList();

        }
        public static List<string> GetPredictiveList(string w, string c)
        {
           
            return (from word in Words
                    where word.name.StartsWith(w) && word.category.Equals(c)
                    select word.name).ToList();
        }

    }
}
