using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railroad
{
    public class playerclass
    {
        public int id;
        public string name;

        public int money = 0;

        public bool human = true;

        public List<trainclass> trains = new List<trainclass>();
    }
}
