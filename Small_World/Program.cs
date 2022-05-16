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
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            //Tree sample = new Tree("queries1.txt", "movies1.txt");
            //Tree smallCase1 = new Tree("queries110.txt", "Movies193.txt"); // 0.23 seconds
            //Tree smallCase2 = new Tree("queries50.txt", "Movies187.txt"); // 1.5 seconds  
            //Tree mediumCase1_query1 = new Tree("queries85.txt", "Movies967.txt"); // 4 seconds
            //Tree mediumCase1_query2 = new Tree("queries4000.txt", "Movies967.txt"); // 1 min, 8 seconds
            //Tree large_query1 = new Tree("queries26.txt", "Movies14129.txt"); // 1 min, 26 seconds
            //Tree large_query2 = new Tree("queries600.txt", "Movies14129.txt");// 17 seconds     
            //Tree extreme_query1 = new Tree("queries22.txt", "Movies122806.txt");// 8 minutes 
            //Tree extreme_query2 = new Tree("queries200.txt", "Movies122806.txt"); // 1 minute 30 seconds
            
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
                 ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

        }
    }
}
