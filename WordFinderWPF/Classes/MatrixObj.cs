using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using WordFinderWPF;

namespace WordFinderWPF.Classes
{
    //Create object with Eventargs inheritance
    public class MatrixEventArgs : EventArgs
    {
        public MatrixObj matrix { get; set; }
    }
    
    //Matrix class
    public class MatrixObj
    {
        private int _streamLenght;
        public int _rows { get; set; }
        public int _columns { get; set; }
        public List<string> _matrix { get; set; }

        public List<string> _allMatrixStreams { get; set; }

        public List<string> _wordsForSearch { get; set; }

        public List<string> _wordsFound { get; set; }


        //Setting overloads
        public MatrixObj(int rows, int cols, List<string> matrix, List<string> wordsForSearch)
        {
            _rows = rows;
            _columns = cols;
            _matrix = matrix;
            _wordsForSearch = wordsForSearch;

            //Initialize word stream lenght
            _streamLenght = _matrix.First().Count();

            //Get all word streams
            _allMatrixStreams = GetAllStreams(_matrix);
        }
        public MatrixObj(int rows, int cols)
        {
            _rows = rows;
            _columns = cols;
        }
       
        public MatrixObj( List<string> wordsForSearch)
        {
            _wordsForSearch = wordsForSearch;
        }


        public MatrixObj()
        {
            
            //Hardcode overload
            _columns = 5;
            _rows = 5;

            _matrix = new List<string>()
            {
                "gatoc",
                "cgsih",
                "ohnli",
                "lqosl",
                "dvwxl"
            };

            _wordsForSearch = new List<string>() { "cold", "gato", "chill", "snow", "gato" };

            //Initialize word stream lenght
            _streamLenght = _matrix.First().Count();

            //Get all word streams
            _allMatrixStreams = GetAllStreams(_matrix);

        }

        private List<string> GetAllStreams(IEnumerable<string> matrix)
        {
            //Initialize word stream lenght
            _streamLenght = _matrix.First().Count();

            var allStreams = new List<string>();
            int iterator = 1;
            //Get Rows from Matrix
            foreach (var row in matrix)
            {
                allStreams.Add(iterator + "_" + row);
                iterator++;
            }

            //Get Columns from Matrix
            for (int i = 0; i < _streamLenght; i++)
            {
                var column = String.Empty;
                foreach (var row in matrix)
                {
                    column += row[i];
                }
                allStreams.Add(iterator + "_" + column);
                iterator++;
            }

            //Return one large list ready to use for linq methods
            return allStreams;
        }

        public List<string> GetMatrix()
        {
            return _matrix;
        }
    }

   

    //EVENT PUBLISHER
    public class MatrixGenerator
    {
        //Define an event using EventHandler<> native delegate
        public event EventHandler<MatrixEventArgs> MatrixGenerated;

        //Raise the event
        protected virtual void OnMatrixGenerated(MatrixObj matrix)
        {
            if (MatrixGenerated != null)
                MatrixGenerated(this, new MatrixEventArgs() { matrix = matrix });
            
        }

        public void Generate(MatrixObj matrix)
        {
            OnMatrixGenerated(matrix);
        }

    }


    //EVENT SUSCRIBER
    public class Method4
    {
        //EVENT LISTENER
        public void OnMatrixGenerated(object sender, MatrixEventArgs args)
        {

        }
    }
    public class Method3
    {
        //EVENT LISTENER
        public void OnMatrixGenerated(object sender, MatrixEventArgs args)
        {

        }
    }

    //EVENT SUSCRIBER
    public class HighLightWordsFoundService
    {
        //This method highlight the words found in the matrix cells
        private Dictionary<int, string> _indexWordsFound;

        private List<string> _cellsToHighLight;

        public HighLightWordsFoundService()
        {
            _indexWordsFound = new Dictionary<int, string>();

            _cellsToHighLight = new List<string>();
        }

        

        //EVENT LISTENER
        public void OnMatrixGenerated(object sender, MatrixEventArgs args)
        {
            //Call interface
            var results = Utilities.CallInterface(args.matrix);

            //Custom Implementation
            //Get the cells to be highlight in the matrix cells
            foreach (var r in results)
            {
                string[] split = r.Split("_");


                var row = string.Empty;
                var col = string.Empty;
                var cellIndex = string.Empty;

                var index = Convert.ToInt32(split[0]);
                var word = split[1];

                int start;

                _indexWordsFound[index] = word;

                if(index <= args.matrix._rows)
                {
                    //Row
                    row = "r" + index.ToString();

                    string[] splitRow = args.matrix._allMatrixStreams[index - 1].Split("_");

                    var row_index = splitRow[0];

                    var row_content = splitRow[1];

                    start = row_content.IndexOf(word) + 1;

                    for (int j = start; j < start + word.Length; j++)
                    {
                        //Col
                        col = "c" + j.ToString();

                        cellIndex = row + col;

                        _cellsToHighLight.Add(cellIndex);
                    }
                    
                }
                else
                {
                    //Column
                    col = "c" + (index - args.matrix._rows).ToString();

                    string[] splitCol = args.matrix._allMatrixStreams[index - 1].Split("_");

                    var col_index = splitCol[0];

                    var col_content = splitCol[1];
                
                    start = col_content.IndexOf(word)+1;

                    for (int j = start; j < start + word.Length; j++)
                    {
                        //Row
                        row = "r" + j.ToString();

                        cellIndex = row + col;

                        _cellsToHighLight.Add(cellIndex);
                    }
                    

                }
                       
            }

            Utilities.CellsToHighLight = _cellsToHighLight;
        }

    }

    //EVENT SUSCRIBER
    public class MessageBoxService
    {
        //Simple method, calls interface and show results in MessageBox

        //EVENT LISTENER
        public void OnMatrixGenerated(object sender, MatrixEventArgs args)
        {
            //Call interface
            var results = Utilities.CallInterface(args.matrix);

            //Custom implementation with words found results
            var indexer = new Dictionary<string, int>();

            MessageBox.Show("Number of words found: "+ results.Count(),"Words founded", MessageBoxButton.OK, MessageBoxImage.Information);

            foreach (var r in results)
            {
                string[] split = r.Split("_");

                var index = Convert.ToInt32(split[0]);

                var word = split[1];

                var msg = string.Empty;

                if (index <= args.matrix._rows)
                {
                    msg = ("Row: " + index.ToString() + "  Word: " + word.ToString());
                }
                else
                {
                    index -= args.matrix._rows;

                    msg = ("Column: " + index.ToString() + "  Word: " + word.ToString());
                }

                MessageBox.Show(msg,"Top 10 Words found",MessageBoxButton.OK,MessageBoxImage.Information);

            }

        }
    }

    
}
