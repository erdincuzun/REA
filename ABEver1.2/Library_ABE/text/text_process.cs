using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Library_ABE.text
{
    public static class text_process
    {
        public static string String_Decimal_Clear(string temp)
        {
            return Regex.Replace(temp, @"[\d-]", " ");
        }
        
    }
}
