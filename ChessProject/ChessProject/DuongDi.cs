using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Algorithms.Observers;

namespace ChessProject
{
    class DuongDi
    {
        private int kt;//Kích thước bàn cờ
        private int x, y;//Tọa độ xuất phát ban đầu x,y
        private int[,] dd = new int[2501, 2501];
        private int[,] kn = new int[2501, 2501];
        private int[,] di = new int[3, 9];//Toa do duong di quan ma.
        public int[,] vt = new int[3, 2501];
        public int[,] _vt = new int[3, 2501];//Tạm thời
        private bool ngung = false;
        public int sobd;
        private AdjacencyGraph<string, Edge<string>> graphs;
        private Dictionary<string, double> costHeur;

        public void Set(int _kt, int _x, int _y)
        {
            kt = _kt;
            x = _x;
            y = _y;
        }

        public int ktkn(int i, int j)
        {
            int dem = 0;
            for (int l = 1; l <= 8; l++)
            {
                if (i + di[1, l] >= 1 && i + di[1, l] <= kt && j + di[2, l] >= 1 && j + di[2, l] <= kt)
                {
                    dem++;
                    var e = new Edge<string>(i.ToString() + "-" + j.ToString(), (i + di[1, l]).ToString() + "-" + (j + di[2, l]).ToString());
                    graphs.AddVerticesAndEdge(e);

                }
            }
            return dem;
        }

        public void khoiTao()
        {
            sobd = 0;
            ngung = false;
            graphs = new AdjacencyGraph<string, Edge<string>>();
            //Reset Variable

            //khoi tao
            for (int i = 1; i <= kt * kt; i++)
            {
                vt[1, i] = vt[2, i] = 0;
                _vt[1, i] = _vt[2, i] = 0;
            }
            for (int i = 1; i <= kt; i++)
                for (int j = 1; j <= kt; j++) dd[i, j] = 0;
            di[1, 1] = -1; di[2, 1] = -2;
            di[1, 2] = 1; di[2, 2] = -2;
            di[1, 3] = -1; di[2, 3] = 2;
            di[1, 4] = 1; di[2, 4] = 2;
            di[1, 5] = -2; di[2, 5] = -1;
            di[1, 6] = -2; di[2, 6] = 1;
            di[1, 7] = 2; di[2, 7] = -1;
            di[1, 8] = 2; di[2, 8] = 1;
            for (int i = 1; i <= kt; i++)
            {
                for (int j = 1; j <= kt; j++)
                {
                    kn[i, j] = ktkn(i, j);
                }
            }

        }

        public bool Dijkstra(int kt_x, int kt_y)
        {
            khoiTao(); //Khoi tao do thi.
            Func<Edge<string>, double> edCost = (edge => 1.0D); //Gan theo kieu hang so.
            string root = x.ToString() + "-" + y.ToString();
            string end = kt_x.ToString() + "-" + kt_y.ToString();

            var algo = new DijkstraShortestPathAlgorithm<string, Edge<string>>(graphs, edCost); //Khai bao thuat toan

            var predecessors = new VertexPredecessorRecorderObserver<string, Edge<string>>(); // Khai bao bien quan sat
            using (predecessors.Attach(algo)) //Gan thuat toan vao bien quan sat
            {
                algo.Compute(root); //Chay thuat toan
                IEnumerable<Edge<string>> path;
                if (predecessors.TryGetPath(end, out path))
                {
                    int i = 1;
                    foreach (var item in path)
                    {
                        string[] vertex = item.Source.Split('-');
                        vt[1, i] = int.Parse(vertex[0]);
                        vt[2, i] = int.Parse(vertex[1]);
                        if (i == path.Count())
                        {
                            string[] lastVertex = item.Target.Split('-');
                            vt[1, i + 1] = int.Parse(lastVertex[0]);
                            vt[2, i + 1] = int.Parse(lastVertex[1]);
                        }
                        i++;
                    }
                    sobd = i + 1;
                    if (path.Count() > 0)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public bool AStar(int kt_x, int kt_y)
        {
            khoiTao();
            Func<Edge<string>, double> edCost = (edge => 1.0D);
            costHeur = new Dictionary<string, double>();
            for (int i = 1; i <= kt; i++)
                for (int j = 1; j <= kt; j++)
                    costHeur.Add(i.ToString() + "-" + j.ToString(), kn[i, j]);
            Func<string, double> cost = new Func<string, double>(calHeuristic);

            string root = x.ToString() + "-" + y.ToString();
            string end = kt_x.ToString() + "-" + kt_y.ToString();
            var algo = new AStarShortestPathAlgorithm<string, Edge<string>>(graphs, edCost, cost);

            var predecessors = new VertexPredecessorRecorderObserver<string, Edge<string>>();
            using (predecessors.Attach(algo))
            {
                algo.Compute(root);
                IEnumerable<Edge<string>> path;
                if (predecessors.TryGetPath(end, out path))
                {
                    int i = 1;
                    foreach (var item in path)
                    {
                        string[] vertex = item.Source.Split('-');
                        vt[1, i] = int.Parse(vertex[0]);
                        vt[2, i] = int.Parse(vertex[1]);
                        if (i == path.Count())
                        {
                            string[] lastVertex = item.Target.Split('-');
                            vt[1, i + 1] = int.Parse(lastVertex[0]);
                            vt[2, i + 1] = int.Parse(lastVertex[1]);
                        }
                        i++;
                    }
                    sobd = i + 1;
                    if (path.Count() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        double calHeuristic(string str)
        {
            var cost = costHeur.Where(x => x.Key == str).Single();
            return cost.Value;
        }

        public int kiemtra()
        {
            for (int i = 1; i <= kt; i++)
                for (int j = 1; j <= kt; j++) if (dd[i, j] == 0) return 0;
            return 1;
        }
    }
}
