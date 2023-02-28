using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Railroad
{
    public partial class Form1 : Form
    {
        public static string railroadfolder = @"I:\railroad\";
        public static string extractdir = @"I:\dotnwb3\extract\";
        public static playerclass player = new playerclass();
        public static bool paused = false;

        mapclass map = null;
        FormMainmap mm;
        public Form1()
        {
            InitializeComponent();

            terrainclass.init_terrains();
            railclass.initrails();
            trainclass.inittrains();

            //map = new mapclass(128);
        }

        private void mainmapbutton_Click(object sender, EventArgs e)
        {
            if (map == null)
                mapgeneratorbutton_Click(sender, e);
            map.maptype = mapclass.mainmaptype;
            //map.randommap();
            FormMainmap mm = new FormMainmap(map,"Main map");
            mm.Show();
        }

        private void railselectbutton_Click(object sender, EventArgs e)
        {
            FormRailselect fr = new FormRailselect();
            fr.Show();
        }

        private void bitmapbutton_Click(object sender, EventArgs e)
        {
            railclass.prepare_bitmap_files(@"I:\railpics\");
            railselectbutton_Click(sender, e);
        }

        public void memo(string s)
        {
            richTextBox1.AppendText(s + "\n");
            richTextBox1.ScrollToCaret();
        }

        private void nodedisplaybutton_Click(object sender, EventArgs e)
        {
            memo("Nodes:");
            foreach (int inode in map.railgraph.nodedict.Keys)
            {
                string s = inode + ": " + map.railgraph.nodedict[inode].mapsquare.X+","+ map.railgraph.nodedict[inode].mapsquare.Y + "; in: ";
                foreach (int i in map.railgraph.nodedict[inode].inedge)
                    s += " " + i;
                s += "; out: ";
                foreach (int i in map.railgraph.nodedict[inode].outedge)
                    s += " " + i;
                memo(s);
            }
            memo("Edges:");
            foreach (int iedge in map.railgraph.edgedict.Keys)
            {
                string s = iedge + ": " + map.railgraph.edgedict[iedge].fromnode + " -> " + map.railgraph.edgedict[iedge].tonode;
                memo(s);
            }
        }

        private void mapgeneratorbutton_Click(object sender, EventArgs e)
        {
            FormMapgenerator fmg = new FormMapgenerator();
            fmg.ShowDialog();
            map = fmg.newmap;
        }

        private void gamebutton_Click(object sender, EventArgs e)
        {
            int nscreen = Screen.AllScreens.Count();
            memo("nscreen = " + nscreen);

            if (map == null)
                mapgeneratorbutton_Click(sender, e);
            map.maptype = mapclass.mainmaptype;
            //map.randommap();
            Msg msg = new Msg();
            
            msg.Show();
            mm = new FormMainmap(map, "Main map");
            mm.Show();
            FormTrainlist ft = new FormTrainlist(player,map);
            ft.Show();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (paused)
                return;

            foreach (trainclass train in player.trains)
            {
                List<Point> lp = train.move(timer1.Interval,map);
                mm.refreshsquares(lp);
            }
        }

        private void testrotatebutton_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(32, 32);
            Graphics gg = Graphics.FromImage(bm);
            SolidBrush nullbrush = new SolidBrush(templateclass.templatenull);
            gg.FillRectangle(nullbrush, new Rectangle(0, 0, squareclass.squaresize, squareclass.squaresize));
            SolidBrush redbrush = new SolidBrush(Color.Red);
            gg.FillRectangle(redbrush, new Rectangle(0, 15, 16, 4));
            gg.Flush();

            Bitmap bmrot = templateclass.rotatebitmap(bm, util.tryconvert(TB_xshift.Text),util.tryconvert(TB_yshift.Text),util.tryconvert(TB_angle.Text));

            PB_test.Image = bmrot;
        }
    }
}
