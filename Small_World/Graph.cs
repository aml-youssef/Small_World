using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Small_World
{
    class node
    {
        public int weight = 1;
        public bool isVisited = false;
        public string move;
        public node(string move)
        {
            this.move = move;
        }
    }

    class Graph
    {
        static Dictionary<string, List<string>> actor_to_actor = new Dictionary<string, List<string>>();//st
        static Dictionary<Tuple<string, string>, node> edge = new Dictionary<Tuple<string, string>, node>(); 
        public Graph(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] names = line.Split('/');
                addEdges_for_a_move(names);
            }
        }

        public void addEdges_for_a_move(string[] v)
        {
            for (int i = 1; i < v.Length; i++)
            {
                for (int j = 1; j < v.Length; j++)
                {
                    if (i != j)
                    {
                        if (!actor_to_actor.ContainsKey(v[i]))
                        {
                            actor_to_actor[v[i]] = new List<string>();
                        }
                        actor_to_actor[v[i]].Add(v[j]);


                        if (edge.ContainsKey(new Tuple<string, string>(v[i], v[j])))
                        {
                            edge[new Tuple<string, string>(v[i], v[j])].weight++;
                        }
                        edge[new Tuple<string, string>(v[i], v[j])] = new node(v[0]);
                    }
                }
            }
        }
        public List<string> getAdjacent(string vertex)
        {
            if (actor_to_actor.ContainsKey(vertex))
            {
                return actor_to_actor[vertex];
            }
            else throw new IllegalArgumentException(vertex + " is not a vertex");
        }
        
    }
}

/*if (!edges.ContainsKey(v1))
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
            isMovie[v2] = false;*/

/*public bool isMove(string vertex)
        {
            if (isMovie.ContainsKey(vertex))
            {
                return isMovie[vertex];
            }
            else throw new IllegalArgumentException(vertex + " is not a vertex");
        }*/