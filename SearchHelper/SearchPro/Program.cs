using System;
using System.Collections.Generic;

namespace SearchPro
{
    class Program
    {
        static void Main(string[] args)
        {
            SearchHelper searchHelper = new SearchHelper();
            while (true)
            {
            
                string search = Console.ReadLine();
                //string result = searchHelper.HelpSearch(search);
                //Console.WriteLine(result);

                //string verbose1 = searchHelper.SearchCleaner(search);
                //string verbose2 = searchHelper.Language(search);
                string verbose3 = "";
                string verbose4 = "";
                string verbose5 = "";
                List<string> verbose6 = searchHelper.FinalSearchVariance(search);
               // Console.WriteLine(verbose1);
               // Console.WriteLine(verbose2);
                Console.WriteLine(verbose3);
                Console.WriteLine(verbose4);
                Console.WriteLine(verbose5);
                foreach (var item in verbose6)
                {
                Console.WriteLine(item);

                }
            }
        }
    }
}
