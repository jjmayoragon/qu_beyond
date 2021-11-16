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

    public partial class MainWindow : Window
    {

        Cursor PencilCur, HammerCur, MinimalPen, MinimalCur, DisneyHand;

        Dictionary<int, string> dictionaryStreams;

        Dictionary<string, string> dictionaryCells;

        bool isMethodChecked;

        bool isRandomDice;

        //Matrix object values
        List<string> _matrix;

        int _cols;

        int _rows;

        List<string> _wordsToFind;

        public ObservableCollection<string> listWordsToFind { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            dictionaryStreams = new Dictionary<int, string>();
            
            dictionaryCells = new Dictionary<string, string>();
            
            _wordsToFind = new List<string>();
            
            _matrix = new List<string>();

            matrixContainer.Visibility = Visibility.Visible;

            //Dafault matrix dictionary
            CreateDictionaryCells(5, 5);

            //Default matrix size
            Format(5, 5);

            PaintChessColorsGridMatrix(5, 5);

            //Load cursor resources
            string cursorUri = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources";
            
            PencilCur = new Cursor($"{cursorUri}\\Pencil.cur");
            
            DisneyHand = new Cursor($"{cursorUri}\\DisneyHand.cur");
            
            MinimalCur = new Cursor($"{cursorUri}\\MinimalCursor.cur");
            
            MinimalPen = new Cursor($"{cursorUri}\\MinimalPen.cur");

            HammerCur = new Cursor($"{cursorUri}\\Hammer.cur");

            this.Cursor = MinimalCur;

            listWordsToFind = new ObservableCollection<string>();

            this.DataContext = this;
        }

        #region Form element events
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

            CreateDictionaryCells(rows, cols);

            Format(rows, cols);

            PaintChessColorsGridMatrix(rows, cols);

            SetAllRowsFromDictionaryCell();

            ValidateForm();
        }

        private void sldColumns_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var sld = (Slider)sender;

            var rows = Convert.ToInt32(lblRows.Content);

            var cols = Convert.ToInt32(sld.Value);

            CreateDictionaryCells(rows, cols);

            Format(rows, cols);

            PaintChessColorsGridMatrix(rows, cols);

            SetAllRowsFromDictionaryCell();

            ValidateForm();
        }


        private void row_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Grid)
            {
                var grid = (Grid)e.OriginalSource;

                var beforeColor = grid.Background;

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

        private void btnSetRowToMatrix_Click(object sender, RoutedEventArgs e)
        {
            if (txtSetRow.Text.Length == _cols)
            {
                var row = Convert.ToInt32(lblNumberOfRows.Content.ToString().Split(" ")[2]);

                var stream = txtSetRow.Text;

                SetSpecificRowFromMatrix(row, stream);

                gridSetRow.Visibility = Visibility.Collapsed;

                ValidateForm();
            }
            else
            {
                string msg = ("Text must have " + _cols.ToString() + " characters");

                MessageBox.Show(msg, "Validation Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }
        private void btnFindWord_click(object sender, RoutedEventArgs e)
        {
            SetMatrixValues(_rows, _cols, _matrix, _wordsToFind);
            
            GenerateMatrix();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SetRowVisible(false);
        }

 

        private void row_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Grid)
            {
                var grid = (Grid)e.OriginalSource;

                grid.Cursor = MinimalCur;

                var gName = grid.Name;

                var colIndex = gName.IndexOf("c");

                var rowIndex = gName.Substring(1, colIndex - 1);

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
        private void btn_refreshList(object sender, RoutedEventArgs e)
        {
            listWordsToFind.Clear();
            
            ValidateForm();
        }

        private void txtWordToFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtWordToFind.Text.Length > 0)
            {
                txtWordToFind.Background = Brushes.White;
            }
            else
            {
                txtWordToFind.Background = Brushes.White;
            }
        }
        private void txtSetRow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z) { }
            else { e.Handled = true; }
        }

        private void txtSetRow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) {e.Handled = true;}

            base.OnPreviewKeyDown(e);
        }

        private void txtWordToFind_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) {e.Handled = true;}
            base.OnPreviewKeyDown(e);
        }

        private void txtWordToFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z) { }
            else { e.Handled = true;}
        }

        private void method1_Checked(object sender, RoutedEventArgs e)
        {
            if (method1.IsChecked.Value || method2.IsChecked.Value)
                isMethodChecked = true;
            else
                isMethodChecked = false;

            ValidateForm();
        }

        private void method2_Checked(object sender, RoutedEventArgs e)
        {

            if (method1.IsChecked.Value || method2.IsChecked.Value)
                isMethodChecked = true;
            else
                isMethodChecked = false;

            ValidateForm();
        }

        private void method2_Unchecked(object sender, RoutedEventArgs e)
        {
            if (method1.IsChecked.Value || method2.IsChecked.Value)
                isMethodChecked = true;
            else
                isMethodChecked = false;

            ValidateForm();
        }

        private void method1_Unchecked(object sender, RoutedEventArgs e)
        {
            if (method1.IsChecked.Value || method2.IsChecked.Value)
                isMethodChecked = true;
            else
                isMethodChecked = false;

            ValidateForm();
        }

        private void row_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = string.Empty;

            var gridName = string.Empty;

            if (e.OriginalSource is Grid)
            {
                var grid = (Grid)e.OriginalSource;

                gridName = grid.Name;

                var index_row = grid.Name.IndexOf("r");

                var index_col = grid.Name.IndexOf("c") - 1;

                var lenghtNumber = index_col - index_row;

                row = grid.Name.Substring(index_row + 1, lenghtNumber);

                lblNumberOfRows.Content = "Setting row: " + row;

                SetRowVisible(true);

                txtSetRow.SelectAll();

                txtSetRow.Focus();

                ValidateForm();
            }

            if (sender is Label)
            {
                var lbl = (Label)sender;

                gridName = lbl.Name;

                var index_row = lbl.Name.IndexOf("r");

                var index_col = lbl.Name.IndexOf("c") - 1;

                var lenghtNumber = index_col - index_row;

                row = lbl.Name.Substring(index_row + 1, lenghtNumber);

                lblNumberOfRows.Content = "Setting row: " + row;

                SetRowVisible(true);

                txtSetRow.SelectAll();

                txtSetRow.Focus();

                ValidateForm(); 
            }

        }

        private void btnRollTheDice_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PopulateMarixRandomly();

            isRandomDice = true;

            matrixContainer.Margin = new Thickness(0, 0, 0, 0);

            gridWhiteTransparency.Visibility = Visibility.Collapsed;

            gridRollTheDice.Visibility = Visibility.Collapsed;
        }

        private void btnRollTheDice_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Image)
            {
                var img = (Image)e.OriginalSource;

                img.Margin = new Thickness(3, 3, 0, 0);

                lblRollTheDice.Visibility = Visibility.Collapsed;

                matrixContainer.Margin = new Thickness(0, 0, 0, 0);

                gridRollTheDice.Visibility = Visibility.Collapsed;

                gridWhiteTransparency.Visibility = Visibility.Collapsed;

                isRandomDice = false;

                PaintChessColorsGridMatrix(_rows, _cols);
            }
        }
        private void btnRollTheDice_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Image && !isRandomDice)
            {
                var img = (Image)e.OriginalSource;

                img.Cursor = DisneyHand;

                img.Margin = new Thickness(0, 0, 0, 0);

                gridWhiteTransparency.Visibility = Visibility.Visible;

                matrixContainer.Margin = new Thickness(10, 10, 10, 10);

                gridRollTheDice.Visibility = Visibility.Visible;
            }
        }


        private void btn_addWordToFind_click(object sender, MouseButtonEventArgs e)
        {
            if (txtWordToFind.Text.Length > 0)
            {
                listWordsToFind.Add(txtWordToFind.Text);

                txtWordToFind.SelectAll();

                txtWordToFind.Focus();

                ValidateForm();
            }

            else
            {
                //MessageBox.Show("Word to find is empty", "Validation Message", MessageBoxButton.OK, MessageBoxImage.Information);
                txtWordToFind.Background = Brushes.Firebrick;
                txtWordToFind.Focus();
            }

        }

        private void btn_AddWordToFind_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Grid)
            {
                var btn = (Grid)e.OriginalSource;

                btn.Cursor = Cursor;

                var color = new SolidColorBrush(Color.FromRgb(0xD3, 0x43, 0x4D));

                btn.Background = color;
            }
        }



        private void btn_addWordToFind_mouse_enter(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Grid)
            {
                var btn = (Grid)e.OriginalSource;

                btn.Cursor = DisneyHand;

                btn.Background = Brushes.Firebrick;
            }
        }

        #endregion

        #region Methods
        private void SetMatrixValues(int rows, int cols, List<string> matrix, List<string> wordsToFind)
        {
            _matrix.Clear();
            for (int i = 1; i <= rows; i++)
            {
                _matrix.Add(dictionaryStreams[i]);
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
            var matrix = new MatrixObj(_rows, _cols, _matrix, _wordsToFind);

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

            if (method2.IsChecked == true)
                HighLightCells(Utilities.CellsToHighLight);
        }


        private void ValidateForm()
        {

            if (listWordsToFind != null && dictionaryStreams.Count == _rows && dictionaryCells.Count == (_cols * _rows) && btnFindWords != null)
            {
                bool isStreamLenghtOk = true;

                foreach (var key in dictionaryStreams.Keys)
                {
                    if (dictionaryStreams[key].Length != _cols)
                    {
                        isStreamLenghtOk = false;

                        break;
                    }
                }

                if (isStreamLenghtOk && listWordsToFind.Count >= 1 && dictionaryStreams.Count == _rows && isMethodChecked)
                {
                    btnFindWords.IsEnabled = true;

                    btnFindWords.Background = Brushes.Firebrick;
                }
                else
                {
                    btnFindWords.IsEnabled = false;

                    btnFindWords.Background = Brushes.Black;
                }
            }
        }


        private void SetRowVisible(bool isVisible)
        {
            if (isVisible)
                gridSetRow.Visibility = Visibility.Visible;
            else
                gridSetRow.Visibility = Visibility.Collapsed;
        }

        public void HighLightCells(List<string> cellIndexs)
        {
            PaintChessColorsGridMatrix(_rows, _cols);

            foreach (var cell in cellIndexs)
            {
                var rowGrids = (Grid)UIHelper.FindChild<Grid>(matrixContainer, cell);

                var color = new SolidColorBrush(Color.FromRgb(0xD3, 0x43, 0x4D));

                rowGrids.Background = Brushes.Gold;

                var lbls = (Label)UIHelper.FindChild<Label>(rowGrids, cell + "lbl");

                lbls.Foreground = Brushes.RoyalBlue;
            }
        }

        private void PaintChessColorsGridMatrix(int rows, int cols)
        {
            _cols = cols;

            _rows = rows;

            int aux = 0;

            bool isPair = false;

            if (_cols % 2 == 0)
                isPair = true;

            foreach (var d in dictionaryCells)
            {
                var cell = (Grid)UIHelper.FindChild<Grid>(matrixContainer, d.Key);

                var cellName = cell.Name;

                var index = cellName.Split("c");

                var rowIndex = index[0].Substring(1);

                int colIndex = Convert.ToInt32(index[1]);

                cell.Background = aux % 2 == 0 ? Brushes.Cornsilk : Brushes.White;

                if (isPair && colIndex == _cols)
                    aux++;

                aux++;
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

            //New grids inside container
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var innerGrid = new Grid();

                    Grid.SetRow(innerGrid, i);

                    Grid.SetColumn(innerGrid, j);

                    var cellName = ("r" + (i + 1) + "c" + (j + 1)).ToString();

                    var fontSize = 40;
                    if (_cols > 15 || _rows > 12)
                        fontSize = 25;
                    if (_cols > 20 || _rows > 16)
                        fontSize = 20;
                    if (_cols > 25 || _rows > 20)
                        fontSize = 15;
                    if (_cols > 30 || _rows > 28)
                        fontSize = 10;
                    if (_cols > 50 || _rows > 40)
                        fontSize = 8;
                    if (_cols > 60 || _rows > 60)
                        fontSize = 6;

                    var lbl = new Label
                    {
                        Name = cellName + "lbl",

                        FontSize = fontSize,

                        Margin = new Thickness(0, 0, 0, 0),
      
                        Content = dictionaryCells[cellName],

                        Foreground = Brushes.Black,

                        VerticalAlignment = VerticalAlignment.Center,

                        HorizontalAlignment = HorizontalAlignment.Center                        
                    };

                    innerGrid.Children.Add(lbl);

                    innerGrid.Name = cellName;

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

        
        private void PopulateMarixRandomly()
        {
            for (int i = 1; i <= _rows; i++)
            {
                dictionaryStreams[i] = Utilities.RandomString(_cols);

                SetSpecificRowFromMatrix(i, dictionaryStreams[i]);
            }
            ValidateForm();
        }

        private void CreateDictionaryCells(int rows, int cols)
        {
            //Obtain data from old dictionary
            var oldDictionary = new Dictionary<string, string>(dictionaryCells);

            dictionaryCells.Clear();

            _cols = cols;
            
            _rows = rows;

            dictionaryCells.Clear();

            var totalCells = _cols * _rows;

            for (int i = 1; i <= _rows; i++)
            {
                var r = "r" + i.ToString();

                for (int j = 1; j <= _cols; j++)
                {
                    var c = "c" + j.ToString();

                    var cellName = r + c;

                    dictionaryCells[cellName] = String.Empty;
                }
            }

            //Add values from old dictionary
            foreach (string key in oldDictionary.Keys)
            {
                if (dictionaryCells.ContainsKey(key))
                    dictionaryCells[key] = oldDictionary[key];
            }
        }

        private void SetSpecificRowFromMatrix(int row, string stream)
        {
            //Add stream to dictionary
            dictionaryStreams[row] = stream;

            var cols = sldColumns.Value;

            for (int i = 1; i <= cols; i++)
            {
                //Add specific letter to dictionary for cells
                var r = "r" + row;

                var c = "c" + i;

                var cellName = r + c;

                var letter = stream.Substring(i - 1, 1);

                dictionaryCells[cellName] = letter;

                var gridCell = (Grid)UIHelper.FindChild<Grid>(matrixContainer, cellName);

                var lblCell = (Label)UIHelper.FindChild<Label>(gridCell, cellName + "lbl");

                lblCell.Content = letter;
            }
        }

        private void SetAllRowsFromDictionaryCell()
        {
            dictionaryStreams.Clear();

            int c = 1;

            int r = 1;

            string stream = String.Empty;

            foreach (var key in dictionaryCells.Keys)
            {
                stream += dictionaryCells[key];

                if (c == _cols)
                {
                    dictionaryStreams[r] = stream;

                    stream = String.Empty;

                    c = 1;

                    r++;

                    continue;
                }
                c++;
            }
        }
        #endregion
    }
}
