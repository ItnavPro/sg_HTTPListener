using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using log4net;
using PluginConfiguration;
using PluginModels;

namespace PluginCemax
{
    public class Configuration
    {
        public ILog log;

        public Configuration instance { get; set; }

        // POPUP plugin parameters
        public string ClientName { get; set; }
        public string ThisApp { get; set; }
        public Configuration Initialize(List<EMCConfigurationItemModel> Items, ILog plog)
        {
            log = plog;
            Cfg cfg = new Cfg();
            cfg.GetData<Configuration>(Items, this);
            return this;
        }
    }

}
