using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace HTMLMarkerClass
{
    public class Review_Inf
    {
        public string inner_main_tag;
        public string sub_outerhtml;
        public string sub_tag;
        public string pub_outerhtml;
        public string pub_tag;
    }

    public static class desicionClass_Review
    {
        public static Review_Inf find_Review_Main_Tag(List<element> _element)
        {
            Review_Inf _ri = new Review_Inf();
            Dictionary<string, int> _dic = new Dictionary<string, int>();

            foreach (element _e in _element)	
            {
                int i = 0;
                if (_e.tagName_Orginal.Contains("class=\"left\""))
                    i = 1;

                if (determineClass(_e) == "1")
                    if (_dic.ContainsKey(_e.tagName_Orginal))
                    {
                        int cnt = (int)_dic[_e.tagName_Orginal];
                        cnt++;
                        _dic[_e.tagName_Orginal] = cnt;
                    }
                    else
                        _dic.Add(_e.tagName_Orginal, 1);
            }

            _ri.inner_main_tag = "";
            int max = 0;
            foreach (var item in _dic)
            {
                if((int)item.Value > max)
                {
                    max = (int)item.Value;
                    _ri.inner_main_tag = item.Key;
                }
            }

            //üst etiketi bulma işlemi
            bool baslangic_etiketi = false;
            _ri.sub_outerhtml = "";
            _ri.sub_tag = "";
            _ri.pub_outerhtml = "";
            _ri.pub_tag = "";

            for (int i = _element.Count-1; i >= 0; i--)
            {
                element _e = (element)_element[i];
                if (!baslangic_etiketi)
                {
                    if (_ri.inner_main_tag== _e.tagName_Orginal && _e.repeat_tag_count > 1)//unique değil
                    {
                        _ri.sub_tag = _e.tagName_Orginal;
                        _ri.sub_outerhtml = _e.outerHTML;
                        baslangic_etiketi = true;
                    }
                }
                else
                {
                    if (_e.outerHTML.Contains(_ri.sub_outerhtml))
                    {
                        if (_e.repeat_tag_count > 1)
                        {
                            _ri.sub_tag = _e.tagName_Orginal;
                            _ri.sub_outerhtml = _e.outerHTML;
                        }
                        else if (_e.repeat_tag_count == 1)
                        {
                            _ri.pub_tag = _e.tagName_Orginal;
                            _ri.pub_outerhtml = _e.outerHTML;
                            break; 
                        }//else if
                    }//içinde var mı
                }//else
            }//for

            return _ri;
        }

        //Determine review text main(1) or not(0)?
        public static string determineClass(HTMLMarkerClass.element d)
        {
            if (d.wordCount_AE <= 26)
                if (d.LinkCount <= 0)
                    if (d.p_count <= 0)
                        if (d.wordCount <= 6)
                            if (d.br_count <= 0)
                                if (d.wordCount <= 2)
                                {
                                    if (d.tagName == "div")
                                        if (d.span_count <= 0)
                                            if (d.wordCount_AE <= 10) return "0";
                                            else
                                                if (d.div_count <= 0)
                                                    if (d.DensityinHTML <= 0.001672) return "0";
                                                    else
                                                        if (d.repeat_tag_count <= 10) return "0";
                                                        else return "1";
                                                else
                                                    if (d.input_count <= 0) return "1";
                                                    else return "0";
                                        else return "0";
                                    if (d.tagName == "td") return "0";
                                    if (d.tagName == "li") return "0";
                                }
                                else
                                    if (d.wordCount_AE <= 5) return "0";
                                    else
                                        if (d.div_count <= 0)
                                        {
                                            if (d.tagName == "div") return "0";
                                            if (d.tagName == "td") return "0";
                                            if (d.tagName == "li")
                                                if (d.repeat_tag_count <= 40)
                                                    if (d.repeat_tag_count <= 24) return "0";
                                                    else
                                                        if (d.span_count <= 0) return "0";
                                                        else return "1";
                                                else return "0";
                                        }
                                        else
                                            if (d.repeat_tag_count <= 45) return "0";
                                            else return "1";
                            else
                                if (d.repeat_tag_count <= 21)
                                {
                                    if (d.tagName == "div") return "0";
                                    if (d.tagName == "td")
                                        if (d.repeat_tag_count <= 7) return "0";
                                        else
                                            if (d.td_count <= 1) return "1";
                                            else return "0";
                                    if (d.tagName == "li") return "0";
                                }
                                else
                                    if (d.repeat_tag_count <= 51)
                                    {
                                        if (d.tagName == "div")
                                            if (d.wordCount_AE <= 5) return "0";
                                            else
                                                if (d.wordCount_AE <= 10) return "1";
                                                else return "0";
                                        if (d.tagName == "td") return "1";
                                        if (d.tagName == "li") return "0";
                                    }
                                    else return "0";
                        else
                            if (d.wordCount_AE <= 9)
                                if (d.br_count <= 4) return "0";
                                else
                                    if (d.repeat_tag_count <= 4) return "0";
                                    else return "1";
                            else
                                if (d.repeat_tag_count <= 60)
                                    if (d.repeat_tag_count <= 2) return "0";
                                    else
                                        if (d.repeat_tag_count <= 57)
                                            if (d.br_count <= 0)
                                                if (d.DensityinHTML <= 0.005959)
                                                    if (d.input_count <= 0)
                                                        if (d.wordCount <= 14)
                                                        {
                                                            if (d.tagName == "div")
                                                                if (d.span_count <= 0)
                                                                    if (d.wordCount_AE <= 18)
                                                                        if (d.dot_count <= 1) return "0";
                                                                        else
                                                                            if (d.repeat_tag_count <= 16) return "1";
                                                                            else return "0";
                                                                    else
                                                                        if (d.repeat_tag_count <= 17) return "1";
                                                                        else return "0";
                                                                else return "0";
                                                            if (d.tagName == "td") return "0";
                                                            if (d.tagName == "li")
                                                                if (d.repeat_tag_count <= 27) return "0";
                                                                else
                                                                    if (d.span_count <= 0) return "0";
                                                                    else return "1";
                                                        }
                                                        else return "0";
                                                    else return "0";
                                                else
                                                    if (d.ul_count <= 0)
                                                        if (d.repeat_tag_count <= 38)
                                                        {
                                                            if (d.tagName == "div")
                                                                if (d.repeat_tag_count <= 20)
                                                                    if (d.repeat_tag_count <= 18)
                                                                        if (d.repeat_tag_count <= 10)
                                                                            if (d.wordCount_AE <= 22)
                                                                                if (d.wordCount <= 10)
                                                                                    if (d.dot_count <= 0) return "0";
                                                                                    else
                                                                                        if (d.DensityinHTML <= 0.010116) return "1";
                                                                                        else return "0";
                                                                                else
                                                                                    if (d.DensityinHTML <= 0.00955)
                                                                                        if (d.repeat_tag_count <= 9) return "1";
                                                                                        else return "0";
                                                                                    else return "1";
                                                                            else
                                                                                if (d.DensityinHTML <= 0.013853) return "0";
                                                                                else return "1";
                                                                        else return "0";
                                                                    else
                                                                        if (d.DensityinHTML <= 0.016807) return "1";
                                                                        else return "0";
                                                                else
                                                                    if (d.wordCount <= 16)
                                                                        if (d.wordCount_AE <= 12)
                                                                            if (d.wordCount <= 7) return "0";
                                                                            else return "1";
                                                                        else return "0";
                                                                    else
                                                                        if (d.span_count <= 0) return "1";
                                                                        else return "0";
                                                            if (d.tagName == "td") return "0";
                                                            if (d.tagName == "li")
                                                                if (d.span_count <= 0) return "0";
                                                                else
                                                                    if (d.wordCount <= 8) return "0";
                                                                    else return "1";
                                                        }
                                                        else return "0";
                                                    else return "0";
                                            else
                                                if (d.input_count <= 0)
                                                    if (d.div_count <= 0)
                                                    {
                                                        if (d.tagName == "div")
                                                            if (d.DensityinHTML <= 0.004876) return "0";
                                                            else return "1";
                                                        if (d.tagName == "td")
                                                            if (d.wordCount_AE <= 15) return "0";
                                                            else return "1";
                                                        if (d.tagName == "li") return "0";
                                                    }
                                                    else return "1";
                                                else return "0";
                                        else return "1";
                                else return "0";
                    else
                        if (d.wordCount_AE <= 2) return "0";
                        else
                            if (d.repeat_tag_count <= 3) return "0";
                            else
                                if (d.DensityinHTML <= 0.001309)
                                    if (d.wordCount_AE <= 14)
                                        if (d.wordCount_AE <= 8)
                                        {
                                            if (d.tagName == "div")
                                                if (d.wordCount_AE <= 5) return "1";
                                                else return "0";
                                            if (d.tagName == "td") return "0";
                                            if (d.tagName == "li") return "0";
                                        }
                                        else return "1";
                                    else return "0";
                                else
                                    if (d.h3_count <= 0)
                                        if (d.br_count <= 0) return "1";
                                        else
                                            if (d.dot_count <= 0) return "0";
                                            else return "1";
                                    else return "0";
                else return "0";
            else
                if (d.wordCount <= 19)
                    if (d.p_count <= 0)
                        if (d.br_count <= 1)
                            if (d.wordCount <= 15) return "0";
                            else
                                if (d.LinkCount <= 0)
                                    if (d.wordCount_AE <= 33)
                                        if (d.repeat_tag_count <= 2) return "0";
                                        else
                                            if (d.span_count <= 0)
                                                if (d.repeat_tag_count <= 60) return "1";
                                                else return "0";
                                            else return "0";
                                    else return "0";
                                else return "0";
                        else
                            if (d.repeat_tag_count <= 15) return "0";
                            else
                                if (d.div_count <= 0) return "0";
                                else return "1";
                    else
                        if (d.meanofWordinLinksAllWords <= 0.550336)
                            if (d.repeat_tag_count <= 2) return "0";
                            else
                                if (d.dot_count <= 0)
                                    if (d.br_count <= 0) return "0";
                                    else
                                        if (d.td_count <= 5) return "1";
                                        else return "0";
                                else
                                    if (d.input_count <= 2)
                                    {
                                        if (d.tagName == "div")
                                            if (d.span_count <= 2)
                                                if (d.wordCount_AE <= 30) return "0";
                                                else return "1";
                                            else
                                                if (d.p_count <= 2) return "0";
                                                else return "1";
                                        if (d.tagName == "td") return "0";
                                        if (d.tagName == "li") return "1";
                                    }
                                    else return "0";
                        else return "0";
                else
                    if (d.repeat_tag_count <= 2)
                        if (d.repeat_tag_count <= 1) return "0";
                        else
                            if (d.LinkCount <= 0)
                                if (d.h2_count <= 0)
                                    if (d.dot_count <= 1) return "0";
                                    else
                                        if (d.wordCount_AE <= 64)
                                            if (d.wordCount <= 42) return "0";
                                            else return "1";
                                        else return "0";
                                else return "1";
                            else return "0";
                    else
                        if (d.input_count <= 0)
                            if (d.h3_count <= 0)
                                if (d.li_count <= 5)
                                    if (d.meanofWordinLinksAllWords <= 0.325581)
                                        if (d.h2_count <= 0)
                                            if (d.repeat_tag_count <= 98)
                                                if (d.repeat_tag_count <= 77)
                                                    if (d.repeat_tag_count <= 60)
                                                        if (d.DensityinHTML <= 0.013154)
                                                            if (d.repeat_tag_count <= 47)
                                                                if (d.p_count <= 0)
                                                                    if (d.span_count <= 0)
                                                                        if (d.repeat_tag_count <= 13)
                                                                            if (d.DensityinHTML <= 0.011923) return "0";
                                                                            else return "1";
                                                                        else
                                                                            if (d.repeat_tag_count <= 41)
                                                                                if (d.wordCount_AE <= 77)
                                                                                    if (d.dot_count <= 1)
                                                                                        if (d.DensityinHTML <= 0.006368) return "0";
                                                                                        else return "1";
                                                                                    else return "1";
                                                                                else return "0";
                                                                            else return "0";
                                                                    else return "0";
                                                                else
                                                                    if (d.div_count <= 2)
                                                                        if (d.LinkCount_AE <= 1)
                                                                            if (d.br_count <= 1) return "1";
                                                                            else
                                                                                if (d.dot_count <= 4) return "0";
                                                                                else return "1";
                                                                        else
                                                                            if (d.wordCount <= 37) return "0";
                                                                            else return "1";
                                                                    else return "0";
                                                            else return "1";
                                                        else
                                                        {
                                                            if (d.tagName == "div")
                                                                if (d.meanofWordinLinksAllWords_AE <= 0.073864)
                                                                    if (d.repeat_tag_count <= 6)
                                                                        if (d.span_count <= 1)
                                                                            if (d.repeat_tag_count <= 5)
                                                                                if (d.div_count <= 1)
                                                                                    if (d.wordCount_AE <= 412)
                                                                                        if (d.meanofWordinLinks <= 1.75)
                                                                                            if (d.p_count <= 2)
                                                                                                if (d.wordCount <= 59) return "1";
                                                                                                else
                                                                                                    if (d.wordCount_AE <= 63) return "0";
                                                                                                    else
                                                                                                        if (d.p_count <= 0) return "1";
                                                                                                        else
                                                                                                            if (d.br_count <= 0) return "1";
                                                                                                            else return "0";
                                                                                            else
                                                                                                if (d.br_count <= 0) return "1";
                                                                                                else return "0";
                                                                                        else
                                                                                            if (d.dot_count <= 10) return "1";
                                                                                            else return "0";
                                                                                    else return "0";
                                                                                else return "1";
                                                                            else
                                                                                if (d.p_count <= 0)
                                                                                    if (d.br_count <= 12) return "1";
                                                                                    else return "0";
                                                                                else
                                                                                    if (d.br_count <= 0) return "0";
                                                                                    else return "1";
                                                                        else
                                                                            if (d.wordCountinLink <= 10)
                                                                                if(d.wordCount_AE <= 8)
                                                                                    return "0";
                                                                                else
                                                                                    return "1";
                                                                            else return "1";
                                                                    else
                                                                        if (d.repeat_tag_count <= 10) return "1";
                                                                        else
                                                                            if (d.wordCount_AE <= 43) return "1";
                                                                            else
                                                                                if (d.repeat_tag_count <= 47)
                                                                                    if (d.meanofWordinLinksAllWords_AE <= 0.046729)
                                                                                        if (d.h4_count <= 0)
                                                                                            if (d.LinkCount <= 1)
                                                                                                if (d.p_count <= 0)
                                                                                                    if (d.span_count <= 4)
                                                                                                        if (d.repeat_tag_count <= 30)
                                                                                                            if (d.span_count <= 1) return "1";
                                                                                                            else
                                                                                                                if (d.span_count <= 3) return "0";
                                                                                                                else return "1";
                                                                                                        else return "0";
                                                                                                    else return "0";
                                                                                                else
                                                                                                    if (d.repeat_tag_count <= 12) return "0";
                                                                                                    else
                                                                                                        if (d.repeat_tag_count <= 24)
                                                                                                            if (d.repeat_tag_count <= 17) return "1";
                                                                                                            else
                                                                                                                if (d.repeat_tag_count <= 22)
                                                                                                                    if (d.repeat_tag_count <= 20) return "0";
                                                                                                                    else return "1";
                                                                                                                else return "0";
                                                                                                        else return "1";
                                                                                            else return "1";
                                                                                        else return "1";
                                                                                    else
                                                                                        if (d.div_count <= 2) return "0";
                                                                                        else return "1";
                                                                                else return "1";
                                                                else
                                                                    if (d.meanofWordinLinks <= 2.365854)
                                                                        if (d.wordCount_AE <= 83) return "0";
                                                                        else return "1";
                                                                    else
                                                                        if (d.repeat_tag_count <= 11)
                                                                            if (d.wordCountinLink_AE <= 5)
                                                                                if (d.wordCount <= 26) return "0";
                                                                                else return "1";
                                                                            else return "1";
                                                                        else
                                                                            if (d.meanofWordinLinks <= 2.909091) return "1";
                                                                            else
                                                                                return "1";
                                                            if (d.tagName == "td")
                                                                if (d.div_count <= 1)
                                                                    if (d.p_count <= 0)
                                                                        if (d.br_count <= 2) return "0";
                                                                        else return "1";
                                                                    else return "1";
                                                                else return "0";
                                                            if (d.tagName == "li")
                                                                if (d.span_count <= 0) return "0";
                                                                else return "1";
                                                        }
                                                    else return "0";
                                                else return "1";
                                            else
                                                if (d.div_count <= 1)
                                                {
                                                    if (d.tagName == "div")
                                                        if (d.dot_count <= 1)
                                                            if (d.repeat_tag_count <= 113) return "1";
                                                            else return "0";
                                                        else return "1";
                                                    if (d.tagName == "td") return "1";
                                                    if (d.tagName == "li") return "0";
                                                }
                                                else return "0";
                                        else
                                            if (d.br_count <= 18) return "0";
                                            else return "1";
                                    else
                                        if (d.meanofWordinLinks <= 7.838462)
                                            if (d.LinkCount <= 3)
                                                if (d.div_count <= 1) return "0";
                                                else return "1";
                                            else
                                                if (d.DensityinHTML <= 0.037313) return "0";
                                                else
                                                    if (d.wordCount_AE <= 155) return "0";
                                                    else return "1";
                                        else
                                        {
                                            if (d.tagName == "div")
                                                if (d.repeat_tag_count <= 17)
                                                    if (d.meanofWordinLinksAllWords_AE <= 0.769231) return "1";
                                                    else return "0";
                                                else return "0";
                                            if (d.tagName == "td") return "0";
                                            if (d.tagName == "li") return "0";
                                        }
                                else
                                    if (d.wordCount_AE <= 273) return "0";
                                    else
                                        if (d.ul_count <= 0) return "1";
                                        else return "0";
                            else
                                if (d.p_count <= 7) return "0";
                                else
                                    if (d.div_count <= 4) return "1";
                                    else return "0";
                        else
                            if (d.wordCount_AE > 7)
                                return "1";
                            else
                                return "0";

            return "0";
        }
    }//class
}//namespace
