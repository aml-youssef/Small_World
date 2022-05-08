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
            Queue<String> queue = new Queue<String>();
            //PriorityQueue<string, int> queue = new PriorityQueue<string, int>();

            List<string> sons = new List<string>();
            queue.Enqueue(root);
            distance[root] = 0;
            previous[root] = null;
            //strength[root] = 0;
            while (queue.Count != 0) 
            {
                sons.Clear();
                string u = queue.Dequeue();
                foreach (string v in graph.getAdjacent(u))
                {
                    if (!distance.ContainsKey(v))
                    {
                        distance[v] = distance[u] + 1;      
                        previous[v] = u;
                        //strength[v] = 1;
                        sons.Add(v);
                        queue.Enqueue(v);                   
                    }
                    else if (distance.ContainsKey(v) && !(v.Equals(root)) && !(v.Equals(previous[u]))) 
                    {

                        if (distance[previous[v]] == distance[u] && strength[previous[v]] < strength[u]) //&& graph.isMove(u) )
                        {
                            previous[v] = u;
                            strength[v] = 1;
                            distance[v] = distance[u] + 1;
                            //sons.Add(v);
                        }

                        if (!(previous[v].Equals(root)))
                        {
                            if (previous[previous[v]].Equals(previous[u]))
                            {
                                if (graph.isMove(v))
                                {
                                    //strength[u]++;
                                    foreach(string son in sons)
                                    {
                                        //if(son != v)
                                            strength[son]++;
                                    }
                                }
                                else
                                {
                                    strength[v]++;
                                }
                                //Console.Write("\n" + v + " AlooooooooooooooooooooooooooooWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW \n");
                                /*if(strength[previous[u]] < strength[v] && distance[previous[u]] == distance[v])
                                {
                                    previous[u] = v; 
                                }*/
                            }
                        }

                        

                        /*if (strength[previous[v]] < strength[u] && distance[previous[v]] == distance[u])
                        {
                            //distance[v] = distance[u] + 1;
                            previous[v] = u;
                        }*/
                    }*/

                }
            }
        }
        public int degreeOf_Separation(string vertex)
        {
            return distance[vertex]/2;
        }
        public int relation_strenth(string vertex, string root)
        {
            bool isActor = true;
            int max = 0;
            while (vertex != null && distance.ContainsKey(vertex) && !(vertex.Equals(root)))
            {
                if (isActor)
                {
                    max += strength[vertex];
                    if (strength[vertex] > 1)
                    {
                        //Console.Write("\n" + vertex + " AloooooooooooooooooooooooooooooBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB \n");
                    }
                    isActor = false;
                }
                else
                {
                    isActor = true;
                }

                vertex = previous[vertex];
            }
            return max;
        }

        public Stack<string> chain_of_Movies(string vertex, string root)
        {
            Stack<string> movies = new Stack<string>();
            bool isMove = true;
            while (vertex!=null && distance.ContainsKey(vertex) && vertex != root)
            {
                vertex = previous[vertex];
                if (isMove)
                {
                    movies.Push(vertex);
                    isMove = false;
                }
                else
                {
                    isMove = true;
                }
            }
            return movies;
        }
        public Stack<string> chain_of_Actors(string vertex, string root)
        {
            Stack<string> actors = new Stack<string>();
            bool isActor = true;
            while (vertex != null && distance.ContainsKey(vertex))
            {
                if (isActor)
                {
                    actors.Push(vertex);
                    isActor = false;
                }
                else
                {
                    isActor = true;
                }
                if(vertex.Equals(root))
                {
                    break;
                }
                vertex = previous[vertex];
            }
            return actors;
        }
    }
}

/*bool isActor = true;
int max = 0;
while (vertex != null && distance.ContainsKey(vertex) && !(vertex.Equals(root)))
{
    if (isActor)
    {
        max += strength[vertex];
        if (strength[vertex] > 1)
        {
            //Console.Write("\n" + vertex + " AloooooooooooooooooooooooooooooBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB \n");
        }
        isActor = false;
    }
    else
    {
        isActor = true;
    }

    vertex = previous[vertex];
}
return max;*/


//double result = strength[vertex] / 2;
//return Math.Ceiling(result);// (/2) not sure
