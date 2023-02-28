using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Railroad
{
    public class buildingclass: templateclass
    {
        public static Dictionary<string, buildingclass> buildingdict = new Dictionary<string, buildingclass>();

        public string name;
        public static void init_buildings()
        {

        }

        public buildingclass(string namepar, Bitmap pic)
        {
            name = namepar;
            template = (Bitmap)pic.Clone();
        }
    }
}
