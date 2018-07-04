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
    public class arff_converter
    {
        public Hashtable _word_dic;
        public List<Hashtable> _sentence_word;
        public List<int> _sentence_type;

        public Hashtable _words_in_sentence;//bir cümle içindeki kelimeler için

        public arff_converter(int lang_id, int category_id, string langauge, string stemming, bool addtur, int n, bool punctuation, int nov)
        {
            word_dic _wd = new word_dic();
            comment _c = new comment();

            DataTable _dt = _c.select_comment(lang_id.ToString(), category_id.ToString());
            int id = 1;
            foreach (DataRow row in _dt.Rows) // Loop over the rows.
            {
                var type = row[0];
                string _sentence = row[1].ToString().ToLower();
                int _type = Convert.ToInt32(type);

                _wd.update_hashtables(_sentence, _type, langauge, stemming, addtur, n, punctuation, nov);
                id++;
            }

            _word_dic = _wd._word_dic;
            _sentence_word = _wd._sentence_word;
            _sentence_type = _wd._sentence_type;
        }

        public void create_arff_file(string filename, string weighting)
        {
            StreamWriter file = new StreamWriter(filename);
            file.WriteLine("@RELATION comments");
            foreach (DictionaryEntry Item in _word_dic)
            {
                file.WriteLine("@ATTRIBUTE " + (string)Item.Key + " NUMERIC");
            }
            file.WriteLine("@ATTRIBUTE class {-1,0,1,2}");
            file.WriteLine("@data");
            
            for (int i = 0; i < _sentence_word.Count; i++) // Loop through List with for
            {
                string line = "";
                _words_in_sentence = (Hashtable)_sentence_word[i];
                foreach (DictionaryEntry Item in _word_dic)
                {
                    double _score = 0.0;
                    if (weighting == "BINARY")
                        _score = (int)Item.Value;
                    else if (weighting == "TF")
                        _score = (int)Item.Value;//???
                    else if (weighting == "TF-IDF")
                        _score = 0;//??? gerek kalmadı, weka içinden en iyisi seçilip onu kodu yazılacak.

                    string _w = (string)Item.Key;
                    if (_words_in_sentence.ContainsKey(_w))
                        if (line == "")
                            line = _score.ToString();
                        else
                            line += "," + _score.ToString();
                    else
                        if (line == "")
                            line = "0";
                        else
                            line += ",0";
                }
                line += "," + _sentence_type[i].ToString();
                file.WriteLine(line);
            }
            file.Close();
        }

    }
}
