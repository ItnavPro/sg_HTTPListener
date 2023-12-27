using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using PluginConfiguration;
using log4net;
using PluginModels;

namespace PluginCRMPopup
{
    public class Configuration
    {
        public  ILog log;

        //public static Configuration instance { get; set; }

        // POPUP plugin parameters
        public bool Enabled { get; set; }
        public string ExplorerExe { get; set; }
        public double ExplorerOpenDelay { get; set; }
        public int ExplorerStyle { get; set; }          // style : 0-normal; 1-Hidden; 2-Minimized; 3-Maximized
        public string popupevent { get; set; }          // Alerting/Established
        public int popuptype { get; set; }              //1- new ie, 2 - new tab in ie; 3 - in the same tab; 4 - sametab edge
        public int callerdnlength { get; set; }
        public string URL { get; set; }
        public string URLani { get; set; }
        public string URLlead { get; set; }
        public string URLlogin { get; set; }
        public string prefix { get; set; }          // "03";
        public string calledDNforURL { get; set; }  // "&MT_VDN=";
        public string URLMatch { get; set; }
        public Configuration Initialize(List<EMCConfigurationItemModel> Items, ILog plog)
        {
            log = plog;
            Cfg cfg = new Cfg();
            cfg.GetData<Configuration>(Items, this);

            ExplorerOpenDelay = ExplorerOpenDelay <= 0 ? 5 : ExplorerOpenDelay;
            return this;
        }
    }

}
