using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class enginetypeclass: templateclass
    {
        public string name = "";
        public int maxcars = 8;
        public int enginelength = 16; //in pixels
        public double tractive_force = 100000;
        public double topspeed = 100;
        public double weight = 10000;

        public enginetypeclass(string namepar, int maxcarpar, int lengthpar)
        {
            name = namepar;
            maxcars = maxcarpar;
            enginelength = lengthpar;

            template = new Bitmap(squareclass.squaresize,squareclass.squaresize);
            Graphics gg = Graphics.FromImage(template);
            SolidBrush nullbrush = new SolidBrush(templateclass.templatenull);
            gg.FillRectangle(nullbrush, new Rectangle(0, 0, squareclass.squaresize, squareclass.squaresize));
            SolidBrush redbrush = new SolidBrush(Color.Red);
            gg.FillRectangle(redbrush, new Rectangle(0, 14, enginelength, 4));
            SolidBrush yellowbrush = new SolidBrush(Color.Yellow);
            gg.FillRectangle(yellowbrush, new Rectangle(15, 14, 1, 4));
            gg.Flush();

        }
    }
}
