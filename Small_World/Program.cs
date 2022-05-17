using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Small_World
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true) { 
            Console.WriteLine("0. Non optimized run (including bonus)\n" +
                "1. Optimized Run");
            bool isOptimized = Convert.ToBoolean(Convert.ToInt16(Console.ReadLine()));
            Console.WriteLine("1. Small case 1 (110 queries)\n" +
                "2. Small case 2 (50 queries)\n" +
                "3. Medium case 1 (85 queries)\n" +
                "4. Medium case 2 (4000 queries)\n" +
                "5. Large case 1 (26 queries)\n" +
                "6. Large case 2 (600 queries)\n" +
                "7. Extreme case 1 (22 queries)\n" +
                "8. Extreme case 2 (200 queries)\n" +
                "9. Quit");
            int choice = int.Parse(Console.ReadLine());
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            switch (choice)
            {
                case 1:
                    Tree smallCase1 = new Tree("queries110.txt", "Movies193.txt", isOptimized);
                    break;
                case 2:
                    Tree smallCase2 = new Tree("queries50.txt", "Movies187.txt", isOptimized);
                    break;
                case 3:
                    Tree mediumCase1_query1 = new Tree("queries85.txt", "Movies967.txt", isOptimized);
                    break;
                case 4:
                    Tree mediumCase1_query2 = new Tree("queries4000.txt", "Movies967.txt", isOptimized);
                    break;
                case 5:
                    Tree large_query1 = new Tree("queries26.txt", "Movies14129.txt", isOptimized);
                    break;
                case 6:
                    Tree large_query2 = new Tree("queries600.txt", "Movies14129.txt", isOptimized);
                    break;
                case 7:
                    Tree extreme_query1 = new Tree("queries22.txt", "Movies122806.txt", isOptimized);
                    break;
                case 8:
                    Tree extreme_query2 = new Tree("queries200.txt", "Movies122806.txt", isOptimized);
                    break;
                case 9:
                    return;
                default: throw new Exception("Error");
            }
            //Tree sample = new Tree("queries1.txt", "movies1.txt");
            

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
                 ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            }
        }
    }
}
