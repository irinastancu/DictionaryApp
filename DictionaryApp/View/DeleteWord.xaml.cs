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

namespace DictionaryApp
{
    /// <summary>
    /// Interaction logic for DeleteWord.xaml
    /// </summary>
    public partial class DeleteWord : Window
    {
        public string name;
        public string description;
        public string category;
        public DeleteWord()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           name = deleteWordTxt.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminWindow a = new AdminWindow();
            a.ShowDialog();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Dictionary.DeleteWord(name))
            {
                MessageBox.Show("Your word has been deleted from the dictionary", "Delete Word");
            }
            else
            {
                MessageBox.Show("Your word isnt't in the dictionary", "Delete Word");
            }
        }
    }
}
