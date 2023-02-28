using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class templateclass
    {
        public Bitmap template = null;
        public static Color templatenull = Color.FromArgb(255,255,255,255);
        public string pattern = "s,nw.e,nw.se,nw"; //which corners/sides are connected within the square
        //bool[,] connections = new bool[3, 3]; //which corners/side take rail connections from neighbors

        public Bitmap overlay_template(Bitmap oldpic)
        {
            //Console.WriteLine(templatenull.ToString() + " templatenull ");
            Bitmap bm = (Bitmap)oldpic.Clone();

            for (int i=0;i<bm.Width;i++)
                for (int j=0;j<bm.Height;j++)
                {
                    Color c = template.GetPixel(i, j);
                    //Console.WriteLine(c.ToString() + i + " " + j);
                    if (!template.GetPixel(i, j).Equals(templatenull))
                        bm.SetPixel(i, j, template.GetPixel(i, j));
                }
            //Console.WriteLine("Done");
            return bm;
        }

        public static Bitmap rotatebitmap(Bitmap oldpic, int xshift, int yshift, float angle)
        {
            Bitmap bm = (Bitmap)oldpic.Clone();
            using (Graphics gg = Graphics.FromImage(bm))
            {
                int bsx = 16;
                int bsy = 16;
                gg.FillRectangle(new SolidBrush(templatenull), new Rectangle(0, 0, 32, 32));
                gg.TranslateTransform(bsx, bsy);
                gg.TranslateTransform(xshift, yshift);
                gg.RotateTransform(angle);
                gg.TranslateTransform(-bsx, -bsy);
                gg.DrawImage(oldpic,0,0);
                gg.Flush();
            }
            return bm;
            
        }

        public Bitmap overlay_template(Bitmap oldpic, int xshift, int yshift, float angle)
        {
            //Console.WriteLine(templatenull.ToString() + " templatenull ");
            Bitmap bm = (Bitmap)oldpic.Clone();
            Bitmap temp = rotatebitmap((Bitmap)template,xshift-squareclass.squaresize/2,yshift-squareclass.squaresize/2,angle);

            for (int i = 0; i < bm.Width; i++)
                for (int j = 0; j < bm.Height; j++)
                {
                    //Color c = temp.GetPixel(i, j);
                    //Console.WriteLine(c.ToString() + i + " " + j);
                    if (!temp.GetPixel(i, j).Equals(templatenull))
                        bm.SetPixel(i, j, temp.GetPixel(i, j));
                }
            //Console.WriteLine("Done");
            return bm;

        }
    }
}
