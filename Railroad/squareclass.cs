using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class squareclass
    {
        public static int squaresize = 32;

        //Image image;
        public Bitmap pic; //current appearance
        public Bitmap cleanpic; //appearance with tracks and buildings but without train
        public Bitmap basepic; //empty lot
        public bool changedpic = true;
        public railclass rail = null;
        public stationclass station = null;
        private riverclass river = null;
        public int altitude;
        public terrainclass terrain;
        public playerclass owner = null;
        public List<int> nodelist = new List<int>(); //index to railgraphclass.nodedict
        public List<int> edgelist = new List<int>(); //index to railgraphclass.edgedict
        public Point coord;
        public string placename = "";

        public squareclass(terrainclass terr, int alt, Point coordpar)
        {
            terrain = terr;
            altitude = alt;
            coord = coordpar;
            basepic = (Bitmap)terrain.getrandompic();
            pic = (Bitmap)basepic.Clone();
        }

        public void remove_rail(mapclass map)
        {
            if (edgelist.Count > 0)
            {
                int[] dummyedge = new int[edgelist.Count];
                this.edgelist.CopyTo(dummyedge);
            foreach (int nedge in dummyedge)
                {
                    map.railgraph.removeedge(nedge, map);
                }
            }
            if (nodelist.Count > 0)
            {
                int[] dummynode = new int[nodelist.Count];
                this.nodelist.CopyTo(dummynode);
                foreach (int inode in dummynode)
                {
                    map.railgraph.removenode(inode, map);
                }
            }
            pic = (Bitmap)basepic.Clone();
            rail = null;
        }

        public void addnodes(railclass newrail, mapclass map)
        {
            foreach (nodetypeclass nt in newrail.nodetypes)
            {
                nodeclass nc = new nodeclass(this.coord, map.square(this.coord).placename);
                nc.ID = map.railgraph.addnode(nc);
                nc.nodetype = nt;
                this.nodelist.Add(nc.ID);
                foreach (string side in nt.inconn)
                {
                    int othernode = map.follow_track_to_node(this.coord, side, true);
                    Console.WriteLine("othernode in: " + othernode);
                    if (othernode > 0)
                    {
                        int newedge = map.railgraph.addedge(othernode, nc.ID, nc.ID, side, map);
                    }
                }
                foreach (string side in nt.outconn)
                {
                    int othernode = map.follow_track_to_node(this.coord, side, false);
                    Console.WriteLine("othernode out: " + othernode);
                    if (othernode > 0)
                    {
                        int newedge = map.railgraph.addedge(nc.ID, othernode, nc.ID, side, map);
                    }
                }
            }

        }

        public void addrail(railclass newrail, mapclass map)
        {
            if (rail != null)
                remove_rail(map);
            rail = newrail.Clone();
            pic = rail.overlay_template(pic);
            cleanpic = (Bitmap)pic.Clone();

            foreach (squaretrackclass st in rail.tracks)
            {
                int x0 = coord.X + st.conn[0].u;
                int y0 = coord.Y + st.conn[0].v;
                int x1 = coord.X + st.conn[1].u;
                int y1 = coord.Y + st.conn[1].v;
                int alt0 = 0;
                if (map.inmap(x0,y0))
                    alt0 = map.sq[x0, y0].altitude;
                int alt1 = 0;
                if (map.inmap(x1,y1))
                    alt1 = map.sq[x1, y1].altitude;
                double slope = 100*(alt1 - alt0) / (2000 * map.km_per_pixel);
                Console.WriteLine("slope = " + slope + " %");
            }

            if (newrail.nodetypes.Count > 0) 
            {
                addnodes(newrail,map);
            }
        }

        public void showtrain(trainclass train, int x, int y, float angle)
        {
            pic = train.engine.enginetype.overlay_template(cleanpic, x, y, angle);
        }

        public void restore_cleanpic()
        {
            pic = (Bitmap)cleanpic.Clone();
        }
    }
}
