using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Small_World
{
    class Edge {
        public string movieName;
        public bool isVisited = false;
        public Edge(string movie) {
            this.movieName = movie;
        }
    }

    class Graph
    {
        public static Dictionary<string, List<string>> adjacentActors = new Dictionary<string, List<string>>();
        public static Dictionary<Tuple<string, string>, List<Edge>> edges = new Dictionary<Tuple<string, string>, List<Edge>>();
        
        public Graph(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] names = line.Split('/');
                initializeEdges(names);
            }
        }

        public void initializeEdges(string[] movieTemp)
        {
            for (int i = 1; i < movieTemp.Length; i++)
            {
                for (int j = 1; j < movieTemp.Length; j++)
                {
                    if (i != j)
                    {
                        if (!adjacentActors.ContainsKey(movieTemp[i]))
                        {
                            adjacentActors[movieTemp[i]] = new List<string>();
                        }
                        adjacentActors[movieTemp[i]].Add(movieTemp[j]);

                        if (!edges.ContainsKey(new Tuple<string, string>(movieTemp[i], movieTemp[j])))
                        {
                            edges[new Tuple<string, string>(movieTemp[i], movieTemp[j])] = new List<Edge>();
                        }
                        edges[new Tuple<string, string>(movieTemp[i], movieTemp[j])].Add(new Edge(movieTemp[0]));
                        Console.Write(edges[new Tuple<string, string>(movieTemp[i], movieTemp[j])][0].movieName + "\n");
                    }
                    
                }
            }
        }
        public List<string> getAdjacent(string vertex)
        {
            if (adjacentActors.ContainsKey(vertex))
            {
                return adjacentActors[vertex];
            }
            else throw new IllegalArgumentException(vertex + " is not a vertex");
        }
        
    }
}