using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

using Library_ABE.text;
using Library_ABE.db;

namespace Library_ABE.BayesClassifier
{
    public class prediction
    {
        private Dictionary<int, catDictionary> _cat_dic;
        private Dictionary<int, int> _category_sentence_count;
        private int _all_sentence_count;

        private string language;
        private string stemming;
        private int n;
        private bool addtur;
        private bool punctuation;
        private int nov;

        private Dictionary<int, double> _cat_score;

        public Dictionary<int, catDictionary> getCategoryDic
        {
            get { return _cat_dic; }
        }

        public Dictionary<int, double> Category_Score
        {
            get { return _cat_score; }
        }

        public prediction()
        {
            _cat_dic = null;
        }

        public prediction(Dictionary<int, catDictionary> cat_dic, Dictionary<int, int> category_sentence_count, int all_sentence_count,  string _langauge, string _stemming, bool _addtur, int _n, bool _punctuation, int _nov)
        {
            _cat_dic = cat_dic;
            _category_sentence_count = category_sentence_count;
            _all_sentence_count = all_sentence_count;
            language = _langauge;
            stemming = _stemming;
            addtur = _addtur;
            n = _n;
            nov = _nov;
            punctuation = _punctuation;
        }

        public void CalculateScore(string _sentence)
        {
            _cat_score = new Dictionary<int, double>();

            word_dic _new_wd = new word_dic();
            _new_wd.update_hashtables(_sentence, 0, language, stemming, addtur, n, punctuation, nov);

            foreach (DictionaryEntry _word in _new_wd._words_in_sentence)
            {
                foreach (var _cat in _cat_dic)
                {
                    catDictionary _cd = (catDictionary)_cat.Value;
                    double _countofword = Convert.ToDouble(_cd.Search((string)_word.Key));
                    double _totalWords = Convert.ToDouble(_cd.CountofWordsinCategory);
                    double _countofsentenceincat = (int)_category_sentence_count[(int)_cat.Key];
                    
                    double _score = 0;
                    if (_countofword > 0)
                        _score = System.Math.Log(((double)_countofword + 1) / ((double)_totalWords + _all_sentence_count));
                    else
                        _score = 0;

                    if (!_cat_score.ContainsKey((int)_cat.Key))
                        _cat_score.Add((int)_cat.Key, _score + System.Math.Log((double)_countofsentenceincat / (double)_all_sentence_count));
                    else
                        _cat_score[(int)_cat.Key] = (double)_cat_score[(int)_cat.Key] + _score;
                }
            }
        }

        public int Estimate_Category(string _sentence)
        {
            int _res = -3;
            double _enkucuk = 0;
            CalculateScore(_sentence);//tüm kategoriler için skorlar hesaplanıyor.
            foreach (var _cs in _cat_score)
            {
                if (_cs.Value != 0)
                {
                    if (_enkucuk == 0)
                    {
                        _enkucuk = (double)_cs.Value;
                        _res = (int)_cs.Key;
                    }
                    else
                    {
                        if (_enkucuk > _cs.Value)//en küçük olan alınacak.
                        {
                            _enkucuk = (double)_cs.Value;
                            _res = (int)_cs.Key;
                        }
                    }
                }
            }

            return _res;
        }

        public int[,] ConfusionMatrix(int lang_id, int category_id)
        {
            int[,] _cm = new int[_cat_dic.Count, _cat_dic.Count];
            for (int x = 0; x < _cat_dic.Count; x++)
                for (int y = 0; y < _cat_dic.Count; y++)
                    _cm[x, y] = 0;

            Hashtable _categoriler = new Hashtable();
            int i =0;
            foreach (var _cat in _cat_dic)
            {
                _categoriler.Add((int)_cat.Key, i);
                i++;
            }

            word_dic _wd = new word_dic();
            comment _c = new comment();
            DataTable _dt = _c.select_comment(lang_id.ToString(), category_id.ToString());

            int id = 1;
            foreach (DataRow row in _dt.Rows) // Loop over the rows.
            {
                var type = row[0];
                string _sentence = row[1].ToString().ToLower();
                int _type = Convert.ToInt32(type);
                int x = (int)_categoriler[_type];
                int y = (int)_categoriler[Estimate_Category(_sentence)];
                _cm[x, y]++;
                id++;
            }
            
            return _cm;
        }
    }
}
