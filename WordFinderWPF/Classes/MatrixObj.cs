using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public int _rows { get; set; }
        public int _columns { get; set; }
        public List<string> _matrix { get; set; }
        public List<string> _wordsForSearch { get; set; }
    }



    //EVENT PUBLISHER
    public class MatrixGenerator
    {
        //Define an event using EventHandler<> native delegate
        public event EventHandler<MatrixEventArgs> MatrixGenerated;

        //Raise the event
        protected virtual void OnMatrixGenerated(MatrixObj matrix)
        {

            if(MatrixGenerated != null)
            {
                MatrixGenerated(this, new MatrixEventArgs() { matrix = matrix });
               
            }

        }

        public void Generate(MatrixObj matrix)
        {
            OnMatrixGenerated(matrix);
        }

    }


    //EVENT SUSCRIBER
    public class Method4
    {

    }
    public class Method3
    {
        //EVENT LISTENER
        public void OnMatrixGenerated(object sender, MatrixEventArgs args)
        {

        }
    }

    //EVENT SUSCRIBER
    public class Method2_XAMLGenerator_LabelNotification
    {
        //EVENT LISTENER
        public void OnMatrixGenerated(object sender, MatrixEventArgs args)
        {

        }

    }

    //EVENT SUSCRIBER
    public class MessageBoxService
    {
        //EVENT LISTENER
        public void OnMatrixGenerated(object sender, MatrixEventArgs args)
        {
            args.matrix._columns = 5;
            args.matrix._rows = 5;

            args.matrix._matrix = new List<string>()
            {
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxygatoyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "hqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "ivdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "lbcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "lgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "abcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcdcabcd",
                "rgwiorgwiorgwiorgatorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "chillchillchillchillchillchillchillchillchillchillchillchillchil",
                "gqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "avdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx",
                "tgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwiorgwi",
                "ohillchillchillchillchillchillchillchillchillchillchillchillchil",
                "pqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqnsdpqns",
                "uvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdxyuvdx"

            };
            args.matrix._wordsForSearch = new List<string>() { "cold", "gato", "chill", "snow", "gato" };


            //Custom Implementation
            var results = Utilities.CallInterface(args.matrix);

            MessageBox.Show("Se encontraron: " + results.Count() + " resultados.");

            foreach (var r in results)
            {
                MessageBox.Show(r);
            }


            
        }
    }
}
