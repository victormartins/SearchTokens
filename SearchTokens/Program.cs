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
            string words = String.Empty;
            menu();
            while (words != "quit")
            {
                words = Console.ReadLine();
                
                if (words != "quit")
                {
                    Execute(words);
                    Console.ReadKey();
                    menu();   
                }
            }
        }

        private static void menu()
        {
            Console.Clear();
            Console.WriteLine("Press \"quit\" to exit.");
        }

        private static void Execute(string words)
        {
            SearchTokens st = new SearchTokens();
            Sanitizer sa = new Sanitizer();
            Console.WriteLine("");
            Console.WriteLine("Simple Tokenizer");
            List<string> tokens = st.ForSearch(words);

            foreach (string token in tokens)
            {
                Console.Write("[" + token + "] ");
            }
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Descriminated Tokenizer with escape");
            TokenLists tokenList = st.ForSearchDescriminated(words);
            Console.WriteLine("  Singular");
            Console.Write("  ");
            foreach (string token in tokenList.SingularWords)
            {
                Console.Write("[" + token + "] "); 
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("  Agregated and Escaped words");
            Console.Write("  ");
            foreach (string token in tokenList.AgregatedWords)
            {
                Console.Write("[" + sa.EscapeNonAlphanumeric(token) + "] ");
            }
        }
    }
}
