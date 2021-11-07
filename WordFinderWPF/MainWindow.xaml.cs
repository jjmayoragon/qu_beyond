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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WordFinderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        Cursor PencilCur, HammerCur, MinimalPen, MinimalCur;
        Dictionary<int, string> dictionaryStream;
        Dictionary<string, string> dictionaryCharacter;

        
        
        //Matrix objet values
        List<string> _matrix;
        int _cols;
        int _rows;
        List<string> _wordsToFind;

        public ObservableCollection<string> listWordsToFind { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            dictionaryStream = new Dictionary<int, string>();
            dictionaryCharacter = new Dictionary<string, string>();
            _wordsToFind = new List<string>();
            _matrix = new List<string>();

            matrixContainer.Visibility = Visibility.Visible;

            Format(5, 5);

            //Load cursor resources
            string cursorUri = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources";
            PencilCur = new Cursor($"{cursorUri}\\Pencil.cur");
            HammerCur = new Cursor($"{cursorUri}\\Hammer.cur");
            MinimalPen = new Cursor($"{cursorUri}\\MinimalPen.cur");
            MinimalCur = new Cursor($"{cursorUri}\\MinimalCursor.cur");

            this.Cursor = MinimalCur;

            DataContext = this;

            listWordsToFind = new ObservableCollection<string>();



        }
      

        

      
        private void btnGenerate_click(object sender, RoutedEventArgs e)
        {
            SetMatrixValues(_cols,_rows,_matrix,_wordsToFind);
            GenerateMatrix();
        }

        private void SetMatrixValues(int rows, int cols, List<string> matrix, List<string> wordsToFind)
        {
            _matrix.Clear();
            for (int i = 1; i <= rows; i++)
            {
                _matrix.Add(dictionaryStream[i]);
            }

            _wordsToFind.Clear();
            foreach (var item in listWordsToFind)
            {
                _wordsToFind.Add(item.ToString());
            }
        }

        private void GenerateMatrix()
        {
            //EVENTS
            //Matrix object to be send as a VideEventArgs
            var matrix = new MatrixObj(_cols, _rows, _matrix, _wordsToFind);

            //Publisher
            var generator = new MatrixGenerator();

            //Suscribers
            var messageBox = new MessageBoxService();
            var hightLightWordsFound = new HighLightWordsFoundService();

            //Suscriptions
            if (method1.IsChecked == true)
                generator.MatrixGenerated += messageBox.OnMatrixGenerated;
            if (method2.IsChecked == true)
                generator.MatrixGenerated += hightLightWordsFound.OnMatrixGenerated;

            //Execute event and call suscribers methods
            generator.Generate(matrix);

            HighLightCells(Utilities.CellsToHighLight);
        }


        public void HighLightCells(List<string> cellIndexs)
        {

            foreach (var cell in cellIndexs)
            {
                var rowGrids = (Grid)UIHelper.FindChild<Grid>(matrixContainer, cell);

                var color = new SolidColorBrush(Color.FromRgb(0xD3, 0x43, 0x4D));

                rowGrids.Background = Brushes.Gold;

                var lbls = (Label)UIHelper.FindChild<Label>(rowGrids, cell + "lbl");

                lbls.Foreground = Brushes.RoyalBlue;

               
            }

        }



        private void Format(int rows, int cols)
        {
            _cols = cols;
            _rows = rows;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetRowVisible(true);

           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SetRowVisible(false);

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

        private void btnAddRowToMatrix_Click(object sender, RoutedEventArgs e)
        {
            var row = Convert.ToInt32(lblNumberOfRows.Content.ToString().Split(" ")[2]);
            var stream = txtSetRow.Text;
            AddRowToMatrix(row, stream);

            gridSetRow.Visibility = Visibility.Collapsed;

            
        }
        private void AddRowToMatrix(int row,string stream)
        {
            //Add stream to dictionary
            dictionaryStream[row] = stream;

            //MessageBox.Show(dictionaryStream[row]);

            var cols = sldColumns.Value;

            for (int i = 1; i <= cols; i++)
            {
                //Add specific letter to dictionary for cells
                var r = "r" + row;
                var c = "c" + i;
                var cellName = r + c;
                var letter = stream.Substring(i - 1, 1);
                dictionaryCharacter[cellName] = letter;

                var gridCell = (Grid)UIHelper.FindChild<Grid>(matrixContainer, cellName);
                var lblCell = (Label)UIHelper.FindChild<Label>(gridCell, cellName+"lbl");

                lblCell.Content = letter;
  
            }

           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SetRowVisible(false);
        }

        private void btn_addWordToFind(object sender, RoutedEventArgs e)
        {
            listWordsToFind.Add(txtWordToFind.Text);
        }

        private void row_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = string.Empty;

            var gridName = string.Empty;

            if (e.OriginalSource is Grid)
            {
                var grid = (Grid)e.OriginalSource;

                gridName = grid.Name;

                var index = grid.Name.IndexOf("r");

                row = grid.Name.Substring(index + 1, 1);

                //MessageBox.Show(row);
            }

            if (sender is Label)
            {
                var lbl = (Label)sender;

                gridName = lbl.Name;

                var index = lbl.Name.IndexOf("r");

                row = lbl.Name.Substring(index + 1, 1);

                //MessageBox.Show(row);
            }

            lblNumberOfRows.Content = "Setting row: " + row;

            SetRowVisible(true);

            



            


        }

        private void SetRowVisible(bool isVisible)
        {
            if (isVisible)
                gridSetRow.Visibility = Visibility.Visible;
            else
                gridSetRow.Visibility = Visibility.Collapsed;
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
