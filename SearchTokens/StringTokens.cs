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
        private string WORD_GATHERING_CHARS = "\"'";
        private bool trimWhiteSpace = false;

        public StringTokens() {
            lettersCache = new StringBuilder();
            result = new List<string>();            
        }

        public List<string> ForSearch(string words, bool trimWhiteSpace)
        {
            this.trimWhiteSpace = trimWhiteSpace;
            bool gatheringWord = false;
            lettersCache = new StringBuilder();
            result = new List<string>();

            foreach (char charCode in words)
            {
                string letter = charCode.ToString();

                if (IsWordGatherer(letter))
                {
                    if (gatheringWord)
                    {
                        split();
                    }
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

            split();

            return result;
        }


        public List<string> ForSearch(string words)
        {
            return this.ForSearch(words, false);
        }

        #region helpers
            private bool IsWordGatherer(string letter)
            {
                return WORD_GATHERING_CHARS.Contains(letter.ToString());
            }

            private void split()
            {
                string s = lettersCache.ToString();
                
                if (trimWhiteSpace)
                {
                    s = s.Trim();    
                }

                if (s != String.Empty)
                {
                    result.Add(s);
                }

                lettersCache = new StringBuilder();
            }

        #endregion

    }
}
