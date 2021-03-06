﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringHelpers
{
    public class SearchTokens
    {
        private StringBuilder lettersCache;        
        private List<string> agregatedWords; //words that were between WordGatheringChars
        private List<string> singularWords;
        private SearchTokensOption options = new SearchTokensOption();

        public SearchTokens(SearchTokensOption options)
        {
            this.options = options;
            lettersCache = new StringBuilder();
            initializeLists(); 
        }

        public SearchTokens() {
            lettersCache = new StringBuilder();
            initializeLists(); ;                      
        }

        private void initializeLists()
        {
            agregatedWords = new List<string>();
            singularWords = new List<string>();
        }

        /// <summary>
        /// Returns a list with all tokens detected
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public List<string> ForSearch(string words)
        {
            initializeLists();
            List<string> result;
            TokenizeWords(words);
            agregatedWords.AddRange(singularWords);
            result = agregatedWords;

            return result;
        }
        /// <summary>
        /// Returns an object with the tokens descriminated by type
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public TokenLists ForSearchDescriminated(string words) {
            initializeLists();
            TokenizeWords(words);

            TokenLists result = new TokenLists();
            result.AgregatedWords = agregatedWords;
            result.SingularWords = singularWords;

            return result;
        }

        private void TokenizeWords(string words)
        {
            bool gatheringWord = false;
            lettersCache = new StringBuilder();

            foreach (char charCode in words)
            {
                string letter = charCode.ToString();

                if (IsWordGatherer(letter))
                {
                    gatheringWord = !gatheringWord;
                    CleanCacheAndAddToAgregatedWords();
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
                        CleanCacheAndAddToSingularWords();
                    }
                }
                else
                {
                    lettersCache.Append(letter);
                }
            }
            CleanCacheAndAddToSingularWords();            
        }



        #region helpers
            private bool IsWordGatherer(string letter)
            {
                return options.WordGatheringChars.Contains(letter.ToString());
            }

            private void CleanCacheAndAddToAgregatedWords()
            {
                CleanCacheAndAddToList(agregatedWords);
            }

            private void CleanCacheAndAddToSingularWords()
            {
                CleanCacheAndAddToList(singularWords);
            }

            private void CleanCacheAndAddToList(List<string> list)
            {
                string s = lettersCache.ToString();

                if (options.TrimWhiteSpace)
                {
                    s = s.Trim();
                }

                if (s != String.Empty)
                {
                    list.Add(s);
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

    public class TokenLists
    {
        public List<string> SingularWords { get; set; }
        public List<string> AgregatedWords { get; set; }
    }
}

