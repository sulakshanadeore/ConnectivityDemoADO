using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectivityDemoADO
{
    //to give and take data from the dept table in the database
    internal class Dept
    {
        public int Deptno { get; set; }

        public string Dname { get; set; }

        public string Loc { get; set; }
    }
}
