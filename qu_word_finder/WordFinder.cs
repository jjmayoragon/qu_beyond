using System.Collections.Generic;

namespace qu_word_finder
{
    public class WordFinder
    {
        private readonly IEnumerable<string> _matrix;
        private readonly IEnumerable<string> _result;

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream) 
        {
            //Compare

            //Do the job

            //Result
            return _result;
        }


    } 
}
