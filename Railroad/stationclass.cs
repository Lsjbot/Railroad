using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class stationclass
    {
        public static Dictionary<string, stationclass> stationdict = new Dictionary<string, stationclass>();
        public int stationmapsize = 25;
        public string name;
        public mapclass stationmap;
        public Point location;
        public List<int> nodes = new List<int>();

        public stationclass(string namepar,Point locpar,mapclass mainmap)
        {
            if (!String.IsNullOrEmpty(namepar))
                name = namepar;
            else
                name = "Station " + (stationdict.Count + 1).ToString();
            location = locpar;
            stationmap = new mapclass(stationmapsize,mapclass.stationmaptype);
            stationmap.flatmap(terrainclass.terraindict["Sand"]);
            stationmap.maptype = mapclass.stationmaptype;

            switch (mainmap.sq[location.X,location.Y].rail.pattern)
            {
                case "W,E":
                case "E,W":
                    int y0 = stationmapsize / 2;
                    for (int i = 1; i < stationmapsize-1; i++)
                        stationmap.addrail(new Point(i,y0),mainmap.sq[location.X, location.Y].rail);
                    stationmap.addrail(new Point(1, y0+1), railclass.find_by_pattern("NW,E"));
                    stationmap.addrail(new Point(stationmapsize-2, y0 + 1), railclass.find_by_pattern("W,NE"));
                    for (int i = 2; i < stationmapsize - 2; i++)
                        stationmap.addrail(new Point(i, y0+1), mainmap.sq[location.X, location.Y].rail);
                    stationmap.addrail(new Point(0, y0), railclass.find_by_pattern("W,E.W,SE"));
                    stationmap.addrail(new Point(stationmapsize - 1, y0), railclass.find_by_pattern("E,W.E,SW"));

                    break;
                case "N,S":
                case "S,N":
                    int x0 = stationmapsize / 2;
                    for (int i = 1; i < stationmapsize - 1; i++)
                        stationmap.addrail(new Point(x0,i), mainmap.sq[location.X, location.Y].rail);
                    stationmap.addrail(new Point(x0+1,1), railclass.find_by_pattern("S,NW"));
                    stationmap.addrail(new Point(x0+1,stationmapsize - 2), railclass.find_by_pattern("SW,N"));
                    for (int i = 2; i < stationmapsize - 2; i++)
                        stationmap.addrail(new Point(x0 + 1,i), mainmap.sq[location.X, location.Y].rail);
                    stationmap.addrail(new Point(x0, 0), railclass.find_by_pattern("N,S.N,SE"));
                    stationmap.addrail(new Point(x0, stationmapsize - 1), railclass.find_by_pattern("S,N.S,NE"));

                    break;
                case "SE,NW":
                case "NW,SE":
                    for (int i = 1; i < stationmapsize - 1; i++)
                        stationmap.addrail(new Point(i, i), mainmap.square(location).rail);
                    stationmap.addrail(new Point(1, 0), railclass.find_by_pattern("W,SE"));
                    stationmap.addrail(new Point(stationmapsize-1, stationmapsize-2), railclass.find_by_pattern("S,NW"));
                    for (int i = 1; i < stationmapsize - 2; i++)
                        stationmap.addrail(new Point(i+1, i), mainmap.square(location).rail);
                    stationmap.addrail(new Point(0, 0), railclass.find_by_pattern("NW,SE.NW,E"));
                    stationmap.addrail(new Point(stationmapsize-1,stationmapsize-1), railclass.find_by_pattern("SE,NW.SE,N"));

                    break;
                case "SW,NE":
                case "NE,SW":
                    for (int i = 1; i < stationmapsize - 1; i++)
                        stationmap.addrail(new Point(i, stationmapsize-1-i), mainmap.square(location).rail);
                    stationmap.addrail(new Point(1, stationmapsize-1), railclass.find_by_pattern("W,NE"));
                    stationmap.addrail(new Point(stationmapsize - 1, 1), railclass.find_by_pattern("SW,N"));
                    for (int i = 1; i < stationmapsize - 2; i++)
                        stationmap.addrail(new Point(i+1, stationmapsize-1-i), mainmap.square(location).rail);
                    stationmap.addrail(new Point(0, stationmapsize-1), railclass.find_by_pattern("SW,NE.SW,E"));
                    stationmap.addrail(new Point(stationmapsize - 1, 0), railclass.find_by_pattern("NE,SW.NE,S"));

                    break;

            }

            foreach (nodeclass nt in this.stationmap.railgraph.nodedict.Values)
            {
                nt.refreshconnections(stationmap);
            }

            stationdict.Add(this.name, this);
        }
    }
}
