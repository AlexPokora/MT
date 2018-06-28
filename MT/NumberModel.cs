using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MT
{
    class NumberModel : Window, INotifyPropertyChanged
    {
        public void FuckRandom()
        {
            Random rnd = new Random();
            _firstNumber = rnd.Next(1, 100);
            _secondNumber = rnd.Next(1, 100);
        }

        private int _firstNumber;
        private int _secondNumber;
        private int _answer;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Formula
         => $"{_firstNumber}, '+' ,{_secondNumber}, '='";

        public int Answer
        {
            get
            {
                return _answer;
            }

            set
            {
                _answer = value;
            }
        }

    }
}
