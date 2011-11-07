using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringHelpers
{
    public class SearchTokens
    {
        private StringBuilder lettersCache;
        private List<string> result;                
        private SearchTokensOption options = new SearchTokensOption();

        public SearchTokens(SearchTokensOption options)
        {
            this.options = options;
            lettersCache = new StringBuilder();
            result = new List<string>();            
        }

        public SearchTokens() {
            lettersCache = new StringBuilder();
            result = new List<string>();                    
        }


        public List<string> ForSearch(string words)
        {            
            bool gatheringWord = false;
            lettersCache = new StringBuilder();
            result = new List<string>();

            foreach (char charCode in words)
            {
                string letter = charCode.ToString();

                if (IsWordGatherer(letter))
                {
                    gatheringWord = !gatheringWord;
                    CleanCacheAndAddToResult();
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
                        CleanCacheAndAddToResult();
                    }
                }
                else
                {
                    lettersCache.Append(letter);
                }
            }

            CleanCacheAndAddToResult();

            return result;
        }



        #region helpers
            private bool IsWordGatherer(string letter)
            {
                return options.WordGatheringChars.Contains(letter.ToString());
            }

            private void CleanCacheAndAddToResult()
            {
                string s = lettersCache.ToString();
                
                if (options.TrimWhiteSpace)
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

    public class SearchTokensOption
    {
        public SearchTokensOption()
        {
            WordGatheringChars = "\"'";
            TrimWhiteSpace = false;
        }

        public string WordGatheringChars { get; set; }
        public bool TrimWhiteSpace { get; set; }
    }
}
