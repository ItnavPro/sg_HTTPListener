using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginModels
{
    public class RequestParametersModel
    {
        public string command { get; set; }
        public List<KeyValue> Prms { get; set; }
    }

    public class KeyValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
