using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinderWPF.Interfaces;

namespace WordFinderWPF.Classes
{
    public class SearchEngine
    {
        private readonly IWordFinder _wordFinder;
        private readonly IList<IWordFinder> _performers = new List<IWordFinder>();
        private readonly IEnumerable<string> _wordstream = new List<string>();

        public SearchEngine(IEnumerable<string> wordstream)
        {            
            _wordstream = wordstream;
        }

        public IEnumerable<string> StartSearch()
        {
            return _performers.First().Find(_wordstream);    
        }

        public void AddMethod(IWordFinder wordFinder)
        {
            _performers.Add(wordFinder);
        }
    }
}
