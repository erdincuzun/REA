using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library_ABE.BayesClassifier
{
    public class catDictionary
    {
        private Dictionary<string, double> category_word;
        private int CatName;
        private double totalwords;

        public catDictionary(int _cName)
        {
            CatName = _cName;
            category_word = new Dictionary<string, double>();
            totalwords = 0;
        }

        public void Add(string _word)
        {
            if (category_word.ContainsKey(_word))
                category_word[_word] = (double)category_word[_word] + 1;
            else
                category_word.Add(_word, 1);

            totalwords++;
        }

        public int Search(string _word)
        {
            if (category_word.ContainsKey(_word))
                return (int)category_word[_word]; //kelimeden var ve sayısı
            else
                return 0; //kelimeden yok
        }

        public int CategoryName
        {
            get { return CatName; }
        }

        public double CountofWordsinCategory
        {
            get { return totalwords; }
        }

        public Dictionary<string, double> WordsinCategory
        {
            get { return category_word; }
        }
    }
}
