using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library_ABE.BayesClassifier
{
    public class ExcludedWords
    {
        /// <summary>
        /// List of english words i'm not interested in</summary>
        /// <remarks>
        /// You might use frequently used words for this list
        /// </remarks>
        string[] enu_most_common_English =
		{
			 "the", "to", "and", "a", "an", "in", "is", "it", "you", "that", "i", "we",  "for", "on", "are", "with", "be", "been", "at", "one"
        };

        string[] enu_most_common_Turkish =
		{
			 "ve", "bir", "ben", "sen", "o"
        };

        Dictionary<string, int> m_Dict;
        //language 0: Turkish, 1: English
        public ExcludedWords(int language)
        {
            m_Dict = new Dictionary<string, int>();

            string[] temp_exc;
            if(language == 0)
                temp_exc = enu_most_common_Turkish;
            else
                temp_exc = enu_most_common_English;

            m_Dict.Clear();
            for (int i = 0; i < temp_exc.Length; i++)
            {
                m_Dict.Add(temp_exc[i], i);
            }
        }

        //kelime kontrolü
        public bool IsExcluded(string word)
        {
            return m_Dict.ContainsKey(word);
        }
    }
}
