using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchTokens
{
    public class StringTokens
    {
        private StringBuilder lettersCache;
        private List<string> result;        

        public StringTokens() {
            lettersCache = new StringBuilder();
            result = new List<string>();            
        }

        public List<string> ForSearch(string words)
        {
            bool gatheringWord = false;
                      
            foreach (char charCode in words)
            {
                string letter = charCode.ToString();


                if (letter.ToString() == "\"")
                {
                    gatheringWord = !gatheringWord;
                    continue;
                }

                if (letter == " ")
                {
                    if (gatheringWord)
                    {
                        lettersCache.Append(letter);
                    }
                    else
                    {
                        split();
                    }
                }
                else
                {
                    lettersCache.Append(letter);
                }                
            }

            end();

            return result;
        }

        private void end()
        {
            result.Add(lettersCache.ToString());
        }

        private void split()
        {
            result.Add(lettersCache.ToString());
            lettersCache = new StringBuilder();
        }
    }
}
