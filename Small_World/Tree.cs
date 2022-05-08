using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Small_World
{
    class Tree
    {
        static Dictionary<string, string> previous = new Dictionary<string, string>();
        static Dictionary<string, int> distance = new Dictionary<string, int>();
        //static Dictionary<string, int> strength = new Dictionary<string, int>();
        Graph graph;
        public Tree(string queries_File, string movies_File)
        {
            graph = new Graph(movies_File);
            Stack<string> vertces = new Stack<string>();
            FileStream file = new FileStream(queries_File, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null) 
            {
                vertces.Clear();
                previous.Clear();
                distance.Clear();
                //strength.Clear();
                string[] names = line.Split('/');
                KeyValuePair<string, string> query = new KeyValuePair<string, string>(names[0], names[1]);
                Create_BFS_Tree(graph, query.Key);
                Console.Write(query.Key + "/" + query.Value + " \n" );
                Console.Write("DoS = " + degreeOf_Separation(query.Value));
                Console.Write(",  RS = " + relation_strenth(query.Value, query.Key) + " \n");
                vertces = chain_of_Actors(query.Value, query.Key);
                Console.Write("CHAIN OF ACTORS: " + vertces.Pop());
                while (vertces.Count != 0) { 
                    Console.Write(" -> " + vertces.Pop());
                }
                vertces = chain_of_Movies(query.Value, query.Key);
                Console.Write("\nCHAIN OF MOVIES: => ");
                while (vertces.Count != 0)
                {
                    Console.Write(vertces.Pop() + " => ");
                }
                Console.Write("\n");
                Console.Write("\n");
            }
        }
        public void Create_BFS_Tree(Graph graph, string root)
        {
            distance[root] = 0;
            previous[root] = null;


        }
        public int degreeOf_Separation(string vertex)
        {
            return 0;
        }
        public int relation_strenth(string vertex, string root)
        {
            return 0;
        }

        public Stack<string> chain_of_Movies(string vertex, string root)
        {
            return new Stack<string>();
        }
        public Stack<string> chain_of_Actors(string vertex, string root)
        {
            return new Stack<string>();
        }
    }
}
