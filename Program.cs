using System;
using System.Collections.Generic;

namespace Medvedev
{
    class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            graph[0] = new List<int>() {1, 2 };
            graph[1] = new List<int>() { 0, 3};
            graph[2] = new List<int> { 0, 3, 4 };
            graph[3] = new List<int> { 1, 2, 5 };
            graph[4] = new List<int> { 2, 5 };
            graph[5] = new List<int> { 3, 4 };

            Console.Write("ВВедите начало: ");
            int start = int.Parse(Console.ReadLine());

            Console.Write("Конец: ");
            int end = int.Parse(Console.ReadLine());

            List<int> path = BfsShortestPath(graph, start, end);

            if (path == null) { Console.WriteLine($" Пути из: {start}, в :{end} несуществует"); }

            else
            {
                Console.Write("Кратчайший путь: ");

                for (int i =0;  i < path.Count; i++)
                {
                    if (i > 0) Console.Write("-");
                    Console.Write(path[i]);
                }

                Console.WriteLine($"  (длина: {path.Count - 1} рёбер)");
            }
        }

        public static List<int> BfsShortestPath(Dictionary<int, List<int>> graph, int start, int end)
        {
            if (start == end) return new List<int> { start };

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);

            HashSet<int> visited = new HashSet<int>();
            visited.Add(start);

            Dictionary<int, int> parent = new Dictionary<int, int>();//для востановления пути
            parent[start] = -1; 

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                if(current == end) return ReconstructPath(parent, start, end);

                List<int> neighbors = graph[current];

                for (int i = 0; i < neighbors.Count; i++)
                {
                    int next = neighbors[i];

                    if (visited.Contains(next)) continue;

                    visited.Add(next);

                    parent[next] = current;

                    queue.Enqueue(next);

                }
            }

            return null;
        }

        public static List<int> ReconstructPath(Dictionary<int, int> parent, int start, int end)
        {
            List<int> path = new List<int>();

            int current = end;

            while (current != -1)
            {
                path.Add(current);
                current = parent[current];
            }

            path.Reverse();
            return path;
        }
    }
}