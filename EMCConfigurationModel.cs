using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITNVHTTPListener
{
    public  class EMCConfigurationModel
    {
        public string SectionName { get; set; }
        public string AssemblyFileName { get; set; }
        public List<EMCConfigurationItemModel> Items { get; set; }
    }
    public class EMCConfigurationItemModel
    {
        public string Formula { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
