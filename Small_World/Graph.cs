using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;


namespace Small_World
{
    class Graph
    {
        static Dictionary<string, int> actorsId = new Dictionary<string, int>();//O(1)
        static List<List<int>> adjacentActors = new List<List<int>>();//O(1)
        public static List<List<string>> actorMovies = new List<List<string>>();//O(1)
        static List<string> actorNames = new List<string>();//O(1)
        static int actorCount = 0;//O(1)

        public Graph(string fileName)//O(N^2)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);//O(1)
            StreamReader sr = new StreamReader(file);//O(1)
            string line;//O(1)
            Console.Write("start \n");
            while ((line = sr.ReadLine()) != null)//O(N)
            {
                List<string> names = new List<string>(line.Split('/'));//O(1)
                initializeEdges(names);//O(N)
            }
        }

        void initializeEdges(List<string> movieTemp)
        {

            string movieName = movieTemp[0]; //O(1)
            movieTemp.RemoveAt(0);//O(1)
            for (int i = 0; i < movieTemp.Count; i++)//O(1)
            {   
                if(!actorsId.ContainsKey(movieTemp[i]))//O(1)
                {
                    actorsId.Add(movieTemp[i], actorCount);//O(1)
                    adjacentActors[actorCount] = new List<int>();//O(1)
                    actorMovies[actorCount] = new List<string>();//O(1)
                    actorCount++;//O(1)
                }

                adjacentActors[actorsId[movieTemp[i]] = adjacentActors[actorsId[movieTemp[i]];//O(1)

                /*
                adjacentActors[actorsId[movieTemp[i]] = adjacentActors[actorsId[movieTemp[i]].Union(movieTemp).ToList(); //exclude the duplicate
                actorMovies[movieTemp[i]].Add(movieName);
              */
            }
        }
        public List<string> getAdjacent(string vertex)
        {
            return adjacentActors[vertex];//O(1)
        }
        public int getAdjacentWeight(string source, string destination)
        {
            return actorMovies[source].Intersect(actorMovies[destination]).ToList().Count; //O(N^2)
        }

        public string getCommonMovie(string source, string destination)
        {
            return actorMovies[source].Intersect(actorMovies[destination]).ToList()[0];//O(N^2)
        }
    }
}