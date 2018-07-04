using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

using Library_ABE.db;
using System.IO;

namespace Library_ABE.text
{
    public class arff_converter2
    {
        public List<string> _sentence_data;

        public arff_converter2(int lang_id, int category_id, string langauge, string stemming, bool addtur, int n, bool punctuation, int nov)
        {
            word_dic _wd = new word_dic();
            comment _c = new comment();
            DataTable _dt = _c.select_comment(lang_id.ToString(), category_id.ToString());

            int olumsuz = 0;
            int olumlu = 0;
            int notr = 0;
            int soru = 0;

            int id = 1;
            foreach (DataRow row in _dt.Rows) // Loop over the rows.
            {
                var type = row[0];
                string _sentence = row[1].ToString().ToLower();
                int _type = Convert.ToInt32(type);

                if (_type == -1)
                {
                    olumsuz++;
                    if (olumsuz < 3300000)
                        _wd.update_hashtables(_sentence, _type, langauge, stemming, addtur, n, punctuation, nov);
                }
                if (_type == 0)
                {
                    notr++;
                    if (notr < 1700000)
                        _wd.update_hashtables(_sentence, _type, langauge, stemming, addtur, n, punctuation, nov);
                }
                if (_type == 1)
                {
                    olumlu++;
                    if (olumlu < 550000000)
                        _wd.update_hashtables(_sentence, _type, langauge, stemming, addtur, n, punctuation, nov);
                }
                if (_type == 2)
                {
                    soru++;
                    if (soru < 100000)
                        _wd.update_hashtables(_sentence, _type, langauge, stemming, addtur, n, punctuation, nov);
                }

                id++;
            }

            _sentence_data = _wd._sentence_data;
        }

        public void create_arff_file(string filename)
        {
            StreamWriter file = new StreamWriter(filename);
            file.WriteLine("@RELATION comments");
            file.WriteLine("@ATTRIBUTE Text string");
            file.WriteLine("@ATTRIBUTE class {-1,0,1,2}");
            file.WriteLine("@data");
            
            for (int i = 0; i < _sentence_data.Count; i++) // Loop through List with for
            {
                file.WriteLine(_sentence_data[i]);
            }
            file.Close();
        }

    }
}
