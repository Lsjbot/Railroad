using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class squaretrackclass //track through square, from one connection to the next
    {
        public string name = "";
        public trackconnectionclass[] conn = new trackconnectionclass[2]; //two endpoints of track
        public List<squaretrackclass> safecrossing = new List<squaretrackclass>(); //can have trains on both tracks (default is not)
        public int oneway = 0; //0 = twoway, +1 = from conn[0] to conn[1], -1 = from 1 to 0
        public double slope = 0; // + if conn[1] higher than conn[0], - otherwise

        public squaretrackclass Clone()
        {
            squaretrackclass st = new squaretrackclass();
            st.name = this.name;
            st.conn[0] = this.conn[0];
            st.conn[1] = this.conn[1];
            st.oneway = this.oneway;
            st.slope = this.slope;
            return st;
        }

        public Point other_end(int uin, int vin)
        {
            int k = 0;
            if ((conn[k].u == -uin) && (conn[k].v == -vin))
            {
                k = 1;
            }
            return new Point(conn[k].u, conn[k].v);
        }
    }
}
