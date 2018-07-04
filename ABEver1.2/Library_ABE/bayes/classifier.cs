using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Library_ABE.text;

namespace Library_ABE.BayesClassifier
{
    public class classifier
    {
        private Dictionary<int, catDictionary> _cat_dic;

        word_dic _wd;
        private string language;
        private string stemming;
        private int n;
        private bool addtur;
        private bool punctuation;
        private int nov;

        public Dictionary<int, catDictionary> inf_Category
        {
            get { return _cat_dic; }
        }

        

        public classifier(string _langauge, string _stemming, bool _addtur, int _n, bool _punctuation, int _nov)
        {
            _cat_dic = new Dictionary<int, catDictionary>();
            
            _wd = new word_dic();
            language = _langauge;
            stemming = _stemming;
            addtur = _addtur;
            n = _n;
            punctuation = _punctuation;
            nov = _nov;
        }

        public void AddSentenceforTraining(string _sentence, int _category)
        {
            _wd.update_hashtables(_sentence, _category, language, stemming, addtur, n, punctuation, nov);
            foreach (DictionaryEntry _word in _wd._words_in_sentence)
            {
                AddforTarining(_category, (string)_word.Key);  
            }
        }

        

        public void AddforTarining(int _category, string _word)//Uygun kategori bulunup değer ekleniyor.
        {
            if (_cat_dic.ContainsKey(_category))
            {
                catDictionary category_word = (catDictionary)_cat_dic[_category];
                category_word.Add(_word);
                _cat_dic[_category] = category_word;
            }
            else
            {
                catDictionary category_word = new catDictionary(_category);
                category_word.Add(_word);
                _cat_dic.Add(_category, category_word);
            }
        }
    }
}
