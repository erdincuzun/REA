using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace HTMLMarkerClass
{
    //features that is used in machine learning
    public class element
    {
        public int id;
        public string xPath;
        public int elementlinked_id;

        public string elementName;
        public string parent_elementName;
        public string tagName;
        public string tagName_Orginal;

        public string tag_id_Name;
        public string tag_class_Name;
        public int tag_id;
        public int tag_class;
        public int tag_idORclass;

        public string outerHTML;
        public string innerHTML;

        public string BagofWords;

        public int wordCount;
        public double DensityinHTML;
        public int LinkCount;
        public int wordCountinLink;
        public double meanofWordinLinks;
        public double meanofWordinLinksAllWords;
        public int dot_count;
        public int h1_count;
        public int h2_count;
        public int h3_count;
        public int h4_count;
        public int h5_count;
        public int h6_count;
        public int img_count;
        public int p_count;
        public int br_count;
        public int span_count;
        public int object_count;
        public int ul_count;
        public int li_count;
        public int input_count;
        public int div_count;
        public int td_count;

        //etiketin tüm body içinde tekrar sayısı
        public int repeat_tag_count;

        //wordCountinLink/LinkCount
        public string outerHTML_AE;//After Extraction(AE) nested tags
        public string innerHTML_AE;
        public string BagofWords_AE;

        public int wordCount_AE;
        public double DensityinHTML_AE;
        public int LinkCount_AE;
        public int wordCountinLink_AE;
        public double meanofWordinLinks_AE;
        public double meanofWordinLinksAllWords_AE;
        public int dot_count_AE;
        public int h1_count_AE;
        public int h2_count_AE;
        public int h3_count_AE;
        public int h4_count_AE;
        public int h5_count_AE;
        public int h6_count_AE;
        public int img_count_AE;
        public int p_count_AE;
        public int br_count_AE;
        public int span_count_AE;
        public int object_count_AE;
        public int ul_count_AE;
        public int li_count_AE;
        public int input_count_AE;
        public int div_count_AE;
        public int td_count_AE;       

        //similarity between layouts
        public double sim_bagofword;
        public double sim_bagofword_AE;
        public double sim_innerHTML;
        public double sim_innerHTML_AE;
    }

    //DOM and feature extraction
    public class DOM
    {
        public string domhtmlContent;
        public string savehtmlContent;
        public string resulthmtlContent;
        public string all_words;
        public List<element> _list;
        public Dictionary<string, int> _ht_tag_count;

        public void prepareDOM(string htmlContent2)
        {
            string htmlContent = htmlContent2;
            htmlContent = HTML.trim_commenttags(htmlContent);
            htmlContent = HTML.trimOptions(htmlContent);
            htmlContent = HTML.trimScript(htmlContent);
            htmlContent = HTML.trim_HREF_SCR(htmlContent);
            htmlContent = HTML.trim_some_cases(htmlContent);
            //for fast processing otherwise image, link, javascript loading...

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);
            _list = new List<element>();
            _ht_tag_count = new Dictionary<string, int>();
            
            HtmlAgilityPack.HtmlNode body = htmlDocument.DocumentNode.SelectSingleNode("//body");
            element _firstelement = AnalyzeGivenHTML(body.InnerHtml.Replace("\r\n", "").Trim(), body.InnerText.Replace("\r\n", "").Trim());

            AnalyzeProcess(htmlDocument, _firstelement);
            //AnalyzeProcess(htmlDocument, "//li", _firstelement);
            //AnalyzeProcess(htmlDocument, "//td", _firstelement);

            //count aktarılıyor.
            for (int i = 0; i < _list.Count; i++)
			{
                element _e = (element)_list[i];
                _e.repeat_tag_count = (int)_ht_tag_count[_e.tagName_Orginal];
                _list[i] = _e;
			}
        }

        public void AnalyzeProcess(HtmlAgilityPack.HtmlDocument htmlDocument, element _firstelement)
        {
            all_words = _firstelement.BagofWords;
            try
            {
                int i = 0;
                foreach (HtmlAgilityPack.HtmlNode node in htmlDocument.DocumentNode.Descendants())
                {
                    if (node.Name == "div" || node.Name == "td" || node.Name == "li")
                    {
                        string innerText = node.InnerText.Replace("\r\n", " ").Trim();
                        if (innerText != "")
                        {
                            element _element = new element();
                            _element.outerHTML = node.OuterHtml.Replace("\r\n", " ").Trim();
                            _element.innerHTML = node.InnerHtml.Replace("\r\n", " ").Trim();
                            _element.id = i;
                            i++;

                            int _start = _element.outerHTML.IndexOf('<');
                            int _end = _element.outerHTML.IndexOf('>');
                            string temp_to = _element.outerHTML.Substring(_start, _end - _start + 1).Trim();
                            _element.tagName_Orginal = webfilter.String_Decimal_Clear(temp_to);
                            //tekrar sayısı hesaplanıyor
                            if (_ht_tag_count.ContainsKey(_element.tagName_Orginal))
                            {
                                int _cnt = (int)_ht_tag_count[_element.tagName_Orginal];
                                _cnt++;
                                _ht_tag_count[_element.tagName_Orginal] = _cnt;
                            }
                            else
                                _ht_tag_count.Add(_element.tagName_Orginal, 1);

                            _element.tagName = node.OriginalName;
                            _element.xPath = node.XPath;
                            element _tempelement = AnalyzeGivenHTML(_element.outerHTML, innerText);
                            _element.BagofWords = _tempelement.BagofWords;
                            _element.wordCount = _tempelement.wordCount;
                            _element.DensityinHTML = (double)_element.wordCount / _firstelement.wordCount;
                            _element.LinkCount = _tempelement.LinkCount;
                            _element.wordCountinLink = _tempelement.wordCountinLink;
                            _element.meanofWordinLinks = _tempelement.meanofWordinLinks;
                            _element.meanofWordinLinksAllWords = _tempelement.meanofWordinLinksAllWords;
                            string temp_innerhtml_ = _element.innerHTML.ToUpper(new CultureInfo("en-US", false));//for english words thus html tags
                            _element.dot_count = webfilter.CountStringOccurrences(temp_innerhtml_, ".");
                            _element.h1_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<H1");
                            _element.h2_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<H2");
                            _element.h3_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<H3");
                            _element.h4_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<H4");
                            _element.h5_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<H5");
                            _element.h6_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<H6");
                            _element.img_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<IMG");
                            _element.p_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<P");
                            _element.br_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<BR");
                            _element.span_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<SPAN");
                            _element.object_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<OBJECT");
                            _element.ul_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<UL");
                            _element.li_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<LI");
                            _element.input_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<INPUT")
                                + webfilter.CountStringOccurrences(temp_innerhtml_, "<BUTTON")
                                + webfilter.CountStringOccurrences(temp_innerhtml_, "<LABEL");
                            _element.div_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<DIV");
                            _element.td_count = webfilter.CountStringOccurrences(temp_innerhtml_, "<TD");

                            string[] _sonuclar = ExtractionofSubLayouts(node);
                            string tempinner_text = _sonuclar[0];
                            string tempOuterHTML = _sonuclar[1];
                            string tempinnerHTML = _sonuclar[2];

                            _element.outerHTML_AE = tempinnerHTML.Trim();
                            _element.innerHTML_AE = tempOuterHTML.Trim();
                            _element.BagofWords_AE = tempinner_text.Trim();
                            //After Extraction
                            if (tempinner_text.Trim() != "")
                                AnalyzeGivenHTML_AE(_element.outerHTML_AE, _element.innerHTML_AE, ref _element);

                            _list.Add(_element);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public string[] ExtractionofSubLayouts(HtmlAgilityPack.HtmlNode node)
        {
            string[] _sonuclar = new string[4];
            string tempinner_text = node.InnerText.Replace("\r\n", "").Trim();

            if (tempinner_text != "")
            {
                string tempOuterHTML = node.OuterHtml.Replace("\r\n", "").Trim();
                string tempinnerHTML = node.InnerHtml.Replace("\r\n", "").Trim();


                foreach (HtmlAgilityPack.HtmlNode node_Sub in node.ChildNodes)
                {
                    if (node_Sub.InnerText.Trim() != "")
                    {
                            if (node_Sub.Name == "div"
                                || node_Sub.Name == "table" || node_Sub.Name == "tbody"
                                || node_Sub.Name == "form"|| node_Sub.Name == "ul" || node_Sub.Name == "ol")//
                            {
                                if (tempOuterHTML != "")
                                {
                                    //Clear child tags from bag of words
                                    //Replace function clear all possible words so we write this algorithm
                                    tempinner_text = StripOnlyFirstData(tempinner_text, node_Sub.InnerText.Replace("\r\n", "").Trim());
                                    tempOuterHTML = StripOnlyFirstData(tempOuterHTML, node_Sub.OuterHtml.Replace("\r\n", "").Trim());
                                    tempinnerHTML = StripOnlyFirstData(tempinnerHTML, node_Sub.InnerHtml.Replace("\r\n", "").Trim());
                                }//
                            }//IF DIV TABLE ...
                    }
                }//childrens

                _sonuclar[0] = tempinner_text;
                _sonuclar[1] = tempOuterHTML;
                _sonuclar[2] = tempinnerHTML;
            }
            else
            {
                _sonuclar[0] = "";
                _sonuclar[1] = "";
                _sonuclar[2] = "";
            }

            return _sonuclar;
        }

        public string clear_illegal_characters_for_XML(string strOutput)
        {
            strOutput = strOutput.Replace("<", " ");
            strOutput = strOutput.Replace(">", " ");
            strOutput = strOutput.Replace("\"", " ");
            strOutput = strOutput.Replace("&", " ");
            strOutput = strOutput.Replace("€", " ");
            strOutput = strOutput.Replace("�", " ");
            strOutput = strOutput.Replace("|", " ");

            return strOutput;
        }        
        
        //prepare information for a given hmtl
        public element AnalyzeGivenHTML(string html_content, string inner_text)
        {
            //html_content = RemoveScripts(html_content);
            element _element = new element();
            _element.BagofWords = inner_text;
            _element.wordCount = HTML.WordsCountGivenText(_element.BagofWords);

            string pattern = "href=.*?>(.*?)</a";
            Regex exp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

            MatchCollection matchList = exp.Matches(html_content);
            string[] _list = new string[matchList.Count];
            string URL_INNER = "";
            for (int i = 0; i < matchList.Count; i++)
            {
                Match match = matchList[i];
                if (match.Value.Length > 0)
                {                    
                    URL_INNER = URL_INNER + " " + HTML.stripHtml(match.Groups[1].Value);
                }
            }

            int count_link = webfilter.CountStringOccurrences(html_content, "<a ");
            count_link = count_link + webfilter.CountStringOccurrences(html_content, "<A ");
            count_link = count_link + webfilter.CountStringOccurrences(html_content, "onclick="); //interesting javascript with link

            _element.LinkCount = count_link;
            _element.wordCountinLink = HTML.WordsCountGivenText(URL_INNER);
            if (_element.LinkCount != 0)
                _element.meanofWordinLinks = (double)_element.wordCountinLink / _element.LinkCount;
            else
                _element.meanofWordinLinks = 0;

            if (_element.wordCount != 0)
                _element.meanofWordinLinksAllWords = (double)_element.wordCountinLink / _element.wordCount;
            else
                _element.meanofWordinLinksAllWords = 0;

            return _element;
        }

        //prepare information for a given hmtl
        public element AnalyzeGivenHTML_AE(string html_content, string inner_text, ref element _element)
        {
            _element.BagofWords_AE = inner_text;
            _element.wordCount_AE = HTML.WordsCountGivenText(_element.BagofWords_AE);

            string pattern = "href=.*?>(.*?)</a";
            Regex exp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

            MatchCollection matchList = exp.Matches(html_content);
            string[] _list = new string[matchList.Count];
            string URL_INNER = "";
            for (int i = 0; i < matchList.Count; i++)
            {
                Match match = matchList[i];
                if (match.Value.Length > 0)
                {
                    URL_INNER = URL_INNER + " " + HTML.stripHtml(match.Groups[1].Value);
                }
            }

            _element.LinkCount_AE = matchList.Count;
            _element.wordCountinLink_AE = HTML.WordsCountGivenText(URL_INNER);
            if (_element.LinkCount_AE != 0)
                _element.meanofWordinLinks_AE = (double)_element.wordCountinLink_AE / _element.LinkCount_AE;
            else
                _element.meanofWordinLinks_AE = 0;

            if (_element.wordCount_AE != 0)
                _element.meanofWordinLinksAllWords_AE = (double)_element.wordCountinLink_AE / _element.wordCount_AE;
            else
                _element.meanofWordinLinksAllWords = 0;

            return _element;
        }

        private string StripOnlyFirstData(string _htmlcontent, string extracted_data) 
        {
            int pos = _htmlcontent.IndexOf(extracted_data);

            if (pos >= 0)
            {
                string baslangic = "";
                string son = "";
                if (pos != 0)
                {
                    baslangic = _htmlcontent.Substring(0, pos);
                }

                if (extracted_data.Length + pos <= _htmlcontent.Length)
                    son = _htmlcontent.Substring(extracted_data.Length + pos, _htmlcontent.Length - extracted_data.Length - pos);

              _htmlcontent = baslangic + " " + son;
            }

            return _htmlcontent;
        }


        /*public ArrayList fingTAGibHTML(String htmlContent, String tagName, string filename)
        {
            DateTime _now = DateTime.Now;

            string id_name = "";
            if (tagName.Contains("id="))
                id_name = findElementName(tagName, "id=\"(.*?)\"");
            if (tagName.Contains("ID="))
                id_name = findElementName(tagName, "ID=\"(.*?)\"");
            string class_name = "";
            if (tagName.Contains("class="))
                class_name = findElementName(tagName, "class=\"(.*?)\"");

            // Obtain the document interface
            //IHTMLDocument2 htmlDocument = (IHTMLDocument2)new mshtml.HTMLDocument();
            IHTMLDocument2 htmlDocument = new mshtml.HTMLDocumentClass();
            // Construct the document
            htmlDocument.write(htmlContent);
            // Extract all image elements
            // IHTMLElementCollection imgElements = htmlDocument.images;
            IHTMLElementCollection allElements = htmlDocument.all;
            ArrayList sonuc = new ArrayList();
            // Iterate all the elements and display tag names

            int elementsize = 0;
            int elementcnt = 0;
            foreach (IHTMLElement element in allElements)
            {
                string cn = "";
                if (element.className != null)
                    cn = element.className;
                string id = "";
                if (element.id != null)
                    id = element.id;

                if (element.tagName == "DIV" && cn == class_name && id == id_name)
                    sonuc.Add(element.innerText);

                if (element.innerHTML != null)
                    elementsize += element.innerHTML.Length;


                elementcnt++;
            }

            return sonuc;
        }*/

        //find patterns in html
        private string findElementName(string tagname, string pattern)
        {
            Regex exp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

            MatchCollection matchList = exp.Matches(tagname);

            Match match = matchList[0];
            string _str = match.Groups[1].Value;

            return _str;
        }
        
    }//class DOM

    //HTML Processing
    public class HTML
    {
        //Words count in given text
        public static int WordsCountGivenText(string words)
        {
            words = words.Replace(":", " ");
            words = words.Replace("(", " ");
            words = words.Replace(")", " ");
            words = words.Replace("?", " ");
            words = words.Replace("*", " ");
            words = words.Replace("-", " ");
            words = words.Replace("/", " ");
            words = words.Replace("!", " ");
            words = words.Replace(".", " ");
            // COMPRESS ALL WHITESPACE into a single space, seperating words
            if (words != null)
            {
                if (words.Length > 0)
                {
                    Regex r = new Regex(@"\s+");            //remove all whitespace
                    string compressed = r.Replace(words, " ");
                    return compressed.Split(' ').Length;
                }
                else
                {
                    return 0;
                }
            }
            else
                return 0;
        }
        //trim javascript
        public static string trimScript(string htmlDocText)
        {
            string bodyText = "";

            //garip bir durum. javascript içinde javascript var write komutu içinde... o yüzden bu satır...
            string trimJavascript = "document.write(.*?)";
            Regex regexTrimJs = new Regex(trimJavascript, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            bodyText = regexTrimJs.Replace(htmlDocText, "");

            trimJavascript = @"<SCR.PT(?:\s+[^>]*)?>.*?</SCR.PT\s*>";
            regexTrimJs = new Regex(trimJavascript, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            bodyText = regexTrimJs.Replace(htmlDocText, "");
            return bodyText;
        }
        //trim javascript
        public static string trim_HREF_SCR(string htmlDocText)
        {
            string bodyText = "";
            string trim_str = "href=\".*?\"";
            Regex regexTrimJs = new Regex(trim_str, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            bodyText = regexTrimJs.Replace(htmlDocText, "href=\"\"");

            trim_str = "src=\".*?\"";
            regexTrimJs = new Regex(trim_str, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            bodyText = regexTrimJs.Replace(bodyText, "src=\"\"");

            return bodyText;
        }

        public static string trim_commenttags(string htmlDocText)
        {
            string trim_str = "<!--.*?-->";
            Regex regexTrimJs = new Regex(trim_str, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            htmlDocText = regexTrimJs.Replace(htmlDocText, "");

            return htmlDocText;
        }

                    

        //trim javascript
        public static string trim_some_cases(string htmlDocText)
        {
            string bodyText = "";
            string trim_str = "onload=\".*?\"";
            Regex regexTrimJs = new Regex(trim_str, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            bodyText = regexTrimJs.Replace(htmlDocText, "");

            trim_str = "onerror=\".*?\"";
            regexTrimJs = new Regex(trim_str, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            bodyText = regexTrimJs.Replace(bodyText, "");

            trim_str = @"<.FRAME(?:\s+[^>]*)?>.*?<.*?/.FRAME\s*>";
            regexTrimJs = new Regex(trim_str, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            bodyText = regexTrimJs.Replace(bodyText, "");

            //walesonline.co.uknews özel bir durum
            trim_str = "<tm:contentobject.*?>";
            regexTrimJs = new Regex(trim_str, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            bodyText = regexTrimJs.Replace(bodyText, "");

            trim_str = "<.tm:contentobject.*?>";
            regexTrimJs = new Regex(trim_str, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            bodyText = regexTrimJs.Replace(bodyText, "");

            return bodyText;
        }

        //trim td
        public static string trimTD(string htmlDocText)
        {
            //nested 
            ArrayList _divlist = findgiventag(htmlDocText, "<TD.*?>");
            foreach (string item in _divlist)
            {
                string tag = item;
                tag = tag.Replace(":", ".");
                tag = tag.Replace("(", ".");
                tag = tag.Replace(")", ".");
                tag = tag.Replace("?", ".");
                tag = tag.Replace("*", ".");
                tag = tag.Replace("-", ".");
                tag = tag.Replace("/", ".");
                tag = tag.Replace("!", ".");
                tag = tag.Replace(" ", ".?");
                tag = tag.Replace(";", ".");
                if (htmlDocText.Contains(item))
                {
                    string[] _divstr = webfilter.Contents_of_givenLayout_Tags_TESTER(htmlDocText, tag, false);
                    if (_divstr != null)
                        foreach (string _d in _divstr)
                        {
                            if(_d != null)
                                if(_d != "")
                            htmlDocText = htmlDocText.Replace(_d, "");
                        }
                }

            }

            return htmlDocText;
        } 
        //trim javascript
        public static string trimDIV(string htmlDocText)
        {
            //nested 
            ArrayList _divlist = findgiventag(htmlDocText, "<D[I,i]V.*?>");
            foreach (string item in _divlist)
            {
               string tag = item;
               tag = tag.Replace(":", ".");
               tag = tag.Replace("(", ".");
               tag = tag.Replace(")", ".");
               tag = tag.Replace("?", ".");
               tag = tag.Replace("*", ".");
               tag = tag.Replace("-", ".");
               tag = tag.Replace("/", ".");
               tag = tag.Replace("!", ".");
               tag = tag.Replace(" ", ".?");
               tag = tag.Replace(";", ".");
               if (htmlDocText.Contains(item))
               {
                   string[] _divstr = webfilter.Contents_of_givenLayout_Tags_TESTER(htmlDocText, tag, false);
                   if (_divstr != null)
                       foreach (string _d in _divstr)
                       {
                           if (_d != null)
                               if (_d != "")
                           htmlDocText = htmlDocText.Replace(_d,"");
                       }
               }
                                
            }

            return htmlDocText;
        }

        public static ArrayList findgiventag(string html_content, string pattern)
        {
            ArrayList _al = new ArrayList();
            if (html_content != null)
            {
                Regex exp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

                MatchCollection matchList = exp.Matches(html_content);

                for (int i = 0; i < matchList.Count; i++)
                {
                    Match match = matchList[i];
                    string _str = match.Groups[0].Value;
                    _al.Add(_str);
                }

            }
            return _al;
        }
        //trim options because it is negative effects on calculation of count
        public static string trimOptions(string words)
        {
            // COMPRESS ALL WHITESPACE into a single space, seperating words
            if (words != null)
            {
                if (words.Length > 0)
                {
                    Regex r = new Regex(@"<STYLE(?:\s+[^>]*)?>.*?</STYLE\s*>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);                 //style
                    string compressed = r.Replace(words, " ");
                    r = new Regex(@"<select(?:\s+[^>]*)?>.*?</select\s*>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);                 //select
                    compressed = r.Replace(compressed, " ");
                    return compressed;
                }
                else
                {
                    return "";
                }
            }
            else
                return "";
        }
        //remove html tags
        public static string stripHtml(string strOutput)
        {
            //Strips the HTML tags from strHTML
            if (strOutput != null)
            {
                /*if (strOutput.Contains("<script") || strOutput.Contains("<SCRIPT") || strOutput.Contains("<Script"))
                    strOutput = "xxx";*/
                strOutput = trimScript(strOutput);
                strOutput = trimDIV(strOutput);
                strOutput = trimTD(strOutput);

                System.Text.RegularExpressions.Regex _regex = new System.Text.RegularExpressions.Regex("<(.|\n)+?>");
                strOutput = _regex.Replace(strOutput, " ");
                strOutput = clear_illegal_characters_for_HTML(strOutput);
            }
            return strOutput;
        }
        //clear illegal characters
        public static string clear_illegal_characters_for_HTML(string strOutput)
        {
            strOutput = strOutput.Replace("&quot;", " ");
            strOutput = strOutput.Replace("&#39;", " ");
            strOutput = strOutput.Replace("\n", " ");
            strOutput = strOutput.Replace("\r", " ");
            strOutput = strOutput.Replace("\t", " ");
            strOutput = strOutput.Replace("&nbsp;", " ");
            strOutput = strOutput.Replace("\"", " ");
            strOutput = strOutput.Replace("\\", " ");
            strOutput = strOutput.Replace("`", "");
            strOutput = strOutput.Replace("’", "");
            strOutput = strOutput.Replace("<", " ");
            strOutput = strOutput.Replace(">", " ");
            strOutput = strOutput.Replace("|", " ");
            strOutput = strOutput.Replace("'", "");
            strOutput = strOutput.Replace(",", " ");
            strOutput = strOutput.Replace("?", " ");
            strOutput = strOutput.Replace("!", " ");
            strOutput = strOutput.Replace(".", " ");
            strOutput = strOutput.Replace("*", " ");
            strOutput = strOutput.Replace("-", " ");
            strOutput = strOutput.Replace("•", " ");
            strOutput = strOutput.Replace(":", " ");
            strOutput = strOutput.Replace("/", " ");
            strOutput = strOutput.Replace(";", " ");
            strOutput = strOutput.Replace("#", " ");
            strOutput = strOutput.Replace("(", " ");
            strOutput = strOutput.Replace(")", " ");
            strOutput = strOutput.Replace("$", " ");
            strOutput = strOutput.Replace("%", " ");
            strOutput = strOutput.Replace("&", " ");
            strOutput = strOutput.Replace("{", " ");
            strOutput = strOutput.Replace("}", " ");
            strOutput = strOutput.Replace("=", " ");
            strOutput = strOutput.Replace("]", " ");
            strOutput = strOutput.Replace("[", " ");
            strOutput = strOutput.Replace("*", " ");
            strOutput = strOutput.Replace("_", " ");
            strOutput = strOutput.Replace("-", " ");
            strOutput = strOutput.Replace("£", " ");
            strOutput = strOutput.Replace("é", " ");
            strOutput = strOutput.Replace("½", " ");
            strOutput = strOutput.Replace("~", " ");
            strOutput = strOutput.Replace("“", " ");
            strOutput = strOutput.Replace("»", " ");
            strOutput = strOutput.Replace("+", " ");
            strOutput = strOutput.Replace("‘", " ");
            strOutput = strOutput.Replace("@", " ");

            Regex _regex = new Regex(@"\s+", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

            strOutput = _regex.Replace(strOutput, " ");

            return strOutput;
        }
    }//html class

    //efficient content extractiom module
    public class webfilter
    {
        //word count for a given word in a given text
        private static int CountofStartingTag(string Text, string tag)
        {
            int words;
            Regex reg = new Regex(@tag);
            MatchCollection mc = reg.Matches(Text);
            if (mc.Count > 0)
                words = mc.Count;
            else
                words = 0;

            return words;
        }

        //end tag for a given tag
        private static string find_EndTag(string _tag)
        {
            int end = _tag.IndexOf(" ");
            if(end==-1)
                end = _tag.IndexOf(">");

            string _result = _tag;
            if (end != -1)
            {
                _result = _tag.Substring(0, end) + ">";
                _result = _result.Replace("<", "</");
            }

            return _result;
        }

        //end tag for a given tag
        private static string find_StartTag(string _tag)
        {
            int end = _tag.IndexOf(" ");
            if (end == -1)
                end = _tag.IndexOf(">");

            string _result = _tag;
            if (end != -1)
            {
                _result = _tag.Substring(0, end);
            }

            return _result;
        }

        //Tag'a ait bilgileri getiren fonksiyon
        //birden fazla sonuç varsa gösterebiliyor
        //nested özelliği regular expression sağlanamıyor. Bu fonksiyon sayesinde nested tapıda çözümleniyor.
        private static string[] GrabbingofHTMLTags(string _text, string _tag, int countofTag)
        {
            string[] _resultArray = new string[countofTag];

            string _endtag = find_EndTag(_tag);
            string _starttag = find_StartTag(_tag);

            int k = 0;
            for (int i = 0; i < countofTag; i++)
            {
                k = _text.IndexOf(_tag, k);
                if (k == -1) break;
                string str1 = "";
                if (k != -1)
                {
                    string str2 = _text.Substring(k);
                    str1 = _text.Substring(k);

                    k = k + _tag.Length;
                    //div içini ararken en son nerede kaldığımızı tutan etiket.
                    int l = str2.IndexOf(_endtag);
                    if (l != -1)
                        str1 = str2.Substring(0, l + _endtag.Length);
                    else
                        str1 = str2;

                    int start_position = l + _endtag.Length;
                    while (CountofStartingTag(str1, _starttag) != CountofStartingTag(str1, _endtag))
                    {
                        l = str2.IndexOf(_endtag, start_position);

                        if (l == -1)
                            break;

                        str1 = str2.Substring(0, l + _endtag.Length);

                        start_position = l + _endtag.Length;
                    }
                }//if 1
                else
                    str1 = "";

                //extract only content
                if (str1 != "")
                {
                    if (str1.Length - _tag.Length > 0)
                    {
                        str1 = str1.Substring(_tag.Length, str1.Length - _tag.Length - _endtag.Length);
                        str1 = str1.Trim();
                        _resultArray[i] = str1;
                    }
                }
            }//for

            return _resultArray;
        }//end function

        //find patterns in html
        public static Hashtable filteringHTMLtags(string html_content, string pattern, Hashtable _tags)
        {
            Regex exp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

            MatchCollection matchList = exp.Matches(html_content);

            for (int i = 0; i < matchList.Count; i++)
            {
                Match match = matchList[i];
                string _str = match.Groups[0].Value;
                if (_str.Length > pattern.Length - 3)
                {
                    if (!_tags.ContainsKey(_str))
                        _tags.Add(_str, 1);
                    else
                        _tags[_str] = (int)_tags[_str] + 1;
                }
            }

            return _tags;
        }

        //find patterns in html
        private static Hashtable filteringHTMLtags_TESTER(string html_content, string pattern, Hashtable _tags)
        {
            Regex exp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

            MatchCollection matchList = exp.Matches(html_content);

            for (int i = 0; i < matchList.Count; i++)
            {
                Match match = matchList[i];

                if (!_tags.ContainsKey(matchList[i].ToString()))
                    _tags.Add(matchList[i].ToString(), 1);
                else
                    _tags[matchList[i].ToString()] = (int)_tags[matchList[i].ToString()] + 1;
            }

            return _tags;
        }

        //for a given tag
        private static Hashtable filtergivenHTMLtag_TESTER(string html_content, string pattern)
        {
            Hashtable _tags = new Hashtable();

            _tags = filteringHTMLtags_TESTER(html_content, pattern, _tags);

            return _tags;
        }

        //for test finding operation
        public static string[] Contents_of_givenLayout_Tags_TESTER(string html_content, string pattern, bool cut_sub_blocks)
        {
            Hashtable _tags_in_HTML = filtergivenHTMLtag_TESTER(html_content, pattern);
            string[] _content = null;
            string s_tag = pattern.Replace("."," ");
            string e_tag = find_EndTag(s_tag);
            int elementsize = 0;
            foreach (DictionaryEntry d in _tags_in_HTML)
            {
                string _tag = (string)d.Key;
                int _cnt = (int)d.Value;

                _content = GrabbingofHTMLTags(html_content, _tag, _cnt);
                string temp = "";
                for (int i = 0; i < _content.Length; i++)
                {
                    string t_content = _content[i];
                    if (cut_sub_blocks)
                    {
                        t_content = HTML.trimDIV(t_content);
                        t_content = HTML.trimTD(t_content);
                        _content[i] = t_content;
                    }

                    temp = temp + t_content;

                    //başlagıç ve bitiş etiketi tekrar yazılıyor.
                    _content[i] = s_tag + _content[i] + e_tag;
                }

                elementsize = elementsize + temp.Length;
            } 
  
            //başlangıç etiketini tekrar yapıştır.


            return _content;
        }

        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        public static string String_Decimal_Clear(string temp)
        {
            return Regex.Replace(temp, @"[\d-]", " ");
        }
    }//html class
}//namespace
