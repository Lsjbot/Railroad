using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class nodeclass
    {
        public static int nodecount = 0;

        public int ID;
        public Point mapsquare;
        Boolean active = true;
        public nodetypeclass nodetype;

        public List<int> outedge = new List<int>();
        public List<int> inedge = new List<int>();

        public string name = "";

        public nodeclass(Point msq, string namepar)
        {
            mapsquare = msq;
            name = namepar;
        }

        public List<string> sides(bool incoming)
        {
            if (incoming)
                return nodetype.inconn;
            else
                return nodetype.outconn;
        }

        public void addedge(int iedge,Boolean incoming)
        {
            if (incoming)
                inedge.Add(iedge);
            else
                outedge.Add(iedge);
        }

        public void refreshconnections(mapclass map)
        {
            foreach (string side in nodetype.inconn)
            {
                int othernode = map.follow_track_to_node(this.mapsquare, side, true);
                Console.WriteLine("othernode in: " + othernode);
                if (othernode > 0)
                {
                    bool found = false;
                    foreach (int iedge in inedge)
                        if ((map.railgraph.edgedict[iedge].fromnode == othernode)
                            && (map.railgraph.edgedict[iedge].tonode == this.ID))
                            found = true;
                    if (!found)
                    {
                        map.railgraph.addedge(othernode, this.ID, this.ID, side, map);
                    }
                }
            }
            foreach (string side in nodetype.outconn)
            {
                int othernode = map.follow_track_to_node(this.mapsquare, side, false);
                Console.WriteLine("othernode out: " + othernode);
                if (othernode > 0)
                {
                    bool found = false;
                    foreach (int iedge in outedge)
                        if ((map.railgraph.edgedict[iedge].tonode == othernode)
                            && (map.railgraph.edgedict[iedge].fromnode == this.ID))
                            found = true;
                    if (!found)
                    {
                        map.railgraph.addedge(this.ID, othernode, this.ID, side, map);
                    }
                }
            }
        }
    }
}
