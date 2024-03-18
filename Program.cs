using System;
using System.Collections.Generic;
using System.Text;

class Graph
{
    private int V; // Số đỉnh
    private List<int>[] adj; // Danh sách kề

    public Graph(int v)
    {
        V = v;
        adj = new List<int>[v];
        for (int i = 0; i < v; ++i)
            adj[i] = new List<int>();
    }

    // Thêm cạnh vào đồ thị
    public void AddEdge(int v, int w)
    {
        adj[v].Add(w);
    }

    // Tìm kiếm đường đi ngắn nhất từ đỉnh s đến đỉnh t
    public void ShortestPath(int s, int t)
    {
        bool[] visited = new bool[V];
        int[] distance = new int[V];
        int[] prev = new int[V];

        Queue<int> queue = new Queue<int>();
        queue.Enqueue(s);
        visited[s] = true;
        distance[s] = 0;
        prev[s] = -1;

        while (queue.Count != 0)
        {
            int u = queue.Dequeue();

            foreach (int v in adj[u])
            {
                if (!visited[v])
                {
                    visited[v] = true;
                    distance[v] = distance[u] + 1;
                    prev[v] = u;
                    queue.Enqueue(v);
                }
            }
        }

        Console.WriteLine("Đường đi ngắn nhất từ " + s + " đến " + t + " là:");
        PrintPath(prev, t);
    }

    // In đường đi từ đỉnh s đến đỉnh t
    private void PrintPath(int[] prev, int t)
    {
        if (prev[t] == -1)
        {
            Console.Write(t + " ");
            return;
        }
        PrintPath(prev, prev[t]);
        Console.Write(t + " ");
    }

    // Kiểm tra tính liên thông của đồ thị
    public bool IsConnected()
    {
        bool[] visited = new bool[V];

        // Chọn một đỉnh làm điểm bắt đầu
        int start = 0;
        for (int i = 0; i < V; i++)
        {
            if (adj[i].Count > 0)
            {
                start = i;
                break;
            }
        }

        // Duyệt đồ thị từ đỉnh bắt đầu
        BFS(start, visited);

        // Kiểm tra xem tất cả các đỉnh có được duyệt không
        for (int i = 0; i < V; i++)
        {
            if (!visited[i] && adj[i].Count > 0)
                return false;
        }

        return true;
    }

    // Duyệt đồ thị bằng BFS
    private void BFS(int s, bool[] visited)
    {
        Queue<int> queue = new Queue<int>();
        visited[s] = true;
        queue.Enqueue(s);

        while (queue.Count != 0)
        {
            int u = queue.Dequeue();

            foreach (int v in adj[u])
            {
                if (!visited[v])
                {
                    visited[v] = true;
                    queue.Enqueue(v);
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Graph g = new Graph(5);
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 3);
        g.AddEdge(2, 4);

        // Kiểm tra tính liên thông của đồ thị
        if (g.IsConnected())
            Console.WriteLine("Đồ thị liên thông");
        else
            Console.WriteLine("Đồ thị không liên thông");

        // Tìm đường đi ngắn nhất từ đỉnh 0 đến đỉnh 3
        g.ShortestPath(0, 3);
    }
}
