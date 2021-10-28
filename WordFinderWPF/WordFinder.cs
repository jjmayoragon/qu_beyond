using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinderWPF
{
    public class WordFinder
    {
        //Initialize generic for best practice
        private readonly IEnumerable<string> _matrix = new List<string>();

        //Also if no words are found, the "Find" method should return an empty sent of strings
        private List<string> result = new List<string>();

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {

            foreach (var word in wordstream)
            {
                //Linq functions for high performance will allow us to iterate the generic IEnumerable in a native way.
                //FirstOrDefault will find the first result or throw null.
                //FirstOrDefault will avoid repeated results."
                //Lambda expressions and delegates are used for clean code
                //Use of notation convention for a friendly code
                var query = _matrix
                    .Where(m => m.Contains(word))
                    .FirstOrDefault();

                //Only add results if not null.
                if (query != null)
                    result.Add(word);

                //Only Top 10 words break the loop and continue the next statement
                if (result.Count > 10)
                    break;

            }

            //If no words are found, result will be an empty set of strings.
            return result;
        }
    }
}
