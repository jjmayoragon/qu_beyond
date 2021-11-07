using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinderWPF.Interfaces;
using WordFinderWPF.Classes;

namespace WordFinderWPF
{
    public class WordFinder: IWordFinder
    {
        //Initializing generics
        private readonly IEnumerable<string> _matrix = new List<string>();
        private List<string> _foundListNoRepeated = new List<string>();
        private List<string> _foundList = new List<string>();

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix;
        }

        
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            foreach (var word in wordstream)
            {
                //Linq extension methods will allow us to query the generic in a native way and high performance
                //FirstOrDefault will find the first result, otherwise will return "null". Also will avoid repeated results."
                //Lambda expressions and delegates are used for cleaner code
                var query = _matrix
                    .Where(m => m.Contains(word))
                    .FirstOrDefault();

                //Add result if not null.
                if (query != null)
                {
                    //Add result if the word is not repeated
                    if (!_foundListNoRepeated.Contains(word))
                    {
                        //Separate index and stream
                        string[] stream_index = query.Split("_");

                        //Get index
                        var index = stream_index[0];

                        //Add the word found with row index where it was found
                        _foundList.Add(index + "_" + word);

                        //List to check non repeated
                        _foundListNoRepeated.Add(word);

                    }
                    
                }
                //Only Top 10 words break the loop and continue the next statement
                if (_foundList.Count > 10)
                    break;
            }

            //If no words are found, result will be an empty set of strings.
            return _foundList;
        }
    }
}
