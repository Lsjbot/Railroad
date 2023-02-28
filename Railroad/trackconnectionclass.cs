using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class trackconnectionclass //how a track mates with track in next square
    {        
        public string side = "";
        public char leftright = 'c'; //c = single track, l/r = double track (left/right as seen from inside square)
        public int u = 0; // x-diff to mating square
        public int v = 0; // y-diff to mating square
        public Point pixel;
        public string id;
        //public int pixelx = 0; //x-coord of pixel at connection point
        //public int pixely = 0; //y-coord of pixel at connection point

        public trackconnectionclass(string sidepar, char lr, int uu, int vv, int px, int py)
        {
            side = sidepar;
            leftright = lr;
            u = uu;
            v = vv;
            pixel = new Point(px, py);
            id = side + "-" + leftright;
            //pixelx = px;
            //pixely = py;
        }

        public static string match(trackconnectionclass tc)
        {
            string s = railclass.rot180dict[tc.side];
            if (tc.leftright == 'c')
                s += "-c";
            else if (tc.leftright == 'l')
                s += "-r";
            else
                s += "-l";
            return s;
        }

    }
}
