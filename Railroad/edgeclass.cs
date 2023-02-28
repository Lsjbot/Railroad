using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class edgeclass
    {
        public int ID;
        public int fromnode;
        public int tonode;
        public int sidenode;
        public string side;
        public Point[] path; //not used!
        public double traveltime = 1;
    }
}
