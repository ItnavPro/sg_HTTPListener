using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginContracts;
using log4net;
using PluginModels;
using System.Web.UI.WebControls;

using PluginRunApplication;
using System.Text.RegularExpressions;

namespace PluginCRMPopup
{
    public class PluginMain : IPlugin
    {
        private  ILog log;
        public string PluginName { get; set; }
        public string PluginDll { get; set; }
        private Configuration configuration;
        private UUIClass uuitp = null;
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
            StationCallModel arg = new StationCallModel
            {
                CalledDN = rp.Prms.FirstOrDefault(x => x.Name.ToUpper() == "StationId")?.Value ?? "",
                CallerDN = rp.Prms.FirstOrDefault(x => x.Name.ToUpper() == "OtherPartyPhone")?.Value ?? "",
                UCID = rp.Prms.FirstOrDefault(x => x.Name.ToUpper() == "UCID")?.Value ?? "",
                UUI = rp.Prms.FirstOrDefault(x => x.Name.ToUpper() == "UUI")?.Value ?? "",
            };

            string url = BuildURLorParams(arg);
            Popup(url);
            
        }

        private string BuildURLorParams(StationCallModel arg)
        {
            uuitp = new UUIClass(configuration, arg);
            uuitp.BuildParameters();
            string url = uuitp.BuildURL();
            log.Info($"url: {url}");
            url = url.Replace("<UCID>", arg.UCID);
            url = url.Replace("<UUI>", arg.UUI);

            url = Regex.Replace(url, @"<(.*?)>", "");

            return url;
        }
        private async void Popup(string URL)
        {
            log.Info($"---> URL: {URL}");
            if (URL.Length > 0)
            {
                await Task.Run(() =>
                {
                    if (configuration.popuptype <= 3)
                    {
                        IEBrowser iebrowser = new IEBrowser(log, false,
                            configuration.ExplorerExe,
                            configuration.ExplorerStyle,
                            configuration.ExplorerOpenDelay);
                        bool brc = iebrowser.IEOpenOnURL(URL, configuration.popuptype);
                    }
                    else
                    {
                        PopupURLSameTab pust = new PopupURLSameTab(log, configuration.ExplorerExe);
                        log.Info($"ExplorerExe: {configuration.ExplorerExe}");
                        log.Info($"URL: {URL}; URLMatch; {configuration.URLMatch}");

                        pust.Run(URL, configuration.URLMatch);
                    }
                });


            }
            else
                log.Info($"url is empty");
            log.Info("<---");
        }

    }
}

