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
        static Dictionary<string, int> strength = new Dictionary<string, int>();
        List<int> degreeFrequency = new List<int>();
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
                strength.Clear();
                degreeFrequency.Clear();

                string[] names = line.Split('/');
                KeyValuePair<string, string> query = new KeyValuePair<string, string>(names[0], names[1]);
                initializeBFSTree(graph, query.Key, query.Value);
                Console.Write(query.Key + "/" + query.Value + " \n" );
                Console.Write("DoS = " + degreeOf_Separation(query.Value));
                Console.Write(",  RS = " + relation_strenth(query.Value) + " \n");
                vertces = actorsChain(query.Value, query.Key);
                Console.Write("CHAIN OF ACTORS: " + vertces.Pop());
                while (vertces.Count != 0) { 
                    Console.Write(" -> " + vertces.Pop());
                }
                
                vertces = moviesChain(query.Value, query.Key);
                Console.Write("\nCHAIN OF MOVIES: => ");
                while (vertces.Count != 0)
                {
                    Console.Write(vertces.Pop() + " => ");
                }
                Console.Write("\n");
                /*for(int i = 0; i <degreeFrequency.Count; i++)
                {
                    Console.WriteLine(i + " - " + degreeFrequency[i]);
                } --- BONUS */
                Console.Write("\n");
                
            }
        }
        public void initializeBFSTree(Graph graph, string root, string destination)
        {
            Queue<String> nextLevelQueue = new Queue<String>();
            Queue<String> currentQueue = new Queue<String>();
            
            degreeFrequency.Add(1);
            Dictionary<string, int> priorityDictionary = new Dictionary<string, int>();
            bool isDestinationFound = false;
            currentQueue.Enqueue(root);
            distance[root] = 0;
            previous[root] = null;
            strength[root] = 0;

            while (true)
            {
                if (currentQueue.Count == 0)
                {
                    nextLevelQueue = priorityQueue(priorityDictionary);
                    if (nextLevelQueue.Count == 0 || isDestinationFound)
                    {
                        break;
                    }
                    else
                    {
                        degreeFrequency.Add(nextLevelQueue.Count());
                        currentQueue = nextLevelQueue;
                        priorityDictionary.Clear();
                    }
                }
                string u = currentQueue.Dequeue();
                foreach (string v in graph.getAdjacent(u))
                {
                    if (u == v)
                    {
                        continue;
                    }
                    if (v == destination && !isDestinationFound) // REMOVE FOR BONUS
                    {
                        isDestinationFound = true;
                    }
                    if (!distance.ContainsKey(v))
                    {
                        distance[v] = distance[u] + 1;
                        previous[v] = u;
                        priorityDictionary[v] = strength[v] = graph.getAdjacentWeight(u, v) + strength[u];
                        
                    }
                    else
                    {
                        if (priorityDictionary.ContainsKey(v))
                        {
                            if (priorityDictionary[v] < graph.getAdjacentWeight(u, v) + strength[u])
                            {
                                distance[v] = distance[u] + 1;
                                previous[v] = u;
                                priorityDictionary[v] = strength[v] = graph.getAdjacentWeight(u, v) + strength[u];
                            }
                        }
                    }
                }
            }
        }
        public Queue<string> priorityQueue(Dictionary<string, int> priorityDictionary)
        {
            var myList = priorityDictionary.ToList();
            myList.OrderByDescending((pair1 => pair1.Value)).ToList();
            Queue<string> sortedQueue = new Queue<string>(priorityDictionary.Keys);
            return sortedQueue;
        }
        public int degreeOf_Separation(string vertex)
        {
            return distance[vertex];
        }
        public int relation_strenth(string destination)
        {
            return strength[destination];
        }
        public Stack<string> moviesChain(string destination, string root)
        {
            Stack<string> stack = new Stack<string>();
            string current = destination;
            while(previous[current] != null)
            {
                stack.Push(graph.getCommonMovie(previous[current], current));
                current = previous[current];
            }
            return stack;
        }
        public Stack<string> actorsChain(string destination, string root)
        {
            Stack<string> stack = new Stack<string>();
            stack.Push(destination);
            string current = destination;
            while(previous[current] != null) {
                stack.Push(previous[current]);
                current = previous[current];
            }
            return stack;
        }
    }
}
