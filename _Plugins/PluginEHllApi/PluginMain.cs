using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginContracts;
using BasicSharp;
using log4net;
using PluginModels;

namespace PluginEHllApi
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
            string tzvalue = rp.Prms.Where(x => x.Name.ToLower() == "tz")?.Select(x => x.Name)?.FirstOrDefault() ?? "";
            List<ParameterModel> prms = new List<ParameterModel>();
            prms.Add(new ParameterModel { name = "p1", value = configuration.SessionId });
            prms.Add(new ParameterModel { name = "p2", value = tzvalue });
            Interpreter basic = new Interpreter(File.ReadAllText(configuration.VbScript), prms);
        }
    }
}
