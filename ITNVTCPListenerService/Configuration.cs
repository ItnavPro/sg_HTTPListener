using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using Newtonsoft.Json;
using Itnavpro.Configuration;
using AgileSoftware.ErrorLog;
using PluginModels;

namespace ITNVHTTPListener
{
    public class Configuration
    {
        public static ILog log;

        private static Configuration instance = null;
        public static Configuration Instance { get => instance; }
        public int Port { get; set; }
        public string AppExecutionType { get; set; }

        public string ConfigApplication { get; set; }
        public string ConfigServer { get; set; }
        public string ConfigPort { get; set; }
        public string ConfigServer2 { get; set; }
        public string ConfigPort2 { get; set; }
        public string ConfigFilter { get; set; }
        //public string PluginPath { get; set; } = string.Empty;
        //public string PluginName { get; set; } = string.Empty;
        //public string PluginDll { get; set; } = string.Empty;
        public string ConfigurationFile { get; set; } = string.Empty;

        public static void Initialize(string[] args)
        {
            if (instance == null)
            {
                instance = new Configuration();
                ItnavConfiguration.Initialize(instance);

                instance = (Configuration)ItnavConfiguration.cm;

                log = ItnavConfiguration.log;
                instance.AppExecutionType = instance.AppExecutionType.ToLower();
                instance.Port = instance.Port == 0 ? 18008 : instance.Port;

                instance.LoadEmcConfig(instance.BuildArguments(args));

                instance.PrintConfig();
            }
        }

        private string[] BuildArguments(string[] args)
        {
            if ((args == null || args.Length <= 0 ) && instance.ConfigApplication.Length <=0)
            {
                return null;
            }
            else if ((args == null || args.Length <= 0) && instance.ConfigApplication.Length > 0)
            {
                List<string> largs = new List<string>();
                largs.Add($"/z");
                largs.Add($"{instance.ConfigApplication}");
                largs.Add($"/s");
                largs.Add($"{instance.ConfigServer}");
                largs.Add($"/p");
                largs.Add($"{instance.ConfigPort}");
                if (instance.ConfigServer2.Length > 0)
                {
                    largs.Add($"/s2");
                    largs.Add($"{instance.ConfigServer2}");
                    if (instance.ConfigPort2.Length > 0)
                    {
                        largs.Add($"/p2");
                        largs.Add($"{instance.ConfigPort2}");
                    }
                    else
                    {
                        largs.Add($"/p2");
                        largs.Add($"{instance.ConfigPort}");
                    }
                }
                largs.Add($"/a");
                largs.Add($"{instance.ConfigFilter}");
                return largs.ToArray();
            }
            else
                return args;
        }
        private void LoadEmcConfig(string[] args)
        {
            log.Info($"-------------> args: {JsonConvert.SerializeObject((string[])args)}");
            EMCConfiguration config = new EMCConfiguration();
            List<EMCConfigurationModel> emcconfig = config.Start((string[])args);

            //log.Info($"EMCConfiguration.emcconfig: {JsonConvert.SerializeObject(EMCConfiguration.emcconfig)}");
            //Console.WriteLine($"{JsonConvert.SerializeObject(EMCConfiguration.emcconfig, Formatting.Indented)}");

            string p = emcconfig?
                        .Where(x => x.SectionName.ToLower() == "General".ToLower())?
                        .FirstOrDefault()?.Items?
                        .Where(x => x?.Key.ToLower() == "Port".ToLower())?
                        .Select(x => x?.Value)?.FirstOrDefault() ?? "";
            int.TryParse(p, out int port);
            if (port != 0)
            {
                Configuration.Instance.Port = port;
            }
        }
        private void PrintConfig()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            log.Info($"--------App config---------------> version: {version}");

            log.Info(JsonConvert.SerializeObject(this, Formatting.Indented));

            log.Info($"--------Plugin config------------>");

            log.Info(JsonConvert.SerializeObject(EMCConfiguration.emcconfig, Formatting.Indented));
            
            log.Info($"<---------------------------------");
        }

    }

}
