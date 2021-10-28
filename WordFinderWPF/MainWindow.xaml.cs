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

namespace WordFinderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Hardcoded entry values
            var matrix = new List<string>() { "abcdc", "rgwio", "chill", "pqnsd", "uvdxy" };

            var wordFinder = new WordFinder(matrix);

            var wordStream = new List<string>() { "dsds", "chill" };

            var result = wordFinder.Find(wordStream);

            MessageBox.Show(result.Count().ToString() + " words found");

            //Showing Top 10 words finder results
            foreach (var r in result)
            {
                MessageBox.Show(r, "Word found");
            }

        }
    }
}
