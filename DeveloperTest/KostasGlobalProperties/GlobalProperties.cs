using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostasGlobalProperties
{
    public static class GlobalProperties
    {
        public static bool VerboseMode { get; set; }

        static GlobalProperties()
        {
            VerboseMode = false;
        }
    }
}
