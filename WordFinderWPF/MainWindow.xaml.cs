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
using WordFinderWPF.Interfaces;
using WordFinderWPF.Classes;
using System.Xml.Linq;
using System.IO;


namespace WordFinderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Cursor PencilCur, HammerCur, MinimalPen, MinimalCur;
        

        public MainWindow()
        {
            InitializeComponent();
           
            matrixContainer.Visibility = Visibility.Visible;

            Format(5,5);
           

            //Load cursor resources
            string cursorUri = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName+"\\Resources";
            PencilCur = new Cursor($"{cursorUri}\\Pencil.cur");
            HammerCur = new Cursor($"{cursorUri}\\Hammer.cur");
            MinimalPen = new Cursor($"{cursorUri}\\MinimalPen.cur");
            MinimalCur = new Cursor($"{cursorUri}\\MinimalCursor.cur");

            this.Cursor = MinimalCur;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //EVENTS
            //Matrix object to be send
            var matrix = new MatrixObj();

            //Publisher
            var generator = new MatrixGenerator();

            //Suscribers
            var hardCodeMatrix = new MessageBoxService();
            
            //Suscription
            generator.MatrixGenerated += hardCodeMatrix.OnMatrixGenerated;

            //Execute event and call suscribers methods
            generator.Generate(matrix);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            setSizePanel.Visibility = Visibility.Collapsed;
            matrixContainer.Visibility = Visibility.Visible;
        }

 



        private void btnGenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            var rows = Convert.ToInt32(lblRows.Content);
            var cols = Convert.ToInt32(lblColumns.Content);

            matrixContainer.Visibility = Visibility.Visible;
            setSizePanel.Visibility = Visibility.Collapsed;

            Format(rows, cols);
        }

       

        private void Format(int rows, int cols)
        {
            matrixContainer.Children.Clear();
            matrixContainer.RowDefinitions.Clear();
            matrixContainer.ColumnDefinitions.Clear();

            //Inner sections for the matrix
            //Row & Columns Definitions
            for (int i = 0; i < rows; i++)
            {
                //Add rows
                matrixContainer.RowDefinitions.Add(new RowDefinition());
            }
            for (int j = 0; j < cols; j++)
            {
                //Add columns
                matrixContainer.ColumnDefinitions.Add(new ColumnDefinition());
            }

            int aux = 0;

            //New grids inside container
            for (int i = 0; i < rows; i++)
            {
                aux = i % 2 == 0 ? 0 : 1;
                
                for (int j = 0; j < cols; j++)
                {
                    var innerGrid = new Grid();

                    Grid.SetRow(innerGrid, i);
                    Grid.SetColumn(innerGrid, j);

                    var index = ("r" + (i + 1) + "c" + (j + 1)).ToString();



                    innerGrid.Background = aux % 2 == 0 ? Brushes.Cornsilk : Brushes.White;
                    aux++;

                    var lbl = new Label
                    {
                        Name = index + "lbl",
                        Content = index.Substring(1, index.IndexOf("c") - 1) + index.Substring(index.IndexOf("c") + 1),
                        FontSize = 40,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };

                    
                    innerGrid.Children.Add(lbl);

                    innerGrid.Name = index;
                    innerGrid.MouseEnter += new MouseEventHandler(row_MouseEnter);
                    innerGrid.MouseLeave += new MouseEventHandler(row_MouseLeave);
                    innerGrid.MouseLeftButtonDown += new MouseButtonEventHandler(row_MouseLeftButtonDown);
                    lbl.MouseLeftButtonDown += new MouseButtonEventHandler(row_MouseLeftButtonDown);

                    matrixContainer.Children.Add(innerGrid);
                }
            }

            setSizePanel.Visibility = Visibility.Collapsed;
            matrixContainer.Visibility = Visibility.Visible;
            matrixContainer.Opacity = 1;
        }

    

        private void sldColumns_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            setSizePanel.Visibility = Visibility.Visible;
            matrixContainer.Opacity = 0.1;
            
           
        }

        private void sldRows_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            setSizePanel.Visibility = Visibility.Visible;
            matrixContainer.Opacity = 0.1;
        }



        private void sldRows_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var sld = (Slider)sender;

            var rows = Convert.ToInt32(sld.Value);
            var cols = Convert.ToInt32(lblColumns.Content);

            Format(rows, cols);
        }

        private void sldColumns_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var sld = (Slider)sender;

            var rows = Convert.ToInt32(lblRows.Content);
            var cols = Convert.ToInt32(sld.Value);

            Format(rows, cols);
        }

    
        private void row_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Grid)
            {
                var grid = (Grid)e.OriginalSource;

                grid.Cursor = MinimalPen;

                var gName = grid.Name;
                var colIndex = grid.Name.IndexOf("c");

                

                int cols = Convert.ToInt32(lblColumns.Content);

                for (int i = 1; i <= cols; i++)
                {
                    var name = gName.Substring(0, colIndex) + "c" + i;
                    var rowGrids = (Grid)UIHelper.FindChild<Grid>(matrixContainer, name);
                    var color = new SolidColorBrush(Color.FromRgb(0xD3, 0x43, 0x4D));
                    rowGrids.Background = Brushes.Gold;
                    var lbls = (Label)UIHelper.FindChild<Label>(rowGrids, name + "lbl");
                    lbls.Foreground = Brushes.RoyalBlue;
                }

            }


        }
        private void row_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Grid)
            {

                var grid = (Grid)e.OriginalSource;

                var name = grid.Name;

                MessageBox.Show(name);

               

            }

            if(sender is Label)
            {
                var lbl = (Label)sender;

                var name = lbl.Name;

                var index = name.IndexOf("lbl");

                MessageBox.Show(name.Substring(0, index));
            }
        }

        private void row_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Grid)
            {

                var grid = (Grid)e.OriginalSource;  

                grid.Cursor = MinimalCur;

                var gName = grid.Name;
                var colIndex = gName.IndexOf("c");


                var rowIndex = gName.Substring(1, colIndex-1);

                int cols = Convert.ToInt32(lblColumns.Content);
                int aux = Convert.ToInt32(rowIndex) % 2 == 0 ? 1 : 0;

                for (int i = 1; i <= cols; i++)
                {
                    var name = gName.Substring(0, colIndex) + "c" + i;
                    var rowGrids = (Grid)UIHelper.FindChild<Grid>(matrixContainer, name);
                    rowGrids.Background = aux % 2 == 0 ? Brushes.Cornsilk : Brushes.White;
                    aux++;
                    var lbls = (Label)UIHelper.FindChild<Label>(rowGrids, name + "lbl");
                    lbls.Foreground = Brushes.Black;
                }

            }
           
        }
    }

    

}
