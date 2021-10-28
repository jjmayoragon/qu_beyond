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
        List<matrix> mt = new List<matrix>();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void sliderSizeMatrix_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mt.Add(new matrix { x_pos = 0, y_pos = 1, letter = "h" });
            mt.Add(new matrix { x_pos = 0, y_pos = 1, letter = "t" });
            mt.Add(new matrix { x_pos = 4, y_pos = 5, letter = "z" });
            mt.Add(new matrix { x_pos = 7, y_pos = 2, letter = "k" });

            foreach (var m in mt)
            {
                MessageBox.Show(m.y_pos.ToString());
                MessageBox.Show(m.x_pos.ToString());
                MessageBox.Show(m.letter.ToString());
            }

        }

        class matrix
        {
            string string Name;
            public void Presentarse(string h)
            {
                Console.WriteLine("Hola {0}, segundo{1}", h, Name);
            }
            public static matrix Parsero(string str)
            {
                var m = new matrix();
                m.x_pos = 10;
                m.y_pos = 2;
                m.letter = "g";
            }
            public int x_pos { get; set; }
            public int y_pos { get; set; }
            public string letter { get; set; }
        }
    }
}
