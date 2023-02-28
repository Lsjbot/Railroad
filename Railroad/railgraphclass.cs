using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph;
using QuikGraph.Algorithms;
using QuikGraph.MSAGL;
using Microsoft.Msagl;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Drawing;

namespace Railroad
{
    public class railgraphclass
    {
        public Dictionary<int, nodeclass> nodedict = new Dictionary<int, nodeclass>();
        public Dictionary<int, edgeclass> edgedict = new Dictionary<int, edgeclass>();
        static Dictionary<Edge<int>, double> edgeCostdict = new Dictionary<Edge<int>, double>();
        Dictionary<Edge<int>, int> edgeintdict = new Dictionary<Edge<int>, int>();
        Func<Edge<int>, double> edgeCost = AlgorithmExtensions.GetIndexer(edgeCostdict);
        AdjacencyGraph<int, Edge<int>> graph = new AdjacencyGraph<int, Edge<int>>();
        bool quikgraph_updated = false;

        public void test()
        {
            var graph = new AdjacencyGraph<Point,Edge<Point>>();
            //IEdgeListGraph<int, Edge<int>> xx = new IEdgeListGraph<int, Edge<int>>();
            //graph.ToMsaglGraph

            Dictionary<int, int[]> dict = new Dictionary<int, int[]>();

            //var graph = GraphExtensions.ToDelegateVertexAndEdgeListGraph<int,int>(dict, kv => Array.ConvertAll(kv.Value, v => new Edge<int>(kv.Key,v)));
            
        }

        private void update_quikgraph()
        {
            edgeCostdict.Clear();
            edgeintdict.Clear();
            graph.Clear();
            foreach (int i in edgedict.Keys)
            {
                QuikGraph.Edge<int> edge = new Edge<int>(edgedict[i].fromnode, edgedict[i].tonode);
                graph.AddVerticesAndEdge(edge);
                edgeCostdict.Add(edge, edgedict[i].traveltime);
                edgeintdict.Add(edge, i);
            }
            quikgraph_updated = true;
        }

        public double find_route(string startstation, string endstation, mapclass map, out int[] edges)
        {
            double minpath = double.MaxValue;
            int minnode = -1;
            int[] minedges = null;
            int[] routeedges = null;
            foreach (int inode in map.square(stationclass.stationdict[startstation].location).nodelist)
            {
                double pathlength = find_route(inode, endstation, map, out routeedges);
                if (pathlength < minpath)
                {
                    minpath = pathlength;
                    minnode = inode;
                    minedges = routeedges;
                }
            }
            if (minnode >= 0)
            {
                edges = routeedges;
                return minpath;
            }
            else
            {
                edges = null;
                return -1;
            }
        }

        public double find_route(int startnode, string endstation, mapclass map, out int[] edges)
        {
            //returns length of shortest route from startnode to either of endstation's nodes

            if (!quikgraph_updated)
                update_quikgraph();

            TryFunc<int, IEnumerable<Edge<int>>> tryGetPaths = graph.ShortestPathsDijkstra(edgeCost, startnode);

            double minpath = double.MaxValue;
            int minnode = -1;
            IEnumerable<Edge<int>> minedges = null;
            foreach (int inode in map.square(stationclass.stationdict[endstation].location).nodelist)
            {
                // Query path for given vertices
                if (tryGetPaths(inode, out IEnumerable<Edge<int>> path))
                {
                    double pathlength = 0;
                    foreach (Edge<int> edge in path)
                    {
                        Console.WriteLine(edge);
                        pathlength += edgeCostdict[edge];
                    }
                    if (pathlength < minpath)
                    {
                        minpath = pathlength;
                        minnode = inode;
                        minedges = path;
                    }
                }
            }

            //if (tryGetPaths(minnode, out IEnumerable<Edge<int>> path2))
            //{
            //    edges = new int[path2.Count()];
            //    //double pathlength = 0;
            //    int i = 0;
            //    foreach (Edge<int> edge in path2)
            //    {
            //        //Console.WriteLine(edge);
            //        //pathlength += edgeCostdict[edge];
            //        edges[i] = edgeintdict[edge];
            //    }

            //    return minpath;
            //}
            if (minnode >= 0)
            {
                edges = new int[minedges.Count()];
                //double pathlength = 0;
                int i = 0;
                foreach (Edge<int> edge in minedges)
                {
                    //Console.WriteLine(edge);
                    //pathlength += edgeCostdict[edge];
                    edges[i] = edgeintdict[edge];
                    i++;
                }
                return minpath;
            }
            edges = null;
            return -1;

        }



        public void drawgraph()
        {
            //var graph = new AdjacencyGraph<int, Edge<int>>();
            //foreach (int i in edgedict.Keys)
            //{
            //    QuikGraph.Edge<int> edge = new Edge<int>(edgedict[i].fromnode, edgedict[i].tonode);
            //    graph.AddVerticesAndEdge(edge);
            //}

            //var msaglgraph = graph.ToMsaglGraph();

            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph msaglgraph = new Microsoft.Msagl.Drawing.Graph("graph");
            ////create the graph content 

            Dictionary<int, string> nodelabels = new Dictionary<int, string>();
            foreach (int i in nodedict.Keys)
            {
                string nodetext = nodedict[i].name;
                if (String.IsNullOrEmpty(nodetext))
                    nodetext = nodedict[i].mapsquare.X + "," + nodedict[i].mapsquare.Y;
                nodelabels.Add(i, i.ToString() + " (" + nodetext + ")");
                msaglgraph.AddNode(new Node(nodelabels[i]));
            }
            foreach (int i in edgedict.Keys)
            {
                //QuikGraph.Edge<int> edge = new Edge<int>(edgedict[i].fromnode, edgedict[i].tonode);
                msaglgraph.AddEdge(nodelabels[edgedict[i].fromnode], nodelabels[edgedict[i].tonode]);
            }
            //graph.AddEdge("A", "B");
            //graph.AddEdge("B", "C");
            //graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            //graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            //graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            //Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            //c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            //c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            //bind the graph to the viewer  
            viewer.Graph = msaglgraph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.Show();
        }

        public int addnode(nodeclass newnode)
        {
            int id = nodedict.Count + 1;
            nodedict.Add(id, newnode);
            quikgraph_updated = false;
            return id;
        }

        public void removenode(int inode, mapclass map)
        {
            foreach (int nedge in nodedict[inode].inedge)
                removeedge(nedge, map);
            foreach (int nedge in nodedict[inode].outedge)
                removeedge(nedge, map);
            map.sq[nodedict[inode].mapsquare.X, nodedict[inode].mapsquare.Y].nodelist.Remove(inode);
            nodedict.Remove(inode);
            quikgraph_updated = false;
        }

        public int addedge(int fromnode, int tonode, int sidenode, string side, mapclass map)
        {
            int id = 1;
            if (edgedict.Count > 0)
                id = edgedict.Keys.Max() + 1;
            edgeclass newedge = new edgeclass();
            newedge.ID = id;
            newedge.fromnode = fromnode;
            newedge.tonode = tonode;
            newedge.sidenode = sidenode;
            newedge.side = side;
            edgedict.Add(id, newedge);
            nodedict[fromnode].addedge(id, false);
            nodedict[tonode].addedge(id, true);
            map.square(nodedict[fromnode].mapsquare).edgelist.Add(newedge.ID);
            map.square(nodedict[tonode].mapsquare).edgelist.Add(newedge.ID);
            bool incoming = (tonode == sidenode);
            map.follow_track_to_node(nodedict[sidenode].mapsquare,side, incoming,newedge.ID);
            quikgraph_updated = false;
            return id;
        }

        public int addedge(int fromnode, int tonode) //for internal edges only, between two nodes in same square
        {
            int id = 1;
            if (edgedict.Count > 0)
                id = edgedict.Keys.Max() + 1;
            edgeclass newedge = new edgeclass();
            newedge.ID = id;
            newedge.fromnode = fromnode;
            newedge.tonode = tonode;
            newedge.sidenode = -1;
            newedge.side = "";
            edgedict.Add(id, newedge);
            nodedict[fromnode].addedge(id, false);
            nodedict[tonode].addedge(id, true);
            quikgraph_updated = false;
            return id;
        }

        public void removeedge(int nedge, mapclass map)
        {
            nodedict[edgedict[nedge].fromnode].outedge.Remove(nedge);
            nodedict[edgedict[nedge].tonode].inedge.Remove(nedge);
            if (edgedict[nedge].sidenode >= 0)
            {
                bool incoming = (edgedict[nedge].tonode == edgedict[nedge].sidenode);
                map.follow_track_to_node(nodedict[edgedict[nedge].sidenode].mapsquare, edgedict[nedge].side, incoming, -edgedict[nedge].ID);
            }
            edgedict.Remove(nedge);
            quikgraph_updated = false;
        }

        public void refresh_all_edges(mapclass map)
        {
            foreach (int iedge in edgedict.Keys)
                if (edgedict[iedge].sidenode >= 0)
                    removeedge(iedge, map);
            foreach (int inode in nodedict.Keys)
                nodedict[inode].refreshconnections(map);
            quikgraph_updated = false;
        }
    }
}
