using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ID32Translit
{
    class ID32TranslitTools
    {
        public static string ConvertToUtf8(string s)
        {
            if (s != null)
            {
                return new string(s.ToCharArray().
                    Select(x => ((x + 848) >= 'А' && (x + 848) <= 'ё') ? (char)(x + 848) : x).
                    ToArray());
            }
            return "";
        }


        public static Boolean isCyrillic(String s)
        {
            if (s != null)
            {
                return Regex.IsMatch(s, @"\p{IsCyrillic}");
            }
            return false;
        }

        public static Boolean IsEnglish(string inputstring)
        {
            if (inputstring != null)
            {
                Regex regex = new Regex(@"[¸`A-Za-z0-9\s\.,-=+\(){}\[\]?|_@#$%^&*\t!""'/]");
                
                MatchCollection matches = regex.Matches(inputstring);
                
                if (matches.Count == inputstring.Length)
                {
                    return true;
                }
                Console.WriteLine(inputstring + " -> " + matches.Count + " " + inputstring.Length);
                return false;
            }
            return true;
        }
        public static Boolean checkIfID3AreInEnglish(ref TagLib.File ID3)
        {
            if (!IsEnglish(ID3.Tag.Title) || !IsEnglish(ID3.Tag.FirstPerformer) || !IsEnglish(ID3.Tag.Album))
            {
                return false;
            }
            return true;
        }
    }
}
