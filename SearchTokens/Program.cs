using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringHelpers
{
    class Program
    {
        static void Main(string[] args)
        {

            SearchTokens st = new SearchTokens();
            List<string> tokens;

            string words = String.Empty;
            Console.WriteLine("Press \"quit\" to exit.");
            while (words != "quit")
            {
                words = Console.ReadLine();
                Console.WriteLine("");
                tokens = st.ForSearch(words);
                
                foreach (string token in tokens)
                {
                    Console.Write("[" + token + "] ");    
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }
}
