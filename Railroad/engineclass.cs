using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railroad
{
    public class engineclass
    {
        public enginetypeclass enginetype;
        public int yearbuilt;

        public engineclass(enginetypeclass et,int year)
        {
            enginetype = et;
            yearbuilt = year;
        }
    }
}
