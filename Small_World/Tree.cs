using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Linq;


namespace Small_World
{
    class Tree
    {
        Graph graph;
        static List<int> previous = new List<int>();//O(1)
        static List<int> distance = new List<int>();
        static List<int> strength = new List<int>();
        List<int> degreeFrequency = new List<int>();
        public Tree(string queries_File, string movies_File, bool isOptimized)
        {
            graph = new Graph(movies_File);//O(1)
            Stack<string> vertices = new Stack<string>();//O(1)
            FileStream file = new FileStream(queries_File, FileMode.Open, FileAccess.Read);//O(1)
            StreamReader sr = new StreamReader(file);//O(1)
            string line;
            while ((line = sr.ReadLine()) != null) //O(N)
            {
                vertices.Clear();
                previous.Clear();
                distance.Clear();
                strength.Clear();
                degreeFrequency.Clear();
                for (int i = 0; i < graph.actorsId.Count; i++)//O(N)
                {
                    previous.Add(-1);
                    distance.Add(0);
                    strength.Add(0);
                }
                string[] names = line.Split('/');//O(N)
                KeyValuePair<int, int> query = new KeyValuePair<int, int>(graph.getActorID(names[0]), graph.getActorID(names[1]));//O(1)
                initializeBFSTree(graph, query.Key, query.Value, isOptimized);
                Console.Write(names[0] + "/" + names[1] + " \n" );
                Console.Write("DoS = " + degreeOf_Separation(query.Value));
                Console.Write(",  RS = " + relation_strenth(query.Value) + " \n");
                if(!isOptimized) { 
                vertices = actorsChain(query.Value, query.Key);
                Console.Write("CHAIN OF ACTORS: " + vertices.Pop());
                }
                while (vertices.Count != 0) { 
                    Console.Write(" -> " + vertices.Pop());
                }
                
                vertices = moviesChain(query.Value, query.Key);
                Console.Write("\nCHAIN OF MOVIES: => ");
                while (vertices.Count != 0)
                {
                    Console.Write(vertices.Pop() + " => ");
                }
                Console.Write("\n");
                if (!isOptimized)
                {
                    for(int i = 0; i <degreeFrequency.Count; i++)
                    {
                        Console.WriteLine(i + " - " + degreeFrequency[i]);
                    } 
                }
                Console.Write("\n");
                
            }
        }
        public void initializeBFSTree(Graph graph, int root, int destination, bool isOptimized)
        {
            Queue<int> nextLevelQueue = new Queue<int>();//O(1)
            Queue<int> currentQueue = new Queue<int>();
            
            degreeFrequency.Add(1);
            Dictionary<int, int> priorityDictionary = new Dictionary<int, int>();
            bool isDestinationFound = false;
            currentQueue.Enqueue(root);//O(1)
            distance[root] = 0;
            previous[root] = -2;
            strength[root] = 0;

            while (true)//O(N^3)
            {
                if (currentQueue.Count == 0)
                {
                    nextLevelQueue = priorityQueue(priorityDictionary);//O(N)
                    if (nextLevelQueue.Count == 0 || isDestinationFound)
                    {
                        break;
                    }
                    else
                    {
                        degreeFrequency.Add(nextLevelQueue.Count());
                        currentQueue = nextLevelQueue;
                        priorityDictionary.Clear();//O(1)
                    }
                }
                int u = currentQueue.Dequeue();//O(1)
                foreach (int v in graph.getAdjacent(u))//O(N^2)
                {
                    if (u == v)//O(1)
                    {
                        continue;
                    }
                    if (v == destination && !isDestinationFound && isOptimized) // REMOVE FOR BONUS //O(1)
                    {
                        isDestinationFound = true;
                    }
                    if (distance[v] == 0)//if()!distance.Contains(v) //O(1)
                    {
                        distance[v] = distance[u] + 1;
                        previous[v] = u;
                        priorityDictionary[v] = strength[v] = graph.getAdjacentWeight(u, v) + strength[u];//O(N)
                    }
                    else
                    {
                        if (priorityDictionary.ContainsKey(v))
                        {
                            if (priorityDictionary[v] < graph.getAdjacentWeight(u, v) + strength[u])//O(N)
                            {
                                distance[v] = distance[u] + 1;
                                previous[v] = u;
                                priorityDictionary[v] = strength[v] = graph.getAdjacentWeight(u, v) + strength[u];//O(N)
                            }
                        }
                    }
                }
            }
        }
        public Queue<int> priorityQueue(Dictionary<int, int> priorityDictionary)//O(N)
        {
            var myList = priorityDictionary.ToList();//O(N)
            myList.OrderByDescending((pair1 => pair1.Value)).ToList();//O(N)
            Queue<int> sortedQueue = new Queue<int>(priorityDictionary.Keys);
            return sortedQueue;
        }
        public int degreeOf_Separation(int vertex)//O(1)
        {
            return distance[vertex];
        }
        public int relation_strenth(int destination)//O(1)
        {
            return strength[destination];
        }
        public Stack<string> actorsChain(int destination, int root)//O(N)
        {
            Stack<string> stack = new Stack<string>();
            int current = destination;
            stack.Push(graph.getActorName(destination));
            while (current != root)//O(N)
            {
                stack.Push(graph.getActorName(previous[current]));
                current = previous[current];
            }
            return stack;
        }
        public Stack<string> moviesChain(int destination, int root)//O(N^2)
        {
            Stack<string> stack = new Stack<string>();
            int current = destination;
            while(current != root)//O(N^2)
            {
                stack.Push(graph.getCommonMovie(previous[current], current));//O(N)
                current = previous[current];
            }
            return stack;
        }
        
    }
}