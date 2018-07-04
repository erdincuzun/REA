using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library_ABE.text
{
    class wtn
    {
        private int truncsize = 5;

        public wtn()
        {
            truncsize = 5; //default durum
        }

        public wtn(int _truncsize)
        {
            truncsize = _truncsize; //default durum
        }

        public string StemWord(string word)
        {
            if (word.Length > truncsize)
                word = word.Substring(0, truncsize);

            return word;
        }
    }
}
