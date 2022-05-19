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
        static List<int> previous = new List<int>();
        static List<int> distance = new List<int>();
        static List<int> strength = new List<int>();
        List<int> degreeFrequency = new List<int>();
        public Tree(string queries_File, string movies_File, bool isOptimized)
        {
            graph = new Graph(movies_File);
            Stack<string> vertices = new Stack<string>();
            FileStream file = new FileStream(queries_File, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null) 
            {
                vertices.Clear();
                previous.Clear();
                distance.Clear();
                strength.Clear();
                degreeFrequency.Clear();
                for (int i = 0; i < graph.actorsId.Count; i++)
                {
                    previous.Add(-1);
                    distance.Add(0);
                    strength.Add(0);
                }
                string[] names = line.Split('/');
                KeyValuePair<int, int> query = new KeyValuePair<int, int>(graph.getActorID(names[0]), graph.getActorID(names[1]));
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
            Queue<int> nextLevelQueue = new Queue<int>();
            Queue<int> currentQueue = new Queue<int>();
            
            degreeFrequency.Add(1);
            Dictionary<int, int> priorityDictionary = new Dictionary<int, int>();
            bool isDestinationFound = false;
            currentQueue.Enqueue(root);
            distance[root] = 0;
            previous[root] = -2;
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
                int u = currentQueue.Dequeue();
                foreach (int v in graph.getAdjacent(u))
                {
                    if (u == v)
                    {
                        continue;
                    }
                    if (v == destination && !isDestinationFound && isOptimized) // REMOVE FOR BONUS
                    {
                        isDestinationFound = true;
                    }
                    if (distance[v] == 0)//if()!distance.Contains(v)
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
        public Queue<int> priorityQueue(Dictionary<int, int> priorityDictionary)
        {
            var myList = priorityDictionary.ToList();
            myList.OrderByDescending((pair1 => pair1.Value)).ToList();
            Queue<int> sortedQueue = new Queue<int>(priorityDictionary.Keys);
            return sortedQueue;
        }
        public int degreeOf_Separation(int vertex)
        {
            return distance[vertex];
        }
        public int relation_strenth(int destination)
        {
            return strength[destination];
        }
        public Stack<string> actorsChain(int destination, int root)
        {
            Stack<string> stack = new Stack<string>();
            int current = destination;
            stack.Push(graph.getActorName(destination));
            while (current != root)
            {
                stack.Push(graph.getActorName(previous[current]));
                current = previous[current];
            }
            return stack;
        }
        public Stack<string> moviesChain(int destination, int root)
        {
            Stack<string> stack = new Stack<string>();
            int current = destination;
            while(current != root)
            {
                stack.Push(graph.getCommonMovie(previous[current], current));
                current = previous[current];
            }
            return stack;
        }
        
    }
}