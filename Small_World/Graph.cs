using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Small_World
{
    class Graph
    {
        static Dictionary<string, List<string>> adjacentActors = new Dictionary<string, List<string>>();
        static Dictionary<Tuple<string, string>, List<string>> edges = new Dictionary<Tuple<string, string>, List<string>>();
        
        public Graph(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            string line;
            Console.Write("start \n");
            while ((line = sr.ReadLine()) != null)
            {
                string[] names = line.Split('/');
                initializeEdges(names);
            }
        }

        void initializeEdges(string[] movieTemp)
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
                            edges[new Tuple<string, string>(movieTemp[i], movieTemp[j])] = new List<string>();
                        }
                        edges[new Tuple<string, string>(movieTemp[i], movieTemp[j])].Add(movieTemp[0]);
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
        public int getAdjacentWeight(string source, string destination)
        {
            if(edges.ContainsKey(new Tuple<string, string>(source, destination)))
            {
                return edges[new Tuple<string, string>(source, destination)].Count;
            }
            else throw new IllegalArgumentException(source + " and " + destination + " is not a vertices");
        }



    }
}