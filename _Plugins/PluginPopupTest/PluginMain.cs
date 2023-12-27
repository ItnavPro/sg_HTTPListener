using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginContracts;
using log4net;
using PluginModels;
using System.Threading;

namespace PluginPopupTest
{
    public class PluginMain : IPlugin
    {
        private ILog log;
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
            log.Info(Newtonsoft.Json.JsonConvert.SerializeObject(rp));
            //http://www.google.com?tz={tz}&amp;account={account}
            string tz = rp.Prms.Where(x => x.Name == "UCID")?.FirstOrDefault()?.Value ?? "";
            string account = rp.Prms.Where(x => x.Name == "OtherPartyPhone")?.FirstOrDefault()?.Value ?? "";
            string url = configuration.URL.Replace("{tz}", tz).Replace("{account}", account);
            for (int i = 0; i <= 10; i++)
            {
                //Console.WriteLine(url);
                Console.WriteLine("" + configuration.callerdnlength + "-" + configuration.BrowserExe + "-" + i);
                log.Debug("" + configuration.callerdnlength + "-" + configuration.BrowserExe + "-" + i);
                //Console.WriteLine(tz + "-" + i);
                Thread.Sleep(1000);
            }
            //RunApplication.Run(configuration.BrowserExe, url);

            //PopupForm popupform = new PopupForm();
            //popupform.lblParam1.Text=configuration.URL;
            //popupform.lblParam2.Text = configuration.BrowserExe;
            //popupform.lblParam3.Text = "" + configuration.callerdnlength;
            //popupform.Show();
        }

    }
}

