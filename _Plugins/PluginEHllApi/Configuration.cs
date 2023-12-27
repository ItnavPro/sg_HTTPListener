using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


using log4net;
using PluginConfiguration;
using PluginModels;

namespace PluginEHllApi
{
    public class Configuration
    {
        public ILog log;

        public Configuration instance { get; set; }

        // POPUP plugin parameters
        public string SessionId { get; set; }
        public string VbScript { get; set; }
        public Configuration Initialize(List<EMCConfigurationItemModel> Items, ILog plog)
        {
            log = plog;
            Cfg cfg = new Cfg();
            cfg.GetData<Configuration>(Items, this);
            return this;
        }
    }

}
