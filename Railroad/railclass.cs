using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Railroad
{
    public class railclass: templateclass
    {
        public squaretrackclass[] tracks;
        bool doubletrack = false;
        public bool stationplace = false;
        public railclass doubleversion = null;
        public List<nodetypeclass> nodetypes = new List<nodetypeclass>();
        public string nodepattern = "";
        public double speedlimit = 100;
        

        public static List<railclass> railtypes = new List<railclass>();
        public static List<railclass> railbasetypes = new List<railclass>();
        public static Dictionary<string, railclass> railbasedict = new Dictionary<string, railclass>();
        public static Dictionary<string, railclass> raildoubledict = new Dictionary<string, railclass>();
        public static List<trackconnectionclass> trackconnections = new List<trackconnectionclass>();
        public static string[] tcnext = new string[24]; //connections ordered around a square
        public static Dictionary<string, string> rot90dict = new Dictionary<string, string>();
        public static Dictionary<string, string> rot180dict = new Dictionary<string, string>();
        public static int doubleoffset = 3; // 

        public static Dictionary<string, Bitmap> bitmapdict = new Dictionary<string, Bitmap>();

        public railclass Clone()
        {
            railclass rc = new railclass(this.pattern, this.doubletrack, this.stationplace,this.nodepattern);
            rc.tracks = new squaretrackclass[this.tracks.Length];
            for (int i = 0; i < this.tracks.Length; i++)
                rc.tracks[i] = this.tracks[i].Clone();
            return rc;
        }

        public static railclass find_by_pattern(string pattern)
        {
            var q = from c in railtypes where c.pattern == pattern select c;
            if (q.Count() == 1)
                return q.First();
            else
            {
                q = from c in railtypes where c.pattern == reversedefstring(pattern) select c;
                if (q.Count() == 1)
                    return q.First();
                else
                {
                    q = from c in railtypes where c.pattern == permutedefstring(pattern) select c;
                    if (q.Count() == 1)
                        return q.First();
                    else
                    {
                        q = from c in railtypes where c.pattern == permutedefstring(reversedefstring(pattern)) select c;
                        if (q.Count() == 1)
                            return q.First();
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public static void initrails()
        {
            int ss = squareclass.squaresize+1;
            int ss2 = ss / 2 + 2;
            int dd = railclass.doubleoffset;
            int dd2 = (int)Math.Round(dd*Math.Sqrt(2));
            trackconnections.Add(new trackconnectionclass("S", 'c', 0, 1,ss2,ss));
            trackconnections.Add(new trackconnectionclass("N", 'c', 0, -1,ss2,0));
            trackconnections.Add(new trackconnectionclass("E", 'c', 1, 0,ss,ss2));
            trackconnections.Add(new trackconnectionclass("W", 'c', -1, 0,0,ss2));
            trackconnections.Add(new trackconnectionclass("SW", 'c', -1, 1,0,ss));
            trackconnections.Add(new trackconnectionclass("SE", 'c', 1, 1,ss,ss));
            trackconnections.Add(new trackconnectionclass("NW", 'c', -1, -1,0,0));
            trackconnections.Add(new trackconnectionclass("NE", 'c', 1, -1,ss,0));
            trackconnections.Add(new trackconnectionclass("S", 'l', 0, 1,ss2+dd,ss));
            trackconnections.Add(new trackconnectionclass("N", 'l', 0, -1,ss2-dd,0));
            trackconnections.Add(new trackconnectionclass("E", 'l', 1, 0,ss,ss2-dd));
            trackconnections.Add(new trackconnectionclass("W", 'l', -1, 0,0,ss2+dd));
            trackconnections.Add(new trackconnectionclass("SW", 'l', -1, 1,dd2,ss));
            trackconnections.Add(new trackconnectionclass("SE", 'l', 1, 1,ss,ss-dd2));
            trackconnections.Add(new trackconnectionclass("NW", 'l', -1, -1,0,dd2));
            trackconnections.Add(new trackconnectionclass("NE", 'l', 1, -1,ss-dd2,0));
            trackconnections.Add(new trackconnectionclass("S", 'r', 0, 1,ss2-dd,ss));
            trackconnections.Add(new trackconnectionclass("N", 'r', 0, -1,ss2+dd,0));
            trackconnections.Add(new trackconnectionclass("E", 'r', 1, 0,ss,ss2+dd));
            trackconnections.Add(new trackconnectionclass("W", 'r', -1, 0,0,ss2-dd));
            trackconnections.Add(new trackconnectionclass("SW", 'r', -1, 1,0,ss-dd2));
            trackconnections.Add(new trackconnectionclass("SE", 'r', 1, 1,ss-dd2,ss));
            trackconnections.Add(new trackconnectionclass("NW", 'r', -1, -1,dd2,0));
            trackconnections.Add(new trackconnectionclass("NE", 'r', 1, -1,ss,dd2));

            tcnext[0] = "Sl";
            tcnext[1] = "Sc";
            tcnext[2] = "Sr";
            tcnext[3] = "SWl";
            tcnext[4] = "SWc";
            tcnext[5] = "SWr";
            tcnext[6] = "Wl";
            tcnext[7] = "Wc";
            tcnext[8] = "Wr";
            tcnext[9] = "NWl";
            tcnext[10] = "NWc";
            tcnext[11] = "NWr";
            tcnext[12] = "Nl";
            tcnext[13] = "Nc";
            tcnext[14] = "Nr";
            tcnext[15] = "NEl";
            tcnext[16] = "NEc";
            tcnext[17] = "NEr";
            tcnext[18] = "El";
            tcnext[19] = "Ec";
            tcnext[20] = "Er";
            tcnext[21] = "SEl";
            tcnext[22] = "SEc";
            tcnext[23] = "SEr";

            rot90dict.Add("S", "W");
            rot90dict.Add("W", "N");
            rot90dict.Add("N", "E");
            rot90dict.Add("E", "S");
            rot90dict.Add("SW", "NW");
            rot90dict.Add("NW", "NE");
            rot90dict.Add("NE", "SE");
            rot90dict.Add("SE", "SW");

            rot180dict.Add("S", "N");
            rot180dict.Add("W", "E");
            rot180dict.Add("N", "S");
            rot180dict.Add("E", "W");
            rot180dict.Add("SW", "NE");
            rot180dict.Add("NW", "SE");
            rot180dict.Add("NE", "SW");
            rot180dict.Add("SE", "NW");

            read_railbase_bitmaps(Form1.railroadfolder+@"railpics\");

            //Nodepattern: "i:S;o:NE,N.i:N,NE;o:S"

            railtypes.Add(new railclass("W,E", true, true, ""));  //straight
            railtypes.Add(new railclass("N,S", true, true, ""));  //straight
            railtypes.Add(new railclass("SW,NE", true, true, "")); //diagonal
            railtypes.Add(new railclass("NW,SE", true, true, "")); //diagonal

            add4types("S,E", false,false, ""); //90-deg turn
            add4types("S,NE", true,false, ""); //curve right
            add4types("S,NW", true, false,""); //curve left

            railtypes.Add(new railclass("SW,NE.SE,NW", true,false, "i:SW;o:NE.i:SE;o:NW")); //X-crossing
            railtypes.Add(new railclass("S,N.E,W", true,false, "i:S;o:N.i:E;o:W")); // +-crossing

            add4types("S,N.S,NE", true,false, "i:S;o:N,NE");  //switches
            add4types("S,N.S,NW", true, false, "i:S;o:N,NW");
            add4types("SW,NE.SW,N", true, false, "i:SW;o:N,NE");
            add4types("SW,NE.SW,E", true, false, "i:SW;o:E,NE");
            add4types("S,NW.S,NE", true, false, "i:S;o:NW,NE");
            add4types("SW,N.SW,E", true, false, "i:SW;o:N,E");
            add4types("S,N.S,NW.S,NE", false, false, "i:S;o:N,NE,NW"); //triple switch straight
            add4types("SW,NE.SW,N.SW,E", false, false, "i:SW;o:N,NE,E"); //triple switch diagonal

            railtypes.Add(new railclass("SW,NE.SW,N.S,N.S,NE", false, false, "i:S,SW;o:N,NE")); //crossing switches
            railtypes.Add(new railclass("SE,NW.SE,N.S,N.S,NW", false, false, "i:S,SE;o:N,NW"));
            railtypes.Add(new railclass("SW,NE.SW,E.W,NE.W,E", false, false, "i:W,SW;o:E,NE"));
            railtypes.Add(new railclass("SE,NW.SE,W.E,NW.E,W", false, false, "i:SE,E;o:N,NW"));

            add4types("SE,NE", false, false, ""); //C-turn

            //railtypes.Add(new railclass("w,e.w,se", true));

        }

        public static string getleaf(string fn)
        {
            string s = fn.Substring(fn.LastIndexOf('\\'));
            return s;
        }

        //Color [A=255, R=75, G=52, B=11]
        Color sleepercolor = Color.FromArgb(255, 75, 52, 11);
        Color trackcolor = Color.FromArgb(255, 0, 0, 0);
        //Color[A = 255, R = 255, G = 255, B = 255]

        public static void read_railbase_bitmaps(string folder)
        {
            foreach (string fn in Directory.GetFiles(folder))
            {
                Console.WriteLine(fn);
                if (fn.Contains("rail_"))
                    continue;
                Bitmap bb = new Bitmap(fn);
                Console.WriteLine(bb.HorizontalResolution);
                string[] fnparts = getleaf(fn).Split('.');
                string defstring = fnparts[1].Replace("-", ",");
                railclass rc = new railclass(defstring, false, false, "", bb);
                railbasetypes.Add(rc);
                if (fnparts[2] == "D")
                {
                    raildoubledict.Add(defstring, rc);
                    if (defstring.Length > 2)
                        raildoubledict.Add(reversedefstring(defstring), rc);
                }
                else
                {
                    railbasedict.Add(defstring, rc);
                    if (defstring.Length > 2)
                        railbasedict.Add(reversedefstring(defstring), rc);
                }
            }

        }

        public static void prepare_bitmap_files(string folder)
        {
            foreach (string fn in Directory.GetFiles(folder))
            {
                Console.WriteLine(fn);
                if (fn.Contains("rail_"))
                    continue;
                Bitmap bb = new Bitmap(fn);
                Console.WriteLine(bb.HorizontalResolution);
                string[] fnparts = getleaf(fn).Split('.');
                string defstring = fnparts[1].Replace("-", ",");
                //Bitmap dum = new Bitmap(bb.Width, bb.Height, PixelFormat.Format32bppArgb);

                ////using (Graphics gdum = Graphics.FromImage(dum))
                ////{
                ////    //gdum.FillRectangle(backgroundbrush, new Rectangle(0, 0, squareclass.squaresize, squareclass.squaresize));
                ////    gdum.DrawImageUnscaled(bb, new Point(0, 0));
                ////    gdum.Flush();
                ////}
                //for (int i = 0; i < 32; i++)
                //    for (int j = 0; j < 32; j++)
                //        dum.SetPixel(i, j, bb.GetPixel(i, j));
                //dum.SetResolution(96, 96);
                railbasetypes.Add(new railclass(defstring, false, false, "",bb));
                string newfn = folder+fnparts[0].Replace("rail","rr")+"." + fnparts[1];
                if (fnparts[2] == "D")
                    newfn += ".D";
                else
                    newfn += ".S";
                newfn += ".bmp";
                //dum.Save(newfn);
                //if (fnparts[0].Contains("curve"))
                //{
                //    bb.RotateFlip(RotateFlipType.RotateNoneFlipY);
                //    newfn = newfn.Replace(fnparts[1], "flipped");
                //    bb.Save(newfn);
                //}

                //if (fnparts[0].Contains("straight") || fnparts[0].Contains("diagonal"))
                //{
                //    string newdef = rot90string(defstring);
                //    newfn = newfn.Replace(fnparts[1], newdef.Replace(",","-"));
                //    bb.RotateFlip(RotateFlipType.Rotate90FlipNone);
                //    bb.Save(newfn);
                //}

                if (!(fnparts[0].Contains("straight") || fnparts[0].Contains("diagonal")))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        defstring = rot90string(defstring);
                        string rotfn = newfn.Replace(fnparts[1], defstring.Replace(",", "-"));
                        bb.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        bb.Save(rotfn);
                    }
                }
            }

        }

        private static void add4types(string defstring,bool hasdouble, bool stationplace, string nodepattern)
        {
            string s = defstring.ToUpper();
            string ns = nodepattern;
            railtypes.Add(new railclass(s,hasdouble,stationplace,nodepattern));
            for (int i = 0; i < 3; i++)
            {
                s = rot90string(s);
                ns = rot90nodestring(ns);
                railtypes.Add(new railclass(s, hasdouble,stationplace,ns));
            }
        }

        private static string reversedefstring(string defstring)
        {
            string s = "";
            string[] tdef = defstring.ToUpper().Split('.');
            int itrack = 0;
            foreach (string td in tdef)
            {
                if (td.Contains(","))
                {
                    string[] ends = td.Split(',');
                    s += ends[1] + "," + ends[0] + ".";
                }
                else
                    s += td + ".";
            }

            return s.Trim('.');

        }

        private static string permutedefstring(string defstring)
        {
            string s = "";
            string[] tdef = defstring.ToUpper().Split('.');
            for (int i = tdef.Length - 1; i >= 0; i--)
                s += tdef[i] + ".";

            return s.Trim('.');

        }

        private static string rot90string(string defstring)
        {
            string s = "";
            string[] tdef = defstring.ToUpper().Split('.');
            //int itrack = 0;
            foreach (string td in tdef)
            {
                if (td.Contains(","))
                {
                    string[] ends = td.Split(',');
                    s += rot90dict[ends[0]] + "," + rot90dict[ends[1]] + ".";
                }
                else
                    s += rot90dict[td] + ".";
            }

            return s.Trim('.');
        }

        private static string rot90nodestring(string nodepattern)
        {
            //Nodepattern: "i:S;o:NE,N.i:N,NE;o:S"

            string s = "";
            if (string.IsNullOrEmpty(nodepattern))
                return s;
            string[] tdef = nodepattern.Split('.');
            foreach (string td in tdef)
            {
                foreach (string nt in td.Split(';'))
                {
                    string[] ntt = nt.Split(':');
                    s += ntt[0]+":";
                    foreach (string side in ntt[1].Split(','))
                    {
                        s += rot90dict[side] + ",";
                    }
                    s = s.Trim(',')+";";
                }
                s = s.Trim(';') + ".";
            }

            return s.Trim('.');
        }

        public List<trackconnectionclass> connections() //lists ALL track connections
        {
            return connections(0, 0);
        }

        public List<trackconnectionclass> connections(int u,int v) //lists track connections towards u,v
        {
            List<trackconnectionclass> lt = new List<trackconnectionclass>();
            foreach (squaretrackclass stc in tracks)
            {
                foreach (trackconnectionclass tc in stc.conn)
                {
                    if ((u == 0) && (v == 0))
                        lt.Add(tc);
                    if ((tc.u == u) && (tc.v == v))
                        lt.Add(tc);
                }
            }
            return lt;
        }

        public railclass() //make empty railclass
        {
        }
        public railclass(string defstring, bool hasdouble, bool stationplacepar,string nodepattern) //making single track; defstring: "s,nw.e,nw.se,nw"
        {
            init_railclass(defstring, hasdouble, stationplacepar, nodepattern, null);

        }

        public railclass(string defstring, bool hasdouble, bool stationplacepar, string nodepattern, Bitmap pic)
        {
            init_railclass(defstring, hasdouble, stationplacepar, nodepattern, pic);

        }

        public void init_railclass(string defstring, bool hasdouble, bool stationplacepar, string nodepattern, Bitmap pic)
        {
            this.pattern = defstring.ToUpper();
            this.doubletrack = hasdouble;
            this.stationplace = stationplacepar;
            this.nodepattern = nodepattern;
            string[] tdef = this.pattern.Split('.');
            tracks = new squaretrackclass[tdef.Length];
            int itrack = 0;
            foreach (string td in tdef)
            {
                tracks[itrack] = new squaretrackclass();
                tracks[itrack].name = td;
                string[] ends = td.Split(',');
                tracks[itrack].conn[0] = get_trackconnection(ends[0], 'c');
                if (ends.Length > 1)
                    tracks[itrack].conn[1] = get_trackconnection(ends[1], 'c');
                else
                    tracks[itrack].conn[1] = get_trackconnection(ends[0], 'c');
           
                itrack++;
            }

            //Nodepattern: "i:S;o:NE,N.i:N,NE;o:S"
            if (!string.IsNullOrEmpty(nodepattern))
                this.nodetypes = parse_nodepattern(nodepattern);

            if (pic == null)
                make_template();
            else
                this.template = pic;
        }

        public string pattern_to_nodepattern() //works only for simple patterns!
        {
            string[] pat = this.pattern.Split(',');
            string s = "i:" + pat[0] + ";o:" + pat[1];// + ".i:" + pat[1] + ";o:" + pat[0];
            return s;
        }

        public List<nodetypeclass> parse_nodepattern(string nodepattern)
        {
            //make both directions!
            List<nodetypeclass> nl = new List<nodetypeclass>();

            string[] nodes = nodepattern.Split('.');
            foreach (string node in nodes)
            {
                nodetypeclass nc1 = new nodetypeclass();
                nodetypeclass nc2 = new nodetypeclass();
                string[] ends = node.Split(';');
                foreach (string end in ends)
                {
                    string[] ss = end.Split(':');
                    if (ss[0] == "i")
                    {
                        foreach (string sss in ss[1].Split(','))
                        {
                            nc1.inconn.Add(sss);
                            nc2.outconn.Add(sss);
                        }
                    }
                    else
                    {
                        foreach (string sss in ss[1].Split(','))
                        {
                            nc2.inconn.Add(sss);
                            nc1.outconn.Add(sss);
                        }
                    }
                }
                nl.Add(nc1);
                nl.Add(nc2);
            }
            return nl;
        }
        public railclass makedouble() //making double track from single template
        {
            railclass rd = new railclass();
            return rd;
        }

        public railclass(string singleside) //making single -> double fork from a given side
        {

        }

        public void merge_template(Bitmap mt)
        {
            //Dictionary<int, int> colorstats = new Dictionary<int, int>();
            //colorstats.Add(templatenull.ToArgb(), 0);
            //colorstats.Add(trackcolor.ToArgb(), 0);
            //colorstats.Add(sleepercolor.ToArgb(), 0);
            for (int i=0;i<squareclass.squaresize;i++)
                for (int j = 0; j < squareclass.squaresize; j++)
                {
                    if (mt.GetPixel(i, j).Equals(trackcolor))
                        this.template.SetPixel(i, j, trackcolor);
                    else if (mt.GetPixel(i, j).Equals(sleepercolor))
                    {
                        if (this.template.GetPixel(i, j).Equals(templatenull))
                            this.template.SetPixel(i, j, sleepercolor);
                    }
                    //colorstats[this.template.GetPixel(i, j).ToArgb()]++;
                }
            //Console.WriteLine("null:    " + colorstats[templatenull.ToArgb()]);
            //Console.WriteLine("track:   " + colorstats[trackcolor.ToArgb()]);
            //Console.WriteLine("sleeper: " + colorstats[sleepercolor.ToArgb()]);
        }

        public void make_template()
        {
            //Console.WriteLine(this.pattern);
            foreach (squaretrackclass st in this.tracks)
            {

                //Console.WriteLine("adding " + st.name);
                if (this.template == null)
                    this.template = (Bitmap)railbasedict[st.name].template.Clone();
                else
                    this.merge_template(railbasedict[st.name].template);
            }
        }

        public static trackconnectionclass get_trackconnection(string sidepar, char lr)
        {
            var q = from c in trackconnections
                    where c.side == sidepar
                    where c.leftright == lr
                    select c;
            if (q.Count() == 1)
                return q.First();
            else
                return null;
        }

        private static Pen trackpen = new Pen(Brushes.Black, 4);
        private static SolidBrush backgroundbrush = new SolidBrush(templatenull);
        public void draw_template()
        {
            this.template = new Bitmap(squareclass.squaresize, squareclass.squaresize);
            Graphics g = Graphics.FromImage(this.template);
            g.FillRectangle(backgroundbrush, new Rectangle(0,0, squareclass.squaresize, squareclass.squaresize));

            int sq = squareclass.squaresize;
            int sq2 = (int)Math.Round(0.5 * sq);
            int sq3 = 3 * squareclass.squaresize;
            int sq32 = sq3 / 2;
            int sq2r2 = (int)Math.Round(0.5*sq*Math.Sqrt(2));

            foreach (squaretrackclass stc in tracks)
            {
                    Rectangle rect;
                    int ry = 0;
                    int rx = 0;
                    int offset = 0;
                //bool drawcurve = true;
                    string sl = stc.conn[0].side + stc.conn[1].side;
                if (sl.Length == 2)
                {
                    if ((stc.conn[0].pixel.X == stc.conn[1].pixel.X)
                || (stc.conn[0].pixel.Y == stc.conn[1].pixel.Y))
                    {
                        g.DrawLine(trackpen, stc.conn[0].pixel, stc.conn[1].pixel);
                        //drawcurve = false;
                    }
                    else
                    {
                        //90-deg bend
                        if ((sl == "SE") || (sl == "ES"))
                        {
                            rx = sq2;
                            ry = sq2;
                        }
                        else if ((sl == "SW") || (sl == "WS"))
                        {
                            rx = -sq2;
                            ry = sq2;
                        }
                        else if ((sl == "NW") || (sl == "WN"))
                        {
                            rx = -sq2;
                            ry = -sq2;
                        }
                        else if ((sl == "NE") || (sl == "EN"))
                        {
                            rx = sq2;
                            ry = -sq2;
                        }
                        rect = new Rectangle(rx, ry, sq, sq);
                        g.DrawArc(trackpen, rect, 0, 360);
                    }
                }
                else if (sl.Length == 3) //45-deg bend
                {
                    int k = 0;
                    if (stc.conn[k].side.Length > 1)
                        k = 1;
                    switch (stc.conn[k].side)
                    {
                        case "W":
                            if (stc.conn[1 - k].side == "NE")
                            {
                                rx = stc.conn[k].pixel.X - sq32;
                                ry = stc.conn[k].pixel.Y - sq3;
                            }
                            else
                            {
                                rx = stc.conn[k].pixel.X - sq32;
                                ry = stc.conn[k].pixel.Y;
                            }
                            break;
                        case "E":
                            if (stc.conn[1 - k].side == "NW")
                            {
                                rx = stc.conn[k].pixel.X - sq32;
                                ry = stc.conn[k].pixel.Y - sq3;
                            }
                            else
                            {
                                rx = stc.conn[k].pixel.X - sq32;
                                ry = stc.conn[k].pixel.Y;
                            }
                            break;
                        case "N":
                            if (stc.conn[1 - k].side == "SW")
                            {
                                rx = stc.conn[k].pixel.X - sq3;
                                ry = stc.conn[k].pixel.Y - sq32;
                            }
                            else
                            {
                                rx = stc.conn[k].pixel.X;
                                ry = stc.conn[k].pixel.Y - sq32;
                            }
                            break;
                        case "S":
                            if (stc.conn[1 - k].side == "NW")
                            {
                                rx = stc.conn[k].pixel.X - sq3;
                                ry = -sq2;
                            }
                            else
                            {
                                rx = stc.conn[k].pixel.X;
                                ry = stc.conn[k].pixel.Y - sq32;
                            }
                            break;
                    }
                    if (rx < 0)
                        rx += offset;
                    if (ry < 0)
                        ry += offset;
                    rect = new Rectangle(rx, ry, sq3 - offset, sq3 - offset);
                    g.DrawArc(trackpen, rect, 0, 360);

                }
                else //Diagonal or C-bend
                {
                    bool drawcurve = true;
                    switch (sl)
                    {
                        case "NWNE":
                        case "NENW":
                            rx = sq2 - sq2r2;
                            ry = -sq2 - sq2r2;
                            break;
                        case "NWSW":
                        case "SWNW":
                            rx = -sq2 - sq2r2;
                            ry = sq2 - sq2r2;
                            break;
                        case "SWSE":
                        case "SESW":
                            rx = sq2 - sq2r2;
                            ry = sq32 - sq2r2;
                            break;
                        case "SENE":
                        case "NESE":
                            rx = sq32 - sq2r2;
                            ry = sq2 - sq2r2;
                            break;
                        default: //diagonal
                            g.DrawLine(trackpen, stc.conn[0].pixel, stc.conn[1].pixel);
                            drawcurve = false;
                            break;
                    }
                    rect = new Rectangle(rx, ry, 2 * sq2r2, 2 * sq2r2);
                    if (drawcurve)
                        g.DrawArc(trackpen, rect, 0, 360);
                }
                
            }

            g.Flush();
        }
    }
}
