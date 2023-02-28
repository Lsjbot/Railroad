using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Railroad
{
    public partial class FormMainmap : Form
    {
        private Bitmap bm;
        private Graphics g;
        private int x0 = 0; //screen origin in bitmap coordinates
        private int y0 = 0;
        private int bmx0 = 0; //bitmap origin in map coordinates
        private int bmy0 = 0;
        public mapclass map;
        private static int maxbitmapsize = 8192;
        private int bitmapsize;

        

        private Point markedsquare = new Point(0, 0); //map coordinates of marked square


        public FormMainmap(mapclass mappar, string maplabel)
        {

            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Left = 0;
            this.Top = 0;
            this.Location = Screen.AllScreens[1].WorkingArea.Location;

            map = mappar;

            bitmapsize = Math.Max(map.mapsizex, map.mapsizey) * squareclass.squaresize;
            if (bitmapsize > maxbitmapsize)
                bitmapsize = maxbitmapsize;
            //bm = new Bitmap(@"I:\Bilder\2008-07-12 Filippinerna\P1010551.JPG");
            bm = new Bitmap(bitmapsize, bitmapsize);
            g = Graphics.FromImage(bm);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //SolidBrush mybrush = new SolidBrush(Color.Black);
            //g.FillRectangle(mybrush, new Rectangle(0,0,bitmapsize,bitmapsize));
            maptobitmap(0, 0);

            g.Flush();

            //this.BackgroundImage = bm;


        }

        private bool inbitmap(int x, int y, int size)
        {
            if (x < 0)
                return false;
            if (y < 0)
                return false;
            if (x + size > bitmapsize)
                return false;
            if (y + size > bitmapsize)
                return false;
            return true;
        }

        private void maptobitmap(int i0, int j0)
        {
            bmx0 = i0;
            bmy0 = j0;
            for (int i = 0; i < map.mapsizex; i++)
                for (int j = 0; j < map.mapsizey; j++)
                {
                    int xi = (i - bmx0) * squareclass.squaresize;
                    int yj = (j - bmy0) * squareclass.squaresize;
                    if (inbitmap(xi, yj, squareclass.squaresize))
                    {
                        g.DrawImageUnscaled(map.sq[i, j].pic, xi, yj);
                        maptexts(i, j, xi, yj);

                    }
                }

        }

        private void maptexts(int i, int j, int xi, int yj)
        {
            g.DrawString(map.sq[i, j].altitude.ToString(), new Font("Tahoma", 6), Brushes.Yellow, xi, yj);
            if (!String.IsNullOrEmpty(map.sq[i, j].placename))
                g.DrawString(map.sq[i, j].placename, new Font("Tahoma", 8), Brushes.Red, xi - 15, yj + 15);

        }

        private static Pen markframepen = new Pen(Brushes.Red, 2);

        private void marksquare(Point mappoint)
        {
            int sq = squareclass.squaresize;
            int xi = (mappoint.X - bmx0) * sq;
            int yj = (mappoint.Y - bmx0) * sq;
            if (inbitmap(xi, yj, squareclass.squaresize))
            {
                Point[] pp = new Point[] { new Point(xi + 1, yj + 1), new Point(xi + sq - 1, yj + 1), new Point(xi + sq - 1, yj + sq - 1), new Point(xi + 1, yj + sq - 1), new Point(xi + 1, yj + 1) };
                g.DrawLines(markframepen, pp);
            }

        }

        private void unmarksquare(Point mappoint)
        {
            refreshsquare(mappoint);
        }

        public void refreshsquares(List<Point> lp)
        {
            foreach (Point p in lp)
                refreshsquare(p);
            g.Flush();
            pb.Invalidate();

        }

        private void refreshsquare(Point mappoint)
        {
            int xi = (mappoint.X - bmx0) * squareclass.squaresize;
            int yj = (mappoint.Y - bmy0) * squareclass.squaresize;
            if (inbitmap(xi, yj, squareclass.squaresize))
            {
                for (int i = 0; i < 32; i++)
                    for (int j = 0; j < 32; j++)
                        bm.SetPixel(xi + i, yj + j, (map.sq[mappoint.X, mappoint.Y].pic.GetPixel(i, j)));
                //g.DrawImageUnscaled(map.sq[mappoint.X, mappoint.Y].pic, xi, yj);
                maptexts(mappoint.X, mappoint.Y, xi, yj);
            }
            //g.Flush();
        }

        private void pb_Paint(object sender, PaintEventArgs e)
        {
            if (bm.Width <= pb.Width)
                x0 = 0;
            if (bm.Height <= pb.Height)
                y0 = 0;
            if (x0 < 0)
                x0 = 0;
            if (y0 < 0)
                y0 = 0;
            //if (x0 + pb.Width > bm.Width)
            //    x0 = bm.Width - pb.Width;
            //if (y0 + pb.Height > bm.Height)
            //    y0 = bm.Height - pb.Height;
            using (Bitmap bmc = bm.Clone(new Rectangle(x0, y0, Math.Min(bm.Width,pb.Width), Math.Min(bm.Height,pb.Height)), bm.PixelFormat))
                e.Graphics.DrawImageUnscaled(bmc, 0, 0);
        }

        private Point mapsquare_from_screenpoint(Point screenpoint)
        {
            int bmx = screenpoint.X + x0;
            int bmy = screenpoint.Y + y0;
            int mx = bmx0 + bmx / squareclass.squaresize;
            int my = bmx0 + bmy / squareclass.squaresize;
            Point p = new Point(mx,my);
            return p;
        }

        private Point screenpoint_from_mapsquare(Point mapsquare)
        {
            int bmx = (mapsquare.X - bmx0) * squareclass.squaresize;
            int bmy = (mapsquare.Y - bmy0) * squareclass.squaresize;
            Point p = new Point(bmx - x0, bmy - y0);
            return p;
        }

        private void buildrail()
        {
            List<string> requiredtracks = new List<string>();
            List<string> vetosides = new List<string>();
            int railsides = 0;
            for (int u=-1;u<=1;u++)
                for (int v=-1;v<=1;v++)
                {
                    if (u == 0 && v == 0)
                        continue;
                    int xx = markedsquare.X + u;
                    int yy = markedsquare.Y + v;
                    if (map.inmap(xx,yy))
                    {
                        if (map.sq[xx,yy].rail != null)
                        {
                            railsides++;
                            foreach (trackconnectionclass tc in map.sq[xx,yy].rail.connections(-u,-v))
                            {
                                requiredtracks.Add(trackconnectionclass.match(tc));
                            }
                        }
                    }
                    else //don't build rail to the edge
                    {
                        if (u == 0)
                        {
                            if ( v > 0)
                            {
                                vetosides.Add("S");
                                vetosides.Add("SE");
                                vetosides.Add("SW");
                            }
                            else
                            {
                                vetosides.Add("N");
                                vetosides.Add("NE");
                                vetosides.Add("NW");
                            }
                        }
                        else if (v == 0)
                        {
                            if (u > 0)
                            {
                                vetosides.Add("E");
                                vetosides.Add("SE");
                                vetosides.Add("NE");
                            }
                            else
                            {
                                vetosides.Add("W");
                                vetosides.Add("SW");
                                vetosides.Add("NW");
                            }

                        }
                    }
                }
            FormRailselect fr = new FormRailselect(requiredtracks, vetosides, screenpoint_from_mapsquare(markedsquare)+new Size(this.Left,this.Top)+new Size(pb.Left,pb.Top));
            fr.ShowDialog();
            if (fr.selrail != null)
            {
                map.addrail(markedsquare, fr.selrail);
                if ((railsides > 1)&&(fr.selrail.nodetypes.Count == 0))
                {
                    //connecting two pieces of track; check for new edge
                    string[] pp = map.sq[markedsquare.X, markedsquare.Y].rail.pattern.Split(',');
                    int node1f = map.follow_track_to_node(markedsquare, pp[0], true);
                    int node1t = map.follow_track_to_node(markedsquare, pp[1], false);
                    int node2f = map.follow_track_to_node(markedsquare, pp[1], true);
                    int node2t = map.follow_track_to_node(markedsquare, pp[0], false);
                    if (node1f > 0)
                        map.railgraph.nodedict[node1f].refreshconnections(map);
                    if (node1t > 0)
                        map.railgraph.nodedict[node1t].refreshconnections(map);
                    if (node2f > 0)
                        map.railgraph.nodedict[node2f].refreshconnections(map);
                    if (node2t > 0)
                        map.railgraph.nodedict[node2t].refreshconnections(map);
                }
                refreshsquare(markedsquare);
                marksquare(markedsquare);
            }

        }

        public void buildstation()
        {
            if (map.sq[markedsquare.X, markedsquare.Y].rail == null)
            {
                MessageBox.Show("Cannot build station without track");
                return;
            }
            if (!map.sq[markedsquare.X, markedsquare.Y].rail.stationplace)
            {
                MessageBox.Show("Cannot build station on this type of track");
                return;
            }
            string name = map.sq[markedsquare.X, markedsquare.Y].placename;
            InputBox ib = new InputBox("Build new station", name, false);
            ib.ShowDialog();
            if (ib.DialogResult != DialogResult.OK)
                return;

            map.sq[markedsquare.X, markedsquare.Y].station = new stationclass(ib.gettext(), markedsquare,map);
            map.stationlist.Add(map.sq[markedsquare.X, markedsquare.Y].station);
            map.sq[markedsquare.X, markedsquare.Y].placename = ib.gettext();

            int[] edgedummy = new int[map.sq[markedsquare.X, markedsquare.Y].edgelist.Count];
            map.sq[markedsquare.X, markedsquare.Y].edgelist.CopyTo(edgedummy);
            foreach (int nedge in edgedummy)
            {
                map.railgraph.removeedge(nedge, map);
            }

            string np = map.sq[markedsquare.X, markedsquare.Y].rail.pattern_to_nodepattern();
            map.sq[markedsquare.X, markedsquare.Y].rail.nodepattern = np;
            map.sq[markedsquare.X, markedsquare.Y].rail.nodetypes = map.sq[markedsquare.X, markedsquare.Y].rail.parse_nodepattern(np);
            map.sq[markedsquare.X, markedsquare.Y].addnodes(map.sq[markedsquare.X, markedsquare.Y].rail,map);
            foreach (int inc in map.square(markedsquare).nodelist)
            {
                map.railgraph.nodedict[inc].refreshconnections(map);
            }
            map.square(markedsquare).edgelist.Add(map.railgraph.addedge(map.square(markedsquare).nodelist[0], map.square(markedsquare).nodelist[1]));
            map.square(markedsquare).edgelist.Add(map.railgraph.addedge(map.square(markedsquare).nodelist[1], map.square(markedsquare).nodelist[0]));
        }

        private void FormMainmap_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("Key = " + e.KeyChar);
            int step = squareclass.squaresize/2;
            switch (e.KeyChar)
            {
                case 'b':
                case 'B':
                    buildrail();
                    break;
                case 's':
                case 'S':
                    if (map.maptype == mapclass.mainmaptype)
                        buildstation();
                    break;
                case 'g':
                case 'G':
                    map.railgraph.drawgraph();
                    break;
                case 'p':
                case 'P':
                    Form1.paused = !Form1.paused;
                    break;
                //case (char)Keys.Right:
                //    x0 += step;
                //    break;

                //case (char)Keys.Left:
                //    x0 -= step;
                //    break;
                //case (char)Keys.Down:
                //    y0 += step;
                //    break;
                //case (char)Keys.Up:
                //    y0 -= step;
                //    break;
            }
            pb.Invalidate();
            this.Invalidate();

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //capture up arrow key
            Console.WriteLine("Key PCK = " + keyData);
            int step = 10;
            switch (keyData)
            {
                case Keys.Right:
                    x0 += step;
                    break;

                case Keys.Left:
                    x0 -= step;
                    break;
                case Keys.Down:
                    y0 += step;
                    break;
                case Keys.Up:
                    y0 -= step;
                    break;
                default: return base.ProcessCmdKey(ref msg, keyData);
                    
            }
            this.Invalidate();
            pb.Invalidate();

            return true;
        }

        private void FormMainmap_SizeChanged(object sender, EventArgs e)
        {
            pb.Width = this.Width - 40;
            pb.Height = this.Height - 40;
            pb.Invalidate();
        }

        private void move_markedsquare(Point newmarked)
        {
            unmarksquare(markedsquare);
            markedsquare = newmarked;
            marksquare(markedsquare);
            this.Invalidate();
            pb.Invalidate();

        }

        private void pb_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Location.X + " "+e.Location.Y);
            move_markedsquare(mapsquare_from_screenpoint(e.Location));
        }

        private void pb_DoubleClick(object sender, EventArgs e)
        {

        }

        private void pb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            move_markedsquare(mapsquare_from_screenpoint(e.Location));
            if (map.sq[markedsquare.X,markedsquare.Y].station != null)
            {
                FormMainmap stationview = new FormMainmap(map.sq[markedsquare.X, markedsquare.Y].station.stationmap, "Station" + map.sq[markedsquare.X, markedsquare.Y].station.name);
                stationview.Show();
            }
        }
    }
}
