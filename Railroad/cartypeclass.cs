using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class cartypeclass: templateclass
    {
        public static int carlength = 16;

        public string name = "";
        public List<cargoclass> cargolist = new List<cargoclass>();

        public cartypeclass(string namepar)
        {
            name = namepar;

            template = new Bitmap(squareclass.squaresize, squareclass.squaresize);
            Graphics gg = Graphics.FromImage(template);
            SolidBrush nullbrush = new SolidBrush(templateclass.templatenull);
            gg.FillRectangle(nullbrush, new Rectangle(0, 0, squareclass.squaresize, squareclass.squaresize));
            SolidBrush bluebrush = new SolidBrush(Color.Blue);
            gg.FillRectangle(bluebrush, new Rectangle(15, 14, carlength, 4));
            SolidBrush yellowbrush = new SolidBrush(Color.Yellow);
            gg.FillRectangle(yellowbrush, new Rectangle(31, 14, 1, 4));
            gg.Flush();

        }
    }
}
