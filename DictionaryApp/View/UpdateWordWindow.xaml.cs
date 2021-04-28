using DictionaryApp;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DictionaryApp
{
    /// <summary>
    /// Interaction logic for UpdateWordWindow.xaml
    /// </summary>
    public partial class UpdateWordWindow : Window
    {
        private List<string> _boundCategories;
        public List<string> BoundCategories
        {
            get { return _boundCategories; }
            set
            {
                /* EMPTY */
            }
        }

        private bool isChecked = false;

        public string initialName;
        public string name;
        public string description;
        public string category;
        public string imgPath;


        public UpdateWordWindow()
        {
            DataContext = this;

            /* Bind to the category list from Dictionary.cs */
            this._boundCategories = Dictionary.Categories;

            InitializeComponent();

            isChecked = false;
        }

        //getWord button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Word word = Dictionary.GetWord(initialName);

            if (word != null)
            {
                newNameTxt.Text = word.name;
                descriptionTxt.Text = word.description;
                cboCategory.SelectedIndex = cboCategory.Items.IndexOf(word.category);

                if (word.imagePath != null)
                {
                    imgPhoto.Source = new BitmapImage(new Uri(word.imagePath));
                }
                //else
                //{
                //    imgPhoto.Source = new BitmapImage(new Uri("C: /Users/Ina/Desktop/DictionaryApp/DictionaryApp/Resources/No - image - found.jpg"));
                //}

            }
            else
            {
                MessageBox.Show("Your word doesn't exist in the dictionary", "Word update");
            }
        }

        private void wordNameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            initialName = wordNameTxt.Text;
        }

        private void ckcBox_Checked(object sender, RoutedEventArgs e)
        {
            if (ckcBox.IsChecked != false)
            {
                isChecked = true;
            }
            else
            {
                isChecked = false;
            }
        }

        private void cboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            category = (string)cboCategory.SelectedItem;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminWindow a = new AdminWindow();
            a.ShowDialog();
            this.Close();
        }

        private void descriptionTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            description = descriptionTxt.Text;
        }

        private void newCategoryTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            if (isChecked && cboCategory.SelectedItem == null)
            {
                category = (string)newCategoryTxt.Text;

            }
            else if (!isChecked && cboCategory.SelectedItem == null)
            {
                MessageBox.Show("Please choose a category or insert a new one", "Your word doesn't have a category");
            }
        }

        private void btnSaveWord_Click(object sender, RoutedEventArgs e)
        {
            if (Dictionary.UpdateWord(initialName, name, description, category, imgPath))
            {
                MessageBox.Show("Your word has been updated", "Word updated");
            }
            else
            {
                if (wordNameTxt.Text == null)
                {
                    MessageBox.Show("Please enter a name for your word", "Your word doesn't have a name");
                }
                else if (descriptionTxt.Text == null)
                {
                    MessageBox.Show("Please enter a description for your word", "Your word doesn't have a description");
                }
                else if (cboCategory.SelectedItem == null)
                {
                    MessageBox.Show("Please enter a category for your word", "Your word doesn't have a category");
                }
                else
                {
                    MessageBox.Show("Word already in the dictionary", "Please enter a new word");
                }
            }
        }

        private void wordTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = newNameTxt.Text;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                imgPath = imgPhoto.Source.ToString();
            }
        }
    }
}
