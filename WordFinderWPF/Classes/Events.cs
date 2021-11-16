using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WordFinderWPF.Classes
{
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

                if (index <= args.matrix._rows)
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

                    start = col_content.IndexOf(word) + 1;

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

            MessageBox.Show("Number of words found: " + results.Count(), "Words founded", MessageBoxButton.OK, MessageBoxImage.Information);

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
                MessageBox.Show(msg, "Top 10 Words found", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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

}
