using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

using Library_ABE.db;
using System.IO;
using HTMLMarkerClass;

namespace Library_ABE.text
{
    public class arff_converter3
    {
        private Dictionary<string, string> _ht_res;

        private string[] deg_real = { "wordCount", "DensityinHTML", "LinkCount", "wordCountinLink", 
                                       "meanofWordinLinks", "meanofWordinLinksAllWords",
                                       "h1_count","h2_count","h3_count","h4_count","h5_count","h6_count",
                                       "p_count", "br_count", "span_count", "object_count", "ul_count", "li_count",
                                       "input_count", "div_count", "td_count", "dot_count",
                                       "wordCount_AE", "DensityinHTML_AE", "LinkCount_AE", "wordCountinLink_AE", 
                                       "meanofWordinLinks_AE", "meanofWordinLinksAllWords_AE",
                                       "h1_count_AE","h2_count_AE","h3_count_AE","h4_count_AE","h5_count_AE","h6_count_AE",
                                       "p_count_AE", "br_count_AE", "span_count_AE", "object_count_AE", "ul_count_AE", "li_count_AE",
                                       "input_count_AE", "div_count_AE", "td_count_AE", "dot_count_AE"};


        public void create_arff_file(string filename, List<element> _list, string template_id)
        {
            rules _r = new rules();
            _ht_res = _r.select_rules_comments(template_id);

            StreamWriter file;
            if (!File.Exists(filename))
            {
                file = new StreamWriter(filename, false);
                file.WriteLine("@relation class_relation");
                foreach (string item in deg_real)
                    file.WriteLine("@attribute '" + item + "' real");
                file.WriteLine("@attribute 'tagName' {div, td, li}");
                file.WriteLine("@attribute 'hastagORID' real");
                file.WriteLine("@attribute 'repeat_tag_count' real");
                file.WriteLine("@ATTRIBUTE class {0,1}");
                file.WriteLine("@data");
                file.Close();
            }

            file = new StreamWriter(filename, true);
            foreach (element _e in _list)
            {
                //int repeat_count = (int)_ht_tag_count[_e.tagName_Orginal];

                string line = _e.wordCount.ToString() + "," + _e.DensityinHTML.ToString() + "," + _e.LinkCount.ToString() + ","
                    + _e.wordCountinLink.ToString() + "," + _e.meanofWordinLinks.ToString() + "," + _e.meanofWordinLinksAllWords.ToString() + ","
                    + _e.h1_count.ToString() + "," + _e.h2_count.ToString() + "," + _e.h3_count.ToString() + ","
                    + _e.h4_count.ToString() + "," + _e.h5_count.ToString() + "," + _e.h6_count.ToString() + ","
                    + _e.p_count.ToString() + "," + _e.br_count.ToString() + "," + _e.span_count.ToString() + ","
                    + _e.object_count.ToString() + "," + _e.ul_count.ToString() + "," + _e.li_count.ToString() + ","
                    + _e.input_count.ToString() + "," + _e.div_count.ToString() + "," + _e.td_count.ToString() + ","
                    + _e.dot_count.ToString() + "," + 
                    _e.wordCount_AE.ToString() + "," + _e.DensityinHTML_AE.ToString() + "," + _e.LinkCount_AE.ToString() + ","
                    + _e.wordCountinLink_AE.ToString() + "," + _e.meanofWordinLinks_AE.ToString() + "," + _e.meanofWordinLinksAllWords_AE.ToString() + ","
                    + _e.h1_count_AE.ToString() + "," + _e.h2_count_AE.ToString() + "," + _e.h3_count_AE.ToString() + ","
                    + _e.h4_count_AE.ToString() + "," + _e.h5_count_AE.ToString() + "," + _e.h6_count_AE.ToString() + ","
                    + _e.p_count_AE.ToString() + "," + _e.br_count_AE.ToString() + "," + _e.span_count_AE.ToString() + ","
                    + _e.object_count_AE.ToString() + "," + _e.ul_count_AE.ToString() + "," + _e.li_count_AE.ToString() + ","
                    + _e.dot_count_AE.ToString() + ","
                    + _e.input_count_AE.ToString() + "," + _e.div_count_AE.ToString() + "," + _e.td_count_AE.ToString() + ","
                    + _e.tagName + "," + _e.tag_idORclass + "," + _e.repeat_tag_count.ToString();

                string _cls = "0";
                if (_ht_res.ContainsKey(_e.tagName_Orginal))
                    _cls = "1";
                
                file.WriteLine(line+","+_cls);
            }
            file.Close();
        }

    }
}
