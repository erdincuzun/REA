using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using Library_ABE.db;

namespace Library_ABE.text
{

    public class sentence_ext
    {
        public int extract_Sentence_Add_Database(ArrayList _comment_list, string url, string template_id)
        {
            comment _c = new comment();
            int commentno = 1;
            foreach (string _comment in _comment_list)
            {
                MatchCollection mc = Regex.Matches(_comment, @"(\S.+?[.!?])(?=\s+|$)", RegexOptions.IgnoreCase);
                int sentenceno = 1;
                foreach (Match mt in mc)
                {
                    string _s = mt.Groups[0].Value.ToString().Replace("\t","");
                    _s = _s.Replace("\n","");
                    _s = _s.Replace("\r","");
                    _c.Insert_Comment(url, commentno.ToString(), sentenceno.ToString(), _s, "-2", template_id);
                    sentenceno++;
                }                
                commentno++;
            }

            return commentno;
        }
    }
}
