using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginModels
{
    public class StationCallModel
    {
        public string CallerDN { get; set; }
        public string CalledDN { get; set;}
        public string UCID { get; set; }
        public string UUI { get; set;}
    }
}
