using ITNVHTTPListener;
using log4net;
using PluginContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginModels;

namespace ITNVHTTPListener
{
    public class Operation
    {
        private static ILog log = Configuration.log;
        private IPlugin plugin = null;
        private string PluginName; 
        private string PluginDll;
        private List<EMCConfigurationItemModel> Items;
        public Operation(string PluginName, string PluginDll, List<EMCConfigurationItemModel> Items)
        {
            this.PluginName = PluginName;
            this.PluginDll = PluginDll;
            this.Items = Items;

        }
        public IPlugin Initialize()
        {
            //plugin = GenericPluginLoader<IPlugin>.LoadPlugins(Configuration.Instance.PluginName, Configuration.Instance.PluginDll, Configuration.Instance.PluginPath, log);
            if (PluginDll == null || PluginDll.Length <= 0) return null;
            plugin = GenericPluginLoader<IPlugin>.LoadPlugins(PluginName, PluginDll, log);
            if (plugin != null) 
            { 
                plugin.PluginDll = PluginDll;
                plugin.PluginName = PluginName;
                plugin.Initialize(Items, log);
            }
            else
            {
                log.Error($"PluginName: {PluginName}, PluginDll: {PluginDll} does not exist");
            }
            return plugin;
        }
        public void PerformAction(RequestParametersModel rp)
        {
            plugin.Execute(rp);
        }
        public void Close()
        {
            plugin.Close();
        }
    }
}
