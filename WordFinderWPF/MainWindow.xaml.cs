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
            var matrix = new List<string>() 
            {
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxygatoyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "hqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "ivdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "lbcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "lgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgatorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "gqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "avdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "tgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "ohillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx"

            };

            var wordFinder = new WordFinder(matrix);

            var wordStream = new List<string>() { "cold", "gato", "chill", "snow", "gato" };

            var result = wordFinder.Find(wordStream);

            MessageBox.Show(result.Count().ToString() + " words found");

            //Show Top 10 words founded
            foreach (var r in result)
            {
                MessageBox.Show(r, "Word found");
            }

        }
    }
}
