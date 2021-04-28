using DictionaryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DictionaryApp.View
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
          
            GameManager.Initialize(this);
            GameManager.OnNewGame();
        }

        public void OnNewGame(string content, string path)
        {
            if (path != null)
            {
                imgPhoto.Source = new BitmapImage(new Uri(path));
            }
            else
            {
                lblContent.Content = content;
            }
            txtGuess.Text = string.Empty;
        }

        public void UpdateScore(uint score)
        {
            lblScore.Content = $"Score: {score}/5";
        }

        public void OnGameOver()
        {
            lblContent.Content = string.Empty;
            txtGuess.Text = string.Empty;

            btnGuess.IsEnabled = false;

            imgPhoto.Visibility = Visibility.Collapsed;
            lblInfo.Content = "GAME OVER";

            

        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminWindow a = new AdminWindow();
            a.ShowDialog();
            this.Close();
        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            string show = GameManager.CheckGuess(txtGuess.Text);

            if (!show.Equals(string.Empty))
            {
                lblInfo.Content = $"Correct answer: {show}";
                if (imgPhoto.Visibility == Visibility.Visible)
                {
                    imgPhoto.Visibility = Visibility.Collapsed;
                }
            }

            GameManager.OnNewGame();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            GameManager.Reset();
        }

        private void btnBack_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow m = new MainWindow();
            m.ShowDialog();
            this.Close();
        }
    }
}
