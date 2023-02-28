using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Railroad
{
    public class trainclass
    {
        public static List<enginetypeclass> enginetypes = new List<enginetypeclass>();
        public static List<cartypeclass> cartypes = new List<cartypeclass>();
        public static int maxid = 0;
        public static Point nullpoint = new Point(-1, -1);

        public int id;
        public string name = "";
        public engineclass engine = null;
        public List<carclass> cars = new List<carclass>();

        public mapclass map; //which map is the train on
        public Point location = nullpoint; //which square is the engine front in
        public Point oldloc = nullpoint; //which square was the engine in before
        public double fraction = 0; //fractional location through a square
        public int u; //direction to next square
        public int v; //direction to next square
        public int u0; //direction from last square
        public int v0; //direction from last square
        public float angle; //current direction of engine
        public double speed = 0;
        public static double refspeed = 200; //200 km/h = 1 square per second at 100 ms per tick
        public static double reftick = 100;  //100 ms per tick default game rate

        public stationclass station = null; //not null if train inside station

        public string[] itinerary = null;
        public int nextstop; //index to itinerary
        public int direction = 1; //+1 to move up the itinerary, -1 to return down

        public int stationcountdown = 0; //how much time (ticks) remaining at station

        public int[] edgeroute; //which edges in the graph should the train traverse to get to its next stop
        public int currentedge; //index to edgeroute

        static int maxpos = 1000;
        public trainposclass[] poshistory = new trainposclass[maxpos];
        public int ipos = 0;

        public static void inittrains()
        {
            enginetypes.Add(new enginetypeclass("Base engine", 8, 16));

            cartypes.Add(new cartypeclass("Base car"));
        }

        public trainclass(mapclass mappar)
        {
            maxid++;
            id = maxid;
            map = mappar;
        }

        public override string ToString()
        {
            string s = id.ToString()+" "+name+":";

            return s;
        }

        public bool set_itinerary() //returns true if successful
        {
            string oldnextstop = itinerary == null ? "" : itinerary[nextstop]; 
            FormItinerary fi = new FormItinerary(itinerary,nextstop);
            fi.ShowDialog();
            if (fi.DialogResult == DialogResult.OK)
            {
                this.itinerary = fi.itinerary();
                this.nextstop = fi.nextstop();
                if (this.location.Equals(nullpoint)) //new train
                {
                    this.station = stationclass.stationdict[this.itinerary[0]];
                    this.location = this.station.location;
                    this.poshistory[0] = new trainposclass(this.location, 15, 15, 0);
                    int[] edges;
                    double dist = map.railgraph.find_route(this.itinerary[0], this.itinerary[1], map, out edges);
                    if (dist >= 0)
                    {
                        edgeroute = edges;
                        currentedge = 0;
                        stationcountdown = 5;
                    }
                    else
                    {
                        //Something is wrong
                        throw new Exception("No route to station");
                    }
                }
                else //train already en route somewhere
                {
                    if (itinerary[nextstop] != oldnextstop) //re-route!
                    {
                        int e = edgeroute[currentedge];
                        int startnode = map.railgraph.edgedict[e].fromnode;
                        int[] edges;
                        double dist = map.railgraph.find_route(startnode, itinerary[nextstop], map, out edges);
                        if ( dist >= 0)
                        {
                            edgeroute = edges;
                            currentedge = 0;
                        }
                        else
                        {
                            //Something is wrong
                            throw new Exception("No route to station");
                        }

                    }
                }


                return true;
            }
            else
                return false;
        }

        public void prepare_departure()
        {

            Msg.memo("Train " + this.id + " departing from " + map.square(location).station.name);
            nextstop++;
            if (nextstop >= itinerary.Length)
                nextstop = 0;
            int[] edges;
            double dist = map.railgraph.find_route(this.station.name, this.itinerary[nextstop], map, out edges);
            if (dist >= 0)
            {
                edgeroute = edges;
                foreach (int i in edgeroute)
                    Msg.memo("edge " + i);
                currentedge = 0;
                //stationcountdown = 5;
                int newedge = edgeroute[currentedge];

                int nextnode = map.railgraph.edgedict[newedge].tonode;
                if (map.square(location).nodelist.Contains(nextnode))
                {
                    //turn around train. Should handle turning time somehow
                    currentedge++;
                    newedge = edgeroute[currentedge];
                }
                foreach (squaretrackclass tc in map.square(location).rail.tracks)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        if (map.sq[location.X + tc.conn[k].u, location.Y + tc.conn[k].v].edgelist.Contains(newedge))
                        {
                            u = tc.conn[k].u;
                            v = tc.conn[k].v;
                            break;
                        }
                    }
                }
                fraction = 0.5;
                u0 = u;
                v0 = v;
                Msg.memo("u,v = " + u  + ","+ v);

            }
            else
            {
                //Something is wrong
                throw new Exception("No route to station");
            }

            this.station = null;

        }

        public List<Point> move(int milliseconds,mapclass map)
        {
            List<Point> lp = new List<Point>();
            if (stationcountdown > 0)
            {
                stationcountdown--;
                if (stationcountdown <= 0)
                    prepare_departure();
                return lp;
            }
            double sqpertick = 0.1*(speed / refspeed) / Math.Sqrt(u*u+v*v);
            //double tickspersec = 1000 / milliseconds;
            fraction += sqpertick;
            if (fraction >= 1) //go to next square
            {
                Point newloc = new Point(location.X + u, location.Y + v);
                fraction = fraction - 1;
                u0 = u;
                v0 = v;
                if (map.square(newloc).nodelist.Count > 0)
                {
                    //find right outgoing track from switch
                    int thisnode = map.railgraph.edgedict[edgeroute[currentedge]].tonode;
                    currentedge++;
                    if (currentedge >= edgeroute.Count() || edgeroute[currentedge] == 0) //should be at the station
                    {
                        if (map.square(newloc).station.name == itinerary[nextstop])
                        {
                            //We've arrived!
                            Msg.memo("Train " + this.id + " arriving at " + map.square(newloc).station.name);
                            speed = 0;
                            stationcountdown = 5;
                            this.station = map.square(newloc).station;
                        }
                        else
                        {
                            // Something is wrong!
                            throw new Exception("Arrived at wrong station");
                        }
                    }
                    else
                    {

                        int newedge = edgeroute[currentedge];
                        foreach (squaretrackclass tc in map.square(newloc).rail.tracks)
                        {
                            for (int k=0;k<2;k++)
                            {
                                if (map.sq[newloc.X+tc.conn[k].u,newloc.Y+tc.conn[k].v].edgelist.Contains(newedge))
                                {
                                    u = tc.conn[k].u;
                                    v = tc.conn[k].v;
                                    break;
                                }
                            }
                        }
                    }
                }
                else //simple track
                {
                    Point newuv = map.square(newloc).rail.tracks.First().other_end(u0, v0);
                    u = newuv.X;
                    v = newuv.Y;
                }
                //if (!oldloc.Equals(nullpoint))
                //    map.square(oldloc).restore_cleanpic();
                oldloc = location;
                lp.Add(oldloc);
                location = newloc;
            }
            double ux = u * fraction + u0 * (1 - fraction);
            double vx = v * fraction + v0 * (1 - fraction);
            angle = 180*(float)(Math.Atan2(vx, ux)/Math.PI);
            Point uv0 = uv_to_coord(u0, v0, true);
            Point uv = uv_to_coord(u, v, false);
            int x = (int)Math.Round(uv0.X + fraction * (uv.X - uv0.X));
            int y = (int)Math.Round(uv0.Y + fraction * (uv.Y - uv0.Y));

            trainposclass tp = new trainposclass(this.location, x, y, angle);
            if (poshistory[ipos].dist2(tp) != 0)
            {
                ipos++;
                if (ipos == maxpos)
                    ipos = 0;
                poshistory[ipos] = tp;
            }

            Msg.memo(location.X + "," + location.Y + ": x,y,angle " + x + "," + y + ", " + Math.Round(angle));
            map.square(location).showtrain(this, x, y, angle);
            if (!oldloc.Equals(nullpoint))
                map.square(oldloc).showtrain(this, x+u0*squareclass.squaresize, y+v0*squareclass.squaresize, angle);
            lp.Add(location);

            double maxspeed = Math.Min(engine.enginetype.topspeed, map.square(location).rail.speedlimit);
            if (speed < maxspeed)
            {
                double acceleration = engine.enginetype.tractive_force / trainweight();
                speed += acceleration;
            }
            if (speed > maxspeed)
                speed = maxspeed;
            return lp;
        }

        private double trainweight()
        {
            double w = engine.enginetype.weight;
            foreach (carclass cc in cars)
            {
                w += cc.weight;
            }
            return w;
        }

        private Point uv_to_coord(int u,int v, bool incoming)
        {
            //return coordinates in square of track exiting square in u/v direction.
            int uu = incoming ? -u : u;
            int vv = incoming ? -v : v;

            int x;
            int y;
            int farside = squareclass.squaresize-1;
            if (uu*vv == 0)
            {
                if ( uu == 0)
                {
                    if ( vv < 0)
                    {
                        x = farside / 2;
                        y = 0;
                    }
                    else
                    {
                        x = farside / 2;
                        y = farside;
                    }
                }
                else
                {
                    if (uu < 0)
                    {
                        x = 0;
                        y = farside / 2;
                    }
                    else
                    {
                        x = farside;
                        y = farside / 2;
                    }
                }
            }
            else //corner
            {
                if (uu < 0)
                {
                    if (vv < 0)
                    {
                        x = 0;
                        y = 0;
                    }
                    else
                    {
                        x = 0;
                        y = farside;
                    }
                }
                else
                {
                    if ( vv < 0)
                    {
                        x = farside;
                        y = 0;
                    }
                    else
                    {
                        x = farside;
                        y = farside;
                    }
                }
            }

            return new Point(x, y);
        }
    }
}
