using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;


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
                /*vertces = chain_of_Actors(query.Value, query.Key);
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
                Console.Write("\n");*/
                Console.Write("\n");
            }
        }
        public void Create_BFS_Tree(Graph graph, string root)
        {
            Queue<String> nextLevelQueue = new Queue<String>();
            Queue<String> currentQueue = new Queue<String>();
            Dictionary<string, int> weight = new Dictionary<string, int>();
            currentQueue.Enqueue(root);
            distance[root] = 0;
            previous[root] = null;

            while (true)
            {
                if (currentQueue.Count == 0)
                {
                    nextLevelQueue = priorityQueue(weight);
                    if (nextLevelQueue.Count == 0)
                    {
                        break;
                    }
                    else
                    {
                        currentQueue = nextLevelQueue;
                        weight.Clear();
                    }
                }
                string u = currentQueue.Dequeue();
                foreach (string v in graph.getAdjacent(u))
                {
                    if (!distance.ContainsKey(v))
                    {
                        distance[v] = distance[u] + 1;
                        previous[v] = u;
                        weight[v] = graph.getVertixWeight(u, v);//Enqueue(v);

                    }
                    else
                    {
                        if (weight.ContainsKey(v))
                        {
                            if (weight[v] < graph.getVertixWeight(u, v))
                            {
                                distance[v] = distance[u] + 1;
                                previous[v] = u;
                                weight[v] = graph.getVertixWeight(u, v);
                            }
                        }
                    }
                }
            }
        }
        public Queue<string> priorityQueue(Dictionary<string, int> weight)
        {
            Queue<string> sortedQueue = new Queue<string>();
            var myList = weight.ToList();
            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myList.Reverse();
            foreach (var value in myList)
            {
                sortedQueue.Enqueue(value.Key);
            }
            return sortedQueue;
        }
        public int degreeOf_Separation(string vertex)
        {
            return distance[vertex];
        }
        public int relation_strenth(string destination, string root)
        {
            int weightSum = 0;
            string alo;
            while (destination != root)
            {
                alo = previous[destination];
                weightSum += graph.getVertixWeight(alo, destination);
                destination = previous[destination];
            }
            return weightSum;
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
