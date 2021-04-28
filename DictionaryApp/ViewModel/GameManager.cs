using DictionaryApp.Model;
using DictionaryApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp.ViewModel
{
    class GameManager
    {
        private static Game game;
        private static Queue<Word> randomWords;
        private static GameWindow gameWindow;
        private static uint score;

        public static void Initialize(GameWindow window)
        {
            score = 0;

            gameWindow = window;

            PickRandomWords();
        }

        public static void OnNewGame()
        {
            if (randomWords.Count > 0)
            {
                game = new Game(randomWords.Dequeue());

                gameWindow.OnNewGame(game.Description, game.ImagePath);
            }
            else
            {
                gameWindow.OnGameOver();
            }
        }

        public static string CheckGuess(string guess)
        {
            Tuple<bool, string> answer = game.HasGuessed(guess);

            var isCorrect = answer.Item1;
            var correctWord = answer.Item2;

            if (isCorrect)
            {
                ++score;
                gameWindow.UpdateScore(score);

                return "";
            }
            else
            {
                return correctWord;
                //TODO show correct word
            }

        }

        public static void Reset()
        {
            game = null;
            randomWords = null;
            gameWindow = null;
        }

        public static void PickRandomWords()
        {
            List<int> pickedPositions = new List<int>();

            randomWords = new Queue<Word>();

            Random random = new Random();

            while (randomWords.Count < 5)
            {
                int index = random.Next(0, Dictionary.Words.Count);

                if (!pickedPositions.Contains(index))
                {
                    randomWords.Enqueue(Dictionary.Words[index]);
                    pickedPositions.Add(index);
                }
            }
        }

    }
}
