using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class terrainclass
    {
        public static List<terrainclass> terrainlist = new List<terrainclass>();
        public static Dictionary<string, terrainclass> terraindict = new Dictionary<string, terrainclass>();


        public Bitmap pic;
        public string name = "terrain";
        public Color averagecolor = Color.Green; //take average of pic color, to use when filling gaps etc.
        public int buildcost = 0;
        Random rnd = new Random();
        static int npic = 10;
        public Bitmap[] picset;


        public terrainclass(Color col, string namepar)
        {
            name = namepar;
            pic = new Bitmap(squareclass.squaresize, squareclass.squaresize);
            averagecolor = col;
            Graphics g = Graphics.FromImage(pic);
            SolidBrush mybrush = new SolidBrush(col);
            g.FillRectangle(mybrush, new Rectangle(0,0, squareclass.squaresize, squareclass.squaresize));
            g.Flush();

            picset = new Bitmap[npic];
            for (int i = 0; i < npic; i++)
                picset[i] = makeuniquepic();

        }
        public terrainclass(Bitmap b, string namepar)
        {
            name = namepar;
            pic = b;
            averagecolor = pic.GetPixel(1,1);

        }

        public terrainclass(int altitude, string namepar)
        {
            if (altitude == 0)
                averagecolor = Color.Blue;
            else
            {
                averagecolor = altitudecolor(altitude);
            }
            name = namepar;
            pic = new Bitmap(squareclass.squaresize, squareclass.squaresize);
            Graphics g = Graphics.FromImage(pic);
            SolidBrush mybrush = new SolidBrush(averagecolor);
            g.FillRectangle(mybrush, new Rectangle(0, 0, squareclass.squaresize, squareclass.squaresize));
            g.Flush();
        }

        public static void init_terrains()
        {
            terrainlist.Add(new terrainclass(Color.Blue, "Ocean"));
            terrainlist.Add(new terrainclass(Color.Green, "Grass"));
            terrainlist.Add(new terrainclass(Color.Brown, "Rock"));
            terrainlist.Add(new terrainclass(Color.White, "Snow"));
            terrainlist.Add(new terrainclass(Color.Tan, "Sand"));

            foreach (terrainclass tc in terrainlist)
                terraindict.Add(tc.name, tc);
        }

        public static Color altitudecolor(float altitude)
        {
            int g = 0;
            int r = 0;
            int b = 0;
            int lim1 = 200;
            int lim2 = 500;
            int lim3 = 1000;
            int lim4 = 3000;
            if (altitude < lim1)
            {
                g = (int)(100 + 155 * altitude / lim1);
                r = 0;
                b = (int)(50 - 50 * altitude / lim1);
            }
            else if (altitude < lim2)
            {
                g = 255;
                r = (int)(155 * (altitude-lim1) / (lim2-lim1));
                b = 0;
            }
            else if (altitude < lim3)
            {
                g = 255 - (int)(100*(altitude-lim2)/(lim3-lim2));
                r = 155;
                b = (int)(155*(altitude-lim2)/(lim3-lim2));
            }
            else if (altitude < lim4)
            {
                g = 155 + (int)(100 * (altitude-lim3) / (lim4 - lim3));
                r = 155 + (int)(100 * (altitude - lim3) / (lim4 - lim3));
                b = 155 + (int)(100 * (altitude - lim3) / (lim4 - lim3));
            }
            else
            {
                r = 255;
                g = 255;
                b = 255;
            }
            return Color.FromArgb(r, g, b);
        }

        private Bitmap makeuniquepic()
        {
            Bitmap bm = (Bitmap)pic.Clone();
            //for (int i=0;i<squareclass.squaresize;i++)
            //    for (int j = 0; j < squareclass.squaresize; j++)
            //    {
            //        int c = bm.GetPixel(i, j).ToArgb();
            //        int bit = rnd.Next(24);
            //        c = c ^= (1 << bit); //flip a random bit
            //        bm.SetPixel(i,j,Color.FromArgb(c));
            //    }

            return bm;
        }

        public Bitmap getrandompic()
        {
            if (picset == null)
                return pic;
            else
                return picset[rnd.Next(npic)];
        }
    }
}
