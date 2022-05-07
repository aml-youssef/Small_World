using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Small_World
{
    class Graph
    {
        static Dictionary<string, List<string>> edges = new Dictionary<string, List<string>>();//st
        static Dictionary<string, bool> isMovie = new Dictionary<string, bool>();

        public Graph(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] names = line.Split('/');
                for (int i = 1; i < names.Length; i++)
                {
                    addEdge(names[0], names[i]);
                }
            }
        }

        public void addEdge(string v1, string v2)
        {
            if (!edges.ContainsKey(v1))
            {
                edges[v1] = new List<string>();
            }
            if (!edges.ContainsKey(v2))
            {
                edges[v2] = new List<string>();
            }
            edges[v1].Add(v2);
            edges[v2].Add(v1);
            isMovie[v1] = true;
            isMovie[v2] = false;
        }
        public List<string> getAdjacent(string vertex)
        {
            if (edges.ContainsKey(vertex))
            {
                return edges[vertex];
            }
            else throw new IllegalArgumentException(vertex + " is not a vertex");
        }
        public bool isMove(string vertex)
        {
            if (isMovie.ContainsKey(vertex))
            {
                return isMovie[vertex];
            }
            else throw new IllegalArgumentException(vertex + " is not a vertex");
        }
    }
}
