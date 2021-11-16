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

        public MatrixObj() { }

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
}
