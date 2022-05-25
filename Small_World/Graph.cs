using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;


namespace Small_World
{
    class Graph
    {
        public Dictionary<string, int> actorsId = new Dictionary<string, int>();//O(1)
        public Dictionary<string, int> movieId = new Dictionary<string, int>();//O(1)
        List<List<int>> adjacentActors = new List<List<int>>();//O(1)
        public static List<List<int>> actorMovies = new List<List<int>>();//O(1)
        List<string> actorNames = new List<string>();//O(1)
        List<string> moviesNames = new List<string>();//O(1)
        public int actorCount = 0;//O(1)
        public int movieCount = 0;//O(1)

        public Graph(string fileName)//O(N^3) xx
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);//O(1)
            StreamReader sr = new StreamReader(file);//O(1)
            string line;
            Console.Write("start \n");
            while ((line = sr.ReadLine()) != null)//O(N^3) 
            {
                List<string> names = new List<string>(line.Split('/'));//O(N)
                initializeEdges(names);//O(N^2)
            }
        }

        void initializeEdges(List<string> movieTemp)//O(N^2) 
        {
            string movieName = movieTemp[0];
            movieTemp.RemoveAt(0);//O(1)
            movieId[movieName] = movieCount;
            moviesNames.Add(movieName);//O(1)
            movieCount++;
            for (int i = 0; i < movieTemp.Count; i++)//O(N)
            {   
                if(!actorsId.ContainsKey(movieTemp[i]))//O(1)
                {
                    actorsId.Add(movieTemp[i], actorCount);//O(1)
                    actorNames.Add(movieTemp[i]);//O(1)
                    adjacentActors.Add(new List<int>());//O(1)
                    actorMovies.Add(new List<int>());//O(1)
                    actorCount++;
                }                
            }
            for (int i = 0; i < movieTemp.Count; i++)//O(N^2)
            {
                for (int j = 0; j < movieTemp.Count; j++)//O(N)
                {
                    if (i != j)
                    {
                        adjacentActors[actorsId[movieTemp[i]]].Add(actorsId[movieTemp[j]]);//O(1)
                    }
                }
                actorMovies[actorsId[movieTemp[i]]].Add(movieCount-1);//O(1)
            }

        }
        public int getActorID(string actorName)//O(1)
        {
            return actorsId[actorName];
        }
        public string getActorName(int actorID)//O(1)
        {
            return actorNames[actorID];
        }

        public List<int> getAdjacent(int vertex)//O(1)
        {
            return adjacentActors[vertex]; 
        }
        public string getCommonMovie(int source, int destination)//O(N)
        {
           
            List<int> result = new List<int>();
            int first = 0;
            int second = 0;
            while (first < actorMovies[source].Count && second < actorMovies[destination].Count)//O(N)
            {
                if (actorMovies[source][first] > actorMovies[destination][second])//O(1)
                {
                    second++; 
                }
                else if (actorMovies[source][first] < actorMovies[destination][second])//O(1)
                {
                    first++;
                }
                else //O(1)
                {
                    result.Add(actorMovies[source][first]);
                    first++;
                    second++;
                }
            }
            int temp = result[0];
            return moviesNames[temp];
        }

        public int getAdjacentWeight(int source, int destination)//get intersection between 2 lists//O(N) -------------> T(d+s)
        {
            
            List<int> result = new List<int>();
            int first = 0;
            int second = 0;
            while (first < actorMovies[source].Count && second < actorMovies[destination].Count)//O(N) -------------> T(d+s)
            {
                if (actorMovies[source][first] > actorMovies[destination][second])//O(1)
                {
                    second++;
                }
                else if (actorMovies[source][first] < actorMovies[destination][second])//O(1)
                {
                    first++;
                }
                else //O(1)
                {
                    result.Add(actorMovies[source][first]);
                    first++;
                    second++;
                }
            }
            return result.Count;           
        }        

    }
}