using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Library_ABE.db;
using Library_ABE.text;
using System.IO;

namespace Library_ABE.BayesClassifier
{
    public class training
    {
        private classifier _cr;
        private Dictionary<int, int> _category_sentence_count;
        private int all_sentence_count;

        public training()
        {
            _cr = null;
            _category_sentence_count = null;
            all_sentence_count = 0;
        }

        public training(int lang_id, int category_id, string langauge, string stemming, bool addtur, int n, bool punctuation, int nov)
        {
            _cr = new classifier(langauge, stemming, addtur, n, punctuation, nov);//classifier hazır
            _category_sentence_count = new Dictionary<int, int>();
            all_sentence_count = 0;

            word_dic _wd = new word_dic();
            comment _c = new comment();

            DataTable _dt = _c.select_comment(lang_id.ToString(), category_id.ToString());
            
            int id = 1;
            int yuzde50 = _dt.Rows.Count / 2;
            foreach (DataRow row in _dt.Rows) // Loop over the rows.
            {
                var type = row[0];
                string _sentence = row[1].ToString().ToLower();
                int _type = Convert.ToInt32(type);

                _cr.AddSentenceforTraining(_sentence, _type);
                AddASentence(_type);//kategorideki cümle sayısı
                all_sentence_count++;
                id++;
                if (id == yuzde50)
                    break;
            }
        }

        public void AddASentence(int _category)
        {
            if (_category_sentence_count.ContainsKey(_category))
            {
                int _cnt = (int)_category_sentence_count[_category];
                _category_sentence_count[_category] = _cnt;
            }
            else
            {
                _category_sentence_count.Add(_category, 1);
            }
        }

        public Dictionary<int, catDictionary> _cat_dic
        {
            get { return _cr.inf_Category; }
        }

        public classifier BayesianClassifier
        {
            get { return _cr; }
        }

        public Dictionary<int, int> Sentence_Count_In_Category
        {
            get { return _category_sentence_count; }
        }

        public int Sentence_Count
        {
            get { return all_sentence_count; }
        }
    }
}
