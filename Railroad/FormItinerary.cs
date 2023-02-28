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
    public partial class FormItinerary : Form
    {
        public FormItinerary(string[] old_itinerary,int nextstop)
        {
            InitializeComponent();
            foreach (string s in stationclass.stationdict.Keys)
            {
                LB_stations.Items.Add(s);
            }
            if (old_itinerary != null)
            {
                foreach (string sc in old_itinerary)
                    LB_itinerary.Items.Add(sc);
                TB_nextstop.Text = old_itinerary[nextstop];
            }
        }

        private void rightbutton_Click(object sender, EventArgs e)
        {
            if (LB_stations.SelectedIndex >= 0)
            {
                LB_itinerary.Items.Add(LB_stations.Items[LB_stations.SelectedIndex]);
            }
            if (LB_itinerary.Items.Count == 1)
                TB_nextstop.Text = LB_stations.Items[LB_stations.SelectedIndex].ToString();
        }

        private void leftbutton_Click(object sender, EventArgs e)
        {
            if (LB_itinerary.SelectedIndex >= 0)
            {
                LB_itinerary.Items.RemoveAt(LB_itinerary.SelectedIndex);
            }

        }

        private void downbutton_Click(object sender, EventArgs e)
        {
            if (LB_itinerary.SelectedIndex >= 0)
            {
                int i = LB_itinerary.SelectedIndex;
                if (i < LB_itinerary.Items.Count - 1)
                {
                    object o = LB_itinerary.Items[i];
                    LB_itinerary.Items.RemoveAt(i);
                    LB_itinerary.Items.Insert(LB_itinerary.SelectedIndex + 1, o);
                }
            }

        }

        private void upbutton_Click(object sender, EventArgs e)
        {
            if (LB_itinerary.SelectedIndex >= 0)
            {
                int i = LB_itinerary.SelectedIndex;
                if (i > 0)
                {
                    object o = LB_itinerary.Items[i];
                    LB_itinerary.Items.RemoveAt(i);
                    LB_itinerary.Items.Insert(LB_itinerary.SelectedIndex - 1, o);
                }
            }

        }

        public string[] itinerary()
        {
            string[] ss = new string[LB_itinerary.Items.Count];
            for (int i = 0; i < LB_itinerary.Items.Count; i++)
                ss[i] = LB_itinerary.Items[i].ToString();
            return ss;
        }

        public int nextstop()
        {
            for (int i = 0; i < LB_itinerary.Items.Count; i++)
                if (LB_itinerary.Items[i].ToString() == TB_nextstop.Text)
                    return i;
            return 0;
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            if (LB_itinerary.Items.Count >= 2)
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;
        }

        private void setnextbutton_Click(object sender, EventArgs e)
        {
            if (LB_itinerary.SelectedIndex >= 1)
                TB_nextstop.Text = LB_itinerary.Items[LB_itinerary.SelectedIndex].ToString();

        }
    }
}
