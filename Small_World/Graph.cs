using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;


namespace Small_World
{
    class Graph
    {
        static Dictionary<string, List<string>> adjacentActors = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<string>> actorMovies = new Dictionary<string, List<string>>();
        
        public Graph(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            string line;
            Console.Write("start \n");
            while ((line = sr.ReadLine()) != null)
            {
                List<string> names = new List<string>(line.Split('/'));
                initializeEdges(names);
            }
        }

        void initializeEdges(List<string> movieTemp)
        {
            string movieName = movieTemp[0];
            movieTemp.RemoveAt(0);
            for (int i = 0; i < movieTemp.Count; i++)
            {
                if (!adjacentActors.ContainsKey(movieTemp[i]))
                {
                    adjacentActors[movieTemp[i]] = new List<string>();
                }
                adjacentActors[movieTemp[i]] = adjacentActors[movieTemp[i]].Union(movieTemp).ToList();//exclude the duplicate
                if (!actorMovies.ContainsKey(movieTemp[i]))
                {
                    actorMovies[movieTemp[i]] = new List<string>();
                }
                actorMovies[movieTemp[i]].Add(movieName);
                    
            }
        }
        public List<string> getAdjacent(string vertex)
        {
            return adjacentActors[vertex];
        }
        public int getAdjacentWeight(string source, string destination)
        {
            return actorMovies[source].Intersect(actorMovies[destination]).ToList().Count;
        }

        public string getCommonMovie(string source, string destination)
        {
            return actorMovies[source].Intersect(actorMovies[destination]).ToList()[0];
        }
    }
}
