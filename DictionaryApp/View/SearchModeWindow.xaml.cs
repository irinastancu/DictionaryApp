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
    /// Interaction logic for SearchModeWindow.xaml
    /// </summary>
    public partial class SearchModeWindow : Window
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

        public SearchModeWindow()
        {
            DataContext = this;

            var tmpList = new List<string>(Dictionary.Categories);
            tmpList.Insert(0, "All categories");

            this._boundCategories = tmpList;
            
            InitializeComponent();
            
            cboCategory.SelectedItem = _boundCategories[0];
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow m = new MainWindow();
            m.ShowDialog();
            this.Close();
        }

        private void txtSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //call function from Dictionary
            string category = cboCategory.Text;
            List<string> predictiveText;

            if (!category.Equals("All categories"))
            {
                predictiveText = Dictionary.GetPredictiveList(txtSearchBar.Text, category);
            }
            else
            {
                predictiveText = Dictionary.GetPredictiveList(txtSearchBar.Text);
            }

            listPredictiveText.ItemsSource = predictiveText;
        }

        private void listPredictiveText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = listPredictiveText.SelectedItem as string;

            Word word = Dictionary.GetWord(name);

            string message = $"Name: {word.name} \n" +
                $"Description: {word.description}\n" +
                $"Category: {word.category}\n";

            MessageBox.Show(message, "Word information");
        }
    }
}
