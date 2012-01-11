using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringHelpers
{
    public class Sanitizer
    {

        public string EscapeNonAlphanumeric(string words){
            StringBuilder sb = new StringBuilder();
            string escaped = String.Empty;
            foreach (char c in words)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    escaped = "\\" + c;
                }
                else
                {
                    escaped = c.ToString();
                }
                sb.Append(escaped);
            }
            return sb.ToString();
        }
    }
}
