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
    public partial class FormNewtrain : Form
    {
        trainclass tc; 

        public FormNewtrain(mapclass map)
        {
            InitializeComponent();
            tc = new trainclass(map);
            foreach (enginetypeclass et in trainclass.enginetypes)
            {
                LB_enginetype.Items.Add(et.name);
            }
        }

        public trainclass GetNewTrain()
        {
            return tc;
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LB_enginetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LB_enginetype.SelectedItem == null)
                return;
            string etname = LB_enginetype.SelectedItem.ToString();
            enginetypeclass et = (from c in trainclass.enginetypes
                                  where c.name == etname
                                  select c).FirstOrDefault();
            tc.engine = new engineclass(et, 2021);

            if (tc.itinerary != null)
                createbutton.Enabled = true;
        }

        private void createbutton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void itinerarybutton_Click(object sender, EventArgs e)
        {
            if (tc.set_itinerary())
                if (tc.engine != null)
                    createbutton.Enabled = true;
            
        }
    }
}
