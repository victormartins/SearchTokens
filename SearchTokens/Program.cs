using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchTokens
{
    class Program
    {
        static void Main(string[] args)
        {

            StringTokens st = new StringTokens();
            List<string> words;

            string word = String.Empty;
            Console.WriteLine("Press \"quit\" to exit.");
            while (word != "quit")
            {
                word = Console.ReadLine();
                Console.WriteLine("");
                words = st.ForSearch(word);
                
                foreach (string s in words)
                {
                    Console.Write("[" + s + "] ");    
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
            



        }
    }
}
