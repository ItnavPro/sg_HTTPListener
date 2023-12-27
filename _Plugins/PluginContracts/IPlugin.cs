
using log4net;
using PluginModels;
using System.Collections.Generic;

namespace PluginContracts
{
    public interface IPlugin
    {
        string PluginName { get; set;  }
        string PluginDll { get; set; }
        void Initialize(List<EMCConfigurationItemModel> Items, ILog log);
        void Execute(RequestParametersModel parameters);
        void Close();
    }
}
