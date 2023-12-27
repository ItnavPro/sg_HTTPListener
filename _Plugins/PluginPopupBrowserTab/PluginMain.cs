using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginContracts;
using log4net;
using PluginModels;

namespace PluginPopupBrowserTab
{
    public class PluginMain : IPlugin
    {
        private  ILog log;
        public string PluginName { get; set; }
        public string PluginDll { get; set; }
        private Configuration configuration;
        public void Initialize(List<EMCConfigurationItemModel> Items, ILog log)
        {
            configuration = new Configuration();
            configuration.Initialize(Items, log);
            this.log = log;
        }
        public void Close()
        {
        }
        public void Execute(RequestParametersModel rp)
        {
            // example http://sergeygu/CrmTest1/CRMTest.htm?x=1&s=2
            log.Info(Newtonsoft.Json.JsonConvert.SerializeObject(rp));
            //http://www.google.com?tz={tz}&amp;account={account}

            string tz = "";
            string account = "";
            string vdn = ""; ;
            string uui = rp.Prms.FirstOrDefault(x => x.Name.ToUpper() == "UUI")?.Value ?? "";
            if (uui.Length > 0)
            {
                string[] arr = uui.Split(';');
                if (arr.Length > 2)
                {
                    tz = arr[0];
                    account = arr[1];
                    vdn = arr[2];

                }
                else if (arr.Length > 1)
                {
                    tz = arr[0];
                    account = arr[1];
                    vdn = "";
                }
                else if (arr.Length > 0)
                {
                    tz = arr[0];
                    account = "";
                    vdn = "";
                }
            }
            
            string url = configuration.URL.Replace("{tz}", tz).Replace("{account}", account).Replace("{vdn}", vdn);
            RunApplication.Run(configuration.BrowserExe, url);
        }

    }
}

