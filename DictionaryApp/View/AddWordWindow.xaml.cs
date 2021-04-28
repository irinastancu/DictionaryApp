using DictionaryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DictionaryApp
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddWordWindow : Window
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

        public string imgPath;

        //public ImageSource ImageSource
        //{
        //    get { return ImageSource; }
        //    set
        //    {
        //        /* EMPTY */
        //    }
        //}

        public AddWordWindow()
        {
            DataContext = this;

            /* Bind to the category list from Dictionary.cs */
            this._boundCategories = Dictionary.Categories;

            InitializeComponent();
            //DataContext = d.words;
        }

        //public void LoadImage()
        //{
        //    ImageSource = new BitmapImage(new Uri("assets/images/yourImage.jpg", UriKind.Relative));
        //}


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name;
            string description;
            string category;

            name = txtBoxName.Text;
            description = txtBoxDescription.Text;
            category = cboCategory.Text;

            if (ckcBox.IsChecked == true)
            {
                category = txtNewCategory.Text;
            }
            
            string message = Dictionary.AddWord(name, description, category, imgPath);

            System.Windows.MessageBox.Show(message);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminWindow a = new AdminWindow();
            a.ShowDialog();
            this.Close();
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
