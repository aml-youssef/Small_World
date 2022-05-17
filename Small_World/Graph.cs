using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;


namespace Small_World
{
    class Graph
    {
        static Dictionary<string, int> actorsId = new Dictionary<string, int>();
        static List<List<int>> adjacentActors = new List<List<int>>();
        public static List<List<string>> actorMovies = new List<List<string>>();
        static List<string> actorNames = new List<string>();
        static int actorCount = 0;

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
                if(!actorsId.ContainsKey(movieTemp[i]))
                {
                    actorsId.Add(movieTemp[i], actorCount);
                    adjacentActors[actorCount] = new List<int>();
                    actorMovies[actorCount] = new List<string>();
                    actorCount++;
                }

                adjacentActors[actorsId[movieTemp[i]] = adjacentActors[actorsId[movieTemp[i]];

                /*
                adjacentActors[actorsId[movieTemp[i]] = adjacentActors[actorsId[movieTemp[i]].Union(movieTemp).ToList(); //exclude the duplicate
                actorMovies[movieTemp[i]].Add(movieName);
              */
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