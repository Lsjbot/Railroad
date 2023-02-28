using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class trainposclass //location of a point on the train
    {
        Point mapsquare;
        int x; //pixel within mapsquare
        int y;
        double angle; //orientation
        public trainposclass(Point ms, int xx, int yy, double ang)
        {
            mapsquare = ms;
            x = xx;
            y = yy;
            angle = ang;
        }

        public int dist2(trainposclass other)
        {
            int dx = (this.mapsquare.X - other.mapsquare.X) * squareclass.squaresize + (this.x - other.x);
            int dy = (this.mapsquare.Y - other.mapsquare.Y) * squareclass.squaresize + (this.y - other.y);
            return dx * dx + dy * dy;
        }
    }

}
