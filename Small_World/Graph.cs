using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;


namespace Small_World
{
    class Graph
    {
        public Dictionary<string, int> actorsId = new Dictionary<string, int>();
        public Dictionary<string, int> movieId = new Dictionary<string, int>();
        List<List<int>> adjacentActors = new List<List<int>>();
        public static List<List<int>> actorMovies = new List<List<int>>();
        List<string> actorNames = new List<string>();
        List<string> moviesNames = new List<string>();
        public int actorCount = 0;
        public int movieCount = 0;

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
            movieId[movieName] = movieCount;
            moviesNames.Add(movieName);
            movieCount++;
            for (int i = 0; i < movieTemp.Count; i++)
            {   
                if(!actorsId.ContainsKey(movieTemp[i]))
                {
                    actorsId.Add(movieTemp[i], actorCount);
                    actorNames.Add(movieTemp[i]);
                    adjacentActors.Add(new List<int>());
                    actorMovies.Add(new List<int>());
                    actorCount++;
                }                
            }
            for (int i = 0; i < movieTemp.Count; i++)
            {
                for (int j = 0; j < movieTemp.Count; j++)
                {
                    if (i != j)
                    {
                        adjacentActors[actorsId[movieTemp[i]]].Add(actorsId[movieTemp[j]]);
                    }
                }
                actorMovies[actorsId[movieTemp[i]]].Add(movieCount-1);
            }

        }
        public int getActorID(string actorName)
        {
            return actorsId[actorName];
        }
        public string getActorName(int actorID)
        {
            return actorNames[actorID];
        }

        public List<int> getAdjacent(int vertex)
        {
            return adjacentActors[vertex]; 
        }
        public string getCommonMovie(int source, int destination)
        {
            int fIter = 0;
            int sIter = 0;
            var result = new List<int>();
            while (fIter < actorMovies[source].Count && sIter < actorMovies[destination].Count)
            {
                if (actorMovies[source][fIter] < actorMovies[destination][sIter])
                {
                    fIter++;
                }
                else if (actorMovies[source][fIter] > actorMovies[destination][sIter])
                {
                    sIter++;
                }
                else
                {
                    result.Add(actorMovies[source][fIter]);
                    fIter++;
                    sIter++;
                }
            }
            return moviesNames[result[0]];
        }

        public int getAdjacentWeight(int source, int destination)//get intersection between 2 lists
        {
            int fIter = 0;
            int sIter = 0;
            var result = new List<int>();
            while (fIter < actorMovies[source].Count && sIter < actorMovies[destination].Count)
            {
                if (actorMovies[source][fIter] < actorMovies[destination][sIter])
                {
                    fIter++;
                }
                else if (actorMovies[source][fIter] > actorMovies[destination][sIter])
                {
                    sIter++;
                }
                else
                {
                    result.Add(actorMovies[source][fIter]);
                    fIter++;
                    sIter++;
                }
            }
            return result.Count;           
        }        

    }
}