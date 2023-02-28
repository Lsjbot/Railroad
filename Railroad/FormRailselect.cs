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
    public partial class FormRailselect : Form
    {
        private Dictionary<IntPtr, railclass> buttondict = new Dictionary<IntPtr, railclass>();

        public railclass selrail = null;
        public FormRailselect()
        {
            InitializeComponent();

            makerailbuttons(new List<string>(), new List<string>());
        }

        public FormRailselect(List<string> requiredtracks, List<string> vetosides, Point location)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = location.X + 48;
            this.Top = location.Y + 72;
            //this.Location = Screen.AllScreens[1].WorkingArea.Location;

            makerailbuttons(requiredtracks, vetosides);
        }


        public void makerailbuttons(List<string> requiredtracks, List<string> vetosides)
        {
            Console.WriteLine("#requiredtracks: " + requiredtracks.Count);
            int sq = squareclass.squaresize + 16;

            int bx = 0;
            int by = 0;
            int x0 = 5;
            int y0 = 5;

            int nrailtype = railclass.railtypes.Count;

            int perrow = (int)Math.Round(Math.Sqrt(nrailtype)) + 1;

            foreach (railclass rc in railclass.railtypes)
            {
                bool requiredmissing = false;
                bool vetofound = false;
                List<string> connlist = new List<string>();
                foreach (trackconnectionclass tc in rc.connections())
                {
                    if (vetosides.Contains(tc.side))
                        vetofound = true;
                    connlist.Add(tc.id);
                }
                foreach (string req in requiredtracks)
                {
                    if (!connlist.Contains(req))
                    {
                        requiredmissing = true;
                        break;
                    }
                }

                if (vetofound)
                    continue;
                if (requiredmissing)
                    continue;

                Button bb = new Button();
                bb.Left = x0 + bx * sq;
                bb.Top = y0 + by * sq;
                bb.Width = sq;
                bb.Height = sq;
                //Bitmap bdum = new Bitmap(32, 32);
                //bb.BackgroundImage = bdum;
                //for (int i = 0; i < 32; i++)
                //    for (int j = 0; j < 32; j++)
                //        bdum.SetPixel(i, j, rc.template.GetPixel(i, j));
                bb.BackgroundImageLayout = ImageLayout.None;
                bb.BackgroundImage = rc.template;
                bb.BackgroundImageLayout = ImageLayout.None;
                bb.Click += new EventHandler(railbutton_Click);
                this.Controls.Add(bb);
                toolTip1.SetToolTip(bb, rc.pattern);

                buttondict.Add(bb.Handle, rc);

                bx++;
                if (bx >= perrow)
                {
                    by++;
                    bx = 0;
                }

            }

            this.Height = 2 * y0 + (by + 2) * (sq + 2);
            this.Width = 2 * x0 + perrow * (sq + 2);

        }

        private void railbutton_Click(object sender, EventArgs e)
        {
            selrail = buttondict[(sender as Button).Handle];
            this.Close();
        }

    }
}
