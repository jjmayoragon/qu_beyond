using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinderWPF
{
    public class WordFinder
    {
        //Initializing generics
        private readonly IEnumerable<string> _matrix = new List<string>();
        private readonly List<string> _allStreams = new List<string>();
        private List<string> _foundList = new List<string>();

        private readonly int _streamLenght;

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix;

            //Initialize word stream lenght
            _streamLenght = _matrix.First().Count();

            //Get all word streams
            _allStreams = GetAllStreams(matrix);
        }

        private List<string> GetAllStreams(IEnumerable<string> matrix)
        {
            var allStreams = new List<string>();

            //Get Rows from Matrix
            foreach (var row in matrix)
            {
                allStreams.Add(row);
            }

            //Get Columns from Matrix
            for (int i = 0; i < _streamLenght; i++)
            {
                var column = String.Empty;
                foreach (var row in matrix)
                {
                    column += row[i];
                }
                allStreams.Add(column);
            }

            //Return one large list ready to use for linq methods
            return allStreams;
        }
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {

            foreach (var row in wordstream)
            {
                //Linq extension methods will allow us to query the generic in a native way and high performance
                //FirstOrDefault will find the first result, otherwise will return "null". Also will avoid repeated results."
                //Lambda expressions and delegates are used for cleaner code
                var query = _allStreams
                    .Where(m => m.Contains(row))
                    .FirstOrDefault();

                //Add result if not null.
                if (query != null)
                    _foundList.Add(row);

                //Only Top 10 words break the loop and continue the next statement
                if (_foundList.Count > 10)
                    break;

            }

            //If no words are found, result will be an empty set of strings.
            return _foundList;
        }
    }
}
