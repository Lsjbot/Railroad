using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Railroad
{
    public partial class FormMapgenerator : Form
    {
        public mapclass newmap = null;

        public static List<cityclass> citylist = new List<cityclass>();

        public FormMapgenerator()
        {
            InitializeComponent();
        }

        private void randombutton_Click(object sender, EventArgs e)
        {
            newmap = new mapclass(util.tryconvert(TB_sizex.Text), util.tryconvert(TB_sizey.Text),mapclass.mainmaptype);
            int nterr = terrainclass.terrainlist.Count;
            Random rnd = new Random();
            for (int i = 0; i < newmap.mapsizex; i++)
            {
                for (int j = 0; j < newmap.mapsizey; j++)
                {
                    int jt = rnd.Next(nterr);
                    newmap.sq[i, j] = new squareclass(terrainclass.terrainlist[jt], 0, new Point(i, j));
                }
                //Console.WriteLine("maprow " + i);
            }
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void read_citylist()
        {
            read_citylist(new RectangleF(-180, -90, 360, 180));
        }
        private void read_citylist(RectangleF latlon)
        {
            Console.WriteLine("top = " + latlon.Top);
            Console.WriteLine("bottom = " + latlon.Bottom);
            Console.WriteLine("left = " + latlon.Left);
            Console.WriteLine("right = " + latlon.Right);
            using (StreamReader sr = new StreamReader(Form1.railroadfolder + "cities500.txt"))
            {
                int n = 0;
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    if (line[0] == '#')
                        continue;

                    //if (n > 250)
                    //    Console.WriteLine(line);

                    string[] words = line.Split('\t');

                    //bool badone = false;

                    //foreach (string s in words)
                    //    Console.WriteLine(s);

                    //Console.WriteLine(words[0] + "|" + words[1]);

                    //int geonameid = -1;

                    cityclass gn = new cityclass();

                    words[1] = util.initialcap(words[1]);

                    gn.name = words[1];
                    gn.lat = util.tryconvertdouble(words[4]);
                    gn.lon = util.tryconvertdouble(words[5]);
                    if (gn.lat < latlon.Top)
                        continue;
                    if (gn.lat > latlon.Bottom)
                        continue;
                    if (gn.lon < latlon.Left)
                        continue;
                    if (gn.lon > latlon.Right)
                        continue;

                    //if (words[6].Length > 0)
                    //    gn.featureclass = words[6][0];
                    gn.gntype = words[7];

                    //if (!featuredict.ContainsKey(gn.featurecode))
                    //    badone = true;

                    //for (int ii = 0; ii < 4; ii++)
                    //    gn.adm[ii] = -1;
                    //if (countryid.ContainsKey(words[8]))
                    //    gn.adm[0] = countryid[words[8]];
                    //if (!altnamesonly)
                    //    foreach (string ll in words[9].Split(','))
                    //    {
                    //        if ((ll != words[8]) && (countryid.ContainsKey(ll)))
                    //            gn.altcountry.Add(countryid[ll]);
                    //    }
                    //if (adm1dict.ContainsKey(words[8]))
                    //{
                    //    if (adm1dict[words[8]].ContainsKey(words[10]))
                    //        gn.adm[1] = adm1dict[words[8]][words[10]];
                    //    else if (adm1dict[words[8]].ContainsKey("0" + words[10]))
                    //        gn.adm[1] = adm1dict[words[8]]["0" + words[10]];
                    //}
                    //if (gn.adm[1] <= 0)
                    //    provinceless++;

                    //if (adm2dict.ContainsKey(words[8]))
                    //    if (adm2dict[words[8]].ContainsKey(words[10]))
                    //        if (adm2dict[words[8]][words[10]].ContainsKey(words[11]))
                    //            gn.adm[2] = adm2dict[words[8]][words[10]][words[11]];
                    //gn.adm[3] = tryconvert(words[12]);
                    //gn.adm[4] = tryconvert(words[13]);
                    //for (int ii = 1; ii < 4; ii++)
                    //    if (gn.adm[ii] == geonameid)
                    //        gn.adm[ii] = -1;

                    gn.population = util.tryconvertlong(words[14]);
                    gn.elevation = util.tryconvert(words[15]);
                    int dem = util.tryconvert(words[16]);
                    if ((gn.elevation <= 0) && (dem > 0))
                        gn.elevation = dem;
                    //gn.tz = words[17];
                    //gn.moddate = words[18];

                    //if ((gn.featureclass == 'P') && (gn.population <= 0) && (!checkwikidata))
                    //    badone = true;

                    citylist.Add(gn);


                    n++;
                    if ((n % 1000) == 0)
                    {
                        Console.WriteLine("n    (geonames)   = " + n.ToString());
                    }


                }
                Console.WriteLine("n    (geonames)   = " + n.ToString());
            }

        }

        private void cityfilebutton_Click(object sender, EventArgs e)
        {
            read_citylist();
        }

        private void DEMbutton_Click(object sender, EventArgs e)
        {
            string fn = Form1.extractdir+hgtclass.make_hgt_filename(9.7,124.3);
            int[,] hgtmap = hgtclass.get_hgt_array(fn);
            double sum = 0;
            
            for (int i=0;i<1201;i++)
                for (int j=0;j<1201;j++)
            {
                    sum += hgtmap[i, j];
            }
            Console.WriteLine("mean height = " + sum / (1201 * 1201));
        }

        private void latlongbutton_Click(object sender, EventArgs e)
        {
            double lat = util.tryconvertdouble(TB_lat.Text);
            double lon = util.tryconvertdouble(TB_lon.Text);
            int mapsizex = util.tryconvert(TB_sizex.Text);
            int mapsizey = util.tryconvert(TB_sizey.Text);
            double degreewidth = util.tryconvertdouble(TB_degreewidth.Text);
            double degreeheight = (degreewidth * mapsizey) / mapsizex;
            double maxdeg = Math.Max(degreewidth,degreeheight);

            int[,] hgtmap;
            double tophgtlat;
            double lefthgtlon;

            if ( maxdeg <= 0.5)
            {
                string fn = Form1.extractdir + hgtclass.make_hgt_filename(lat,lon);
                hgtmap = hgtclass.get_hgt_array(fn);
                tophgtlat = Math.Ceiling(lat);
                lefthgtlon = Math.Floor(lon);
            }
            else if (maxdeg <= 2.5)
            {
                hgtmap = hgtclass.get_3x3map(lat, lon);
                tophgtlat = Math.Ceiling(lat)+1;
                lefthgtlon = Math.Floor(lon)-1;
            }
            else
            {
                hgtmap = hgtclass.get_9x9map(lat, lon);
                tophgtlat = Math.Ceiling(lat) + 4;
                lefthgtlon = Math.Floor(lon) - 4;
            }

            int hgtsize = hgtmap.GetLength(0);
            double hgtdegrees = hgtsize / 1201;

            double toplat = lat + 0.5*degreeheight;
            double leftlon = lon - 0.5*degreewidth;

            int hgtpixperdegree = 1201;
            RectangleF latlonrect = new RectangleF((float)leftlon, (float)(toplat -degreeheight), (float)degreewidth, (float)degreeheight);
            Rectangle hgtrect = new Rectangle((int)((leftlon - lefthgtlon) * hgtpixperdegree), (int)((tophgtlat - toplat) * hgtpixperdegree), (int)(degreewidth * hgtpixperdegree), (int)(degreeheight * hgtpixperdegree));

            int[,] altmap = hgtclass.condense_map(hgtmap, hgtrect, mapsizex, mapsizey);

            terrainclass oceanterr = (from c in terrainclass.terrainlist where c.name == "Ocean" select c).First();
            terrainclass grassterr = (from c in terrainclass.terrainlist where c.name == "Grass" select c).First();

            newmap = new mapclass(mapsizex, mapsizey,mapclass.mainmaptype);
            for (int i=0;i<mapsizex;i++)
                for (int j=0;j<mapsizex;j++)
                {
                    if (altmap[i,j] == 0)
                        newmap.sq[i, j] = new squareclass(oceanterr, 0, new Point(i, j));
                    else
                        newmap.sq[i, j] = new squareclass(new terrainclass(altmap[i,j],""), 0, new Point(i, j));
                    newmap.sq[i, j].altitude = altmap[i, j];

                }

            newmap.latlonrect = latlonrect;
            newmap.km_per_pixel = (40000 / 360) * degreewidth / newmap.mapsizex;
            citylist.Clear();
            read_citylist(newmap.latlonrect);

            foreach (cityclass cc in citylist)
            {
                Console.WriteLine(cc.name);
                newmap.addname(cc);
            }

            this.Close();
        }
    }
}
