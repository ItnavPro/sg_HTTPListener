using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using PluginConfiguration;
using log4net;
using PluginModels;

namespace PluginPopupTest
{
    public class Configuration
    {
        public  ILog log;

        public Configuration instance { get; set; }

        // POPUP plugin parameters
        public string BrowserExe { get; set; }
        public string URL { get; set; }
        public int callerdnlength { get; set; }
        public Configuration Initialize(List<EMCConfigurationItemModel> Items, ILog plog)
        {
            log = plog;
            Cfg cfg = new Cfg();
            cfg.GetData<Configuration>(Items, this);
            return this;
        }
    }

}
