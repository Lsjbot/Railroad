using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Railroad
{
    class hgtclass
    {
        public static int[,] get_hgt_array(string filename)
        {

            Console.WriteLine("get_hgt_array: " + filename);
            int pixvalue = 0;
            int oldpix = 0;
            int mapsize = 1201;
            int[,] map = new int[mapsize, mapsize];

            try
            {
                FileInfo finfo = new FileInfo(filename);
                Console.WriteLine("File size = " + finfo.Length);
                if (finfo.Length > 3000000)
                {
                    Console.WriteLine("Weird file size <cr>");
                    Console.ReadLine();
                }
                //Console.ReadLine();
                byte[] pixels = File.ReadAllBytes(filename);

                Console.WriteLine("pixels = " + pixels.Length);
                int x = 0;
                int y = 0;
                bool odd = true;
                //bool negative = false;
                foreach (byte b in pixels)
                {
                    if (odd)
                    {
                        //if (b < 128)
                        pixvalue = b;
                        //else
                        //{
                        //    negative = true;
                        //    pixvalue = b - 128;
                        //}
                        odd = !odd;
                    }
                    else
                    {
                        //if ( b < 128 )
                        pixvalue = pixvalue * 256 + b;
                        //else
                        //    pixvalue = -(pixvalue * 256 + b - 128);
                        //if (negative)
                        //    pixvalue = -pixvalue;
                        if (pixvalue > 32768)
                            pixvalue = pixvalue - 65536;
                        else if (pixvalue > 9000)
                            pixvalue = oldpix;
                        //Console.WriteLine(pixvalue);
                        map[x, y] = pixvalue;
                        oldpix = pixvalue;
                        x++;
                        if (x >= mapsize)
                        {
                            x = 0;
                            y++;
                        }
                        odd = !odd;
                        //negative = false;
                    }
                }


            }
            catch (FileNotFoundException e)
            {
                Console.Error.WriteLine(e.Message);
                //Console.WriteLine("Not found!");
                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x, y] = 0;
            }
            catch (OutOfMemoryException e)
            {
                Console.Error.WriteLine(e.Message);
                //Console.WriteLine("Not found!");
                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x, y] = 0;
            }
            return map;
        }

        public static string padint(int n, int len)
        {
            string s = n.ToString();
            while (s.Length < len)
                s = "0" + s;
            return s;
        }

        public static string nexthgt(string filenamepar, string dir)
        {
            //Console.WriteLine("nexthgt: filename before = " + filenamepar);
            string filename = filenamepar;

            switch (dir)
            {
                case "north":
                    if (filename.Contains("N"))
                    {
                        int lat = util.tryconvert(filename.Substring(1, 2));
                        lat++;
                        filename = filename.Replace(filename.Substring(0, 3), "N" + padint(lat, 2));
                    }
                    else //"S"
                    {
                        int lat = util.tryconvert(filename.Substring(1, 2));
                        if (lat > 1)
                        {
                            lat--;
                            filename = filename.Replace(filename.Substring(0, 3), "S" + padint(lat, 2));
                        }
                        else
                        {
                            filename = filename.Replace(filename.Substring(0, 3), "N00");
                        }
                    }
                    break;
                case "south":
                    if (filename.Contains("S"))
                    {
                        int lat = util.tryconvert(filename.Substring(1, 2));
                        lat++;
                        filename = filename.Replace(filename.Substring(0, 3), "S" + padint(lat, 2));
                    }
                    else //"N"
                    {
                        int lat = util.tryconvert(filename.Substring(1, 2));
                        if (lat > 0)
                        {
                            lat--;
                            filename = filename.Replace(filename.Substring(0, 3), "N" + padint(lat, 2));
                        }
                        else
                        {
                            filename = filename.Replace(filename.Substring(0, 3), "S01");
                        }
                    }
                    break;
                case "east":
                    if (filename.Contains("E"))
                    {
                        int lon = util.tryconvert(filename.Substring(4, 3));
                        lon++;
                        if (lon >= 180)
                            filename = filename.Replace(filename.Substring(3, 4), "W180");
                        else
                            filename = filename.Replace(filename.Substring(3, 4), "E" + padint(lon, 3));
                    }
                    else //"W"
                    {
                        int lon = util.tryconvert(filename.Substring(4, 3));
                        if (lon > 1)
                        {
                            lon--;
                            filename = filename.Replace(filename.Substring(3, 4), "W" + padint(lon, 3));
                        }
                        else
                        {
                            filename = filename.Replace(filename.Substring(3, 4), "E000");
                        }
                    }
                    break;
                case "west":
                    if (filename.Contains("W"))
                    {
                        int lon = util.tryconvert(filename.Substring(4, 3));
                        lon++;
                        if (lon > 180)
                            filename = filename.Replace(filename.Substring(3, 4), "E179");
                        else
                            filename = filename.Replace(filename.Substring(3, 4), "W" + padint(lon, 3));
                    }
                    else //"E"
                    {
                        int lon = util.tryconvert(filename.Substring(4, 3));
                        if (lon > 0)
                        {
                            lon--;
                            filename = filename.Replace(filename.Substring(3, 4), "E" + padint(lon, 3));
                        }
                        else
                        {
                            filename = filename.Replace(filename.Substring(3, 4), "W001");
                        }
                    }
                    break;
            }

            //Console.WriteLine("nexthgt: filename after = " + filename);
            return filename;
        }

        public static string make_hgt_filename(double lat, double lon)
        {
            int intlat = Convert.ToInt32(Math.Abs(Math.Floor(lat)));
            int intlon = Convert.ToInt32(Math.Abs(Math.Floor(lon)));

            string filename = "N00E999.hgt";

            if (lat < 0)
                filename = filename.Replace('N', 'S');

            if (lon < 0)
                filename = filename.Replace('E', 'W');

            filename = filename.Replace("00", padint(intlat, 2));
            filename = filename.Replace("999", padint(intlon, 3));

            Console.WriteLine(filename);
            return filename;

        }

        public static int[,] get_9x9map(double lat, double lon)
        {
            //int[,] centermap = get_3x3map(lat, lon);

            int map3x3size = 3603; //centermap.GetLength(0);

            int[,] map = new int[3 * map3x3size, 3 * map3x3size];
            for (int x = 0; x < 3 * map3x3size; x++)
                for (int y = 0; y < 3 * map3x3size; y++)
                    map[x, y] = 0;

            int xoff = map3x3size;
            int yoff = map3x3size;

            for (int u = -1; u <= 1; u++)
                for (int v = -1; v <= 1; v++)
                {
                    int[,] map3x3 = get_3x3map(lat - 3 * u, lon + 3 * v);


                    for (int x = 0; x < map3x3size; x++)
                        for (int y = 0; y < map3x3size; y++)
                            map[x + (u + 1) * xoff, y + (v + 1) * yoff] = map3x3[x, y];

                }

            return map;

        }

        public static int[,] get_3x3map(double lat, double lon)
        {
            int mapsize = 1201;

            string dir = Form1.extractdir;
            string filename = make_hgt_filename(lat, lon);

            int[,] map;

            //if (filename == mapfilecache)
            //    map = mapcache;
            //else
            //{
            //    mapfilecache = filename;
                Console.WriteLine("Garbage collection:");
                GC.Collect();
                Console.WriteLine("Making map array..." + mapsize);
                map = new int[3 * mapsize, 3 * mapsize];
                Console.WriteLine("Map array done.");
                for (int x = 0; x < 3 * mapsize; x++)
                    for (int y = 0; y < 3 * mapsize; y++)
                        map[x, y] = 0;


                // ...
                // .x.
                // ...

                Console.WriteLine("Getting first map square...");
                int[,] map0 = get_hgt_array(dir + filename);

                int xoff = mapsize;
                int yoff = mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ...
                // x..
                // ...

                Console.WriteLine("Getting 2nd map square...");
                filename = nexthgt(filename, "west");
                map0 = get_hgt_array(dir + filename);

                xoff = 0;
                yoff = mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // x..
                // ...
                // ...

                filename = nexthgt(filename, "north");
                map0 = get_hgt_array(dir + filename);

                xoff = 0;
                yoff = 0;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // .x.
                // ...
                // ...

                filename = nexthgt(filename, "east");
                map0 = get_hgt_array(dir + filename);

                xoff = mapsize;
                yoff = 0;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ..x
                // ...
                // ...

                filename = nexthgt(filename, "east");
                map0 = get_hgt_array(dir + filename);

                xoff = 2 * mapsize;
                yoff = 0;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ...
                // ..x
                // ...


                filename = nexthgt(filename, "south");
                map0 = get_hgt_array(dir + filename);

                xoff = 2 * mapsize;
                yoff = mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ...
                // ...
                // ..x

                filename = nexthgt(filename, "south");
                map0 = get_hgt_array(dir + filename);

                xoff = 2 * mapsize;
                yoff = 2 * mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ...
                // ...
                // .x.

                filename = nexthgt(filename, "west");
                map0 = get_hgt_array(dir + filename);

                xoff = mapsize;
                yoff = 2 * mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ...
                // ...
                // x..

                filename = nexthgt(filename, "west");
                map0 = get_hgt_array(dir + filename);

                xoff = 0;
                yoff = 2 * mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

            //    mapcache = map;
            //}

            return map;
        }

        public static int get_x_pixel(double lon, double orilon)
        {
            return get_x_pixel(lon, orilon, 1201);
        }

        public static int get_x_pixel(double lon, double orilon, int mapsize) //mapsize should be one third of actual mapsize!
        {
            double fraction = lon - Math.Floor(lon);
            int pix = Convert.ToInt32((Math.Floor(lon) - Math.Floor(orilon) + 1) * mapsize + mapsize * fraction);
            return pix;
        }

        public static int get_y_pixel(double lat, double orilat)
        {
            return get_y_pixel(lat, orilat, 1201);
        }

        public static int get_y_pixel(double lat, double orilat, int mapsize) //mapsize should be one third of actual mapsize!
        {
            double fraction = lat - Math.Floor(lat);
            int pix = 3 * mapsize - Convert.ToInt32((Math.Floor(lat) - Math.Floor(orilat) + 1) * mapsize + mapsize * fraction);
            return pix;
        }

        public static int[,] condense_map(int[,] hgtmap, Rectangle rect, int mapsizex, int mapsizey)
        {
            double ppp = rect.Width / (float)mapsizex;
            if (ppp < 1)
                ppp = 1;
            int pixperpix = (int)Math.Round(ppp);
            Console.WriteLine("pixperpix = " + pixperpix);
            int[,] outmap = new int[mapsizex, mapsizey];
            for (int i=0;i<mapsizex;i++)
                for (int j=0;j<mapsizey;j++)
                {
                    double sum = 0;
                    double sum2 = 0;
                    int n0 = 0;
                    int nland = 0;
                    for (int u=0;u<pixperpix;u++)
                        for (int v=0;v<pixperpix;v++)
                        {
                            int x = rect.Left + i * pixperpix + u;
                            int y = rect.Top + j * pixperpix + v;
                            sum += hgtmap[x, y];
                            sum2 += hgtmap[x, y] * hgtmap[x, y];
                            if (hgtmap[x, y] == 0)
                                n0++;
                            else
                                nland++;
                        }
                    if (n0 > nland)
                        outmap[i, j] = 0; //ocean
                    else
                    {
                        outmap[i, j] = (int)(sum / nland);
                    }
                }

            return outmap;
        }

    }
}
