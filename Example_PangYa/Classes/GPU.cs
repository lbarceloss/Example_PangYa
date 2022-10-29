using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_PangYa.Classes
{
    internal class GPU
    {
        private string version = null;
        private string name = null;
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
