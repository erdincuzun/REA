using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Library_ABE.text
{
    public static class CaseFunctions
    {
        public static string LowerCase(string _sentence, string language)
        {
            if (language == "Turkish")
                _sentence = _sentence.ToLower(new CultureInfo("tr-TR", false));
            else
                _sentence = _sentence.ToLower(new CultureInfo("en-US", false));

            return _sentence;
        }

        public static string UpperCase(string _sentence, string language)
        {
            if (language == "Turkish")
                _sentence = _sentence.ToUpper(new CultureInfo("tr-TR", false));
            else
                _sentence = _sentence.ToUpper(new CultureInfo("en-US", false));

            return _sentence;
        }
    }
}
