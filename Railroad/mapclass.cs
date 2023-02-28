using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class mapclass
    {
        public static string mainmaptype = "main map";
        public static string stationmaptype = "station map";

        public squareclass[,] sq;
        public int mapsizex;
        public int mapsizey;
        public RectangleF latlonrect;
        public double km_per_pixel = 0;
        public List<stationclass> stationlist = new List<stationclass>();

        public string maptype = "";

        public railgraphclass railgraph = new railgraphclass();
        
        public mapclass(int mapsizepar, string typepar)
        {
            mapsizex = mapsizepar;
            mapsizey = mapsizepar;
            maptype = typepar;
            sq = new squareclass[mapsizex, mapsizey];
        }

        public mapclass(int mapsizeparx,int mapsizepary,string typepar)
        {
            mapsizex = mapsizeparx;
            mapsizey = mapsizepary;
            maptype = typepar;
            sq = new squareclass[mapsizex, mapsizey];
        }


        public void randommap()
        {
            int nterr = terrainclass.terrainlist.Count;
            Random rnd = new Random();
            for (int i = 0; i < mapsizex; i++)
            {
                for (int j = 0; j < mapsizey; j++)
                {
                    int jt = rnd.Next(nterr);
                    sq[i, j] = new squareclass(terrainclass.terrainlist[jt], 0,new Point(i,j));
                }
                //Console.WriteLine("maprow " + i);
            }
        }

        public void flatmap(terrainclass tc)
        {

            for (int i = 0; i < mapsizex; i++)
            {
                for (int j = 0; j < mapsizey; j++)
                {
                    sq[i, j] = new squareclass(tc, 0, new Point(i, j));
                }
                //Console.WriteLine("maprow " + i);
            }

        }

        public bool inmap(Point xy)
        {
            return inmap(xy.X, xy.Y);
        }

        public bool inmap(int x, int y)
        {
            if (x < 0)
                return false;
            if (y < 0)
                return false;
            if (x >= mapsizex)
                return false;
            if (y >= mapsizey)
                return false;
            return true;
        }

        public squareclass square(Point p)
        {
            return this.sq[p.X, p.Y];
        }

        public Point coordfromlatlon(double lat, double lon)
        {
            int x = (int)Math.Round(mapsizex * (lon - latlonrect.Left) / latlonrect.Width);
            int y = (int)Math.Round(mapsizey * (latlonrect.Bottom - lat) / latlonrect.Height);
            return new Point(x, y);

        }

        public void addname(cityclass cc)
        {
            Point xy = coordfromlatlon(cc.lat, cc.lon);
            if (inmap(xy))
            {
                this.sq[xy.X, xy.Y].placename = cc.name;
            }
        }

        public void addrail(Point mapsquare, railclass rail)
        {
            this.sq[mapsquare.X, mapsquare.Y].addrail(rail,this);
            
        }

        public int follow_track_to_node(Point start, string side, bool incoming)
        {
            return follow_track_to_node(start, side, incoming, 0);
        }

        Point nullpoint = new Point(-1, -1);
        public int follow_track_to_node(Point start, string side, bool incoming, int nedge)
        {
            return follow_track_to_node(start, side, incoming, nedge, nullpoint);
        }

        public int follow_track_to_node(Point start,string side, bool incoming,int nedge,Point origin)
        {
            //follow track until next square with a node.
            //if nedge > 0, mark each track with nedge
            //if nedge < 0, remove mark from track
            //if nedge == 0, do not mark

            if (start.Equals(origin)) //loop
                return -1;
            if (origin.Equals(nullpoint))
                origin = start;
            trackconnectionclass tc = (from c in railclass.trackconnections
                                       where c.side == side
                                       select c).First();

            Point newsquare = new Point(start.X + tc.u, start.Y + tc.v);

            if (!inmap(newsquare))
                return -1;
            else if (sq[newsquare.X, newsquare.Y].rail == null)
                return -1;
            else if (sq[newsquare.X, newsquare.Y].nodelist.Count > 0)
            {
                foreach (int inode in sq[newsquare.X, newsquare.Y].nodelist)
                {
                    if (railgraph.nodedict[inode].sides(!incoming).Contains(railclass.rot180dict[side])) //incoming in source node are outgoing in target node
                    {
                        return inode;
                    }
                }
                return -1; //when connecting node not found
            }
            else
            {
                if (nedge > 0)
                    sq[newsquare.X, newsquare.Y].edgelist.Add(nedge);
                else if ( nedge < 0)
                    sq[newsquare.X, newsquare.Y].edgelist.Remove(-nedge);
                string newside = side;
                foreach (squaretrackclass st in sq[newsquare.X, newsquare.Y].rail.tracks)
                    for (int end = 0; end < 2; end++)
                        if (st.conn[end].side == railclass.rot180dict[side])
                        {
                            newside = st.conn[1 - end].side;
                            break;
                        }
                return follow_track_to_node(newsquare, newside, incoming,nedge,origin);
            }
        }
    }
}
