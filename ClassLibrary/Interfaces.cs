using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class Interfaces
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
}
