using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ACCLib;
using AgileSoftware.Configuration;
using PluginModels;
namespace ITNVHTTPListener
{
    public class EMCConfiguration
    {
        private ACClientCtrlClass acc;
        private PluginConfig pcfg;
        private string section;
        private static List<EMCConfigurationItemModel> pluginlist;
        public static List<EMCConfigurationModel> emcconfig;
        public List<EMCConfigurationModel> Start(string[] args)
        {
            string processArgs = "";
            acc = new ACCLib.ACClientCtrlClass();
            // /z CC Elite Multichannel Desktop /s AVAYAEMC /p 29091 /a U=%%U
            // /z CC Elite Multichannel Desktop /s ITNAVPRO-EMC63 /p 29091 /a U=%%U
            // /z CC Elite Multichannel Desktop /s 172.16.5.207 /p 29091 /a M=%%M
            // /z ITNAVChatService /s 172.16.5.207 /p 29091 /a M=%%M
            // /f ASGUIHost.ini
            //bool b = Array.Exists(args, element => element == "/f");
            if (args == null || args.Length <= 0)
            {
                acc.FileName = Configuration.Instance.ConfigurationFile;
                //processArgs = FillConfigParams(args); 
            }
            else if (Array.Exists(args, element => element == "/f"))
            {
                processArgs = string.Join(" ", args);
                string[] arr = processArgs.Split(new char[] { '/' });
                string fn = arr.Where(x => x.Substring(0, 1) == "f").FirstOrDefault()?.Trim().Substring(1).Trim();
                acc.FileName = fn;
            }
            else
                processArgs = FillConfigParams(args);

            acc.ConfigEnable = true;

            pcfg = new PluginConfig();
            pcfg.initialize(acc);
            ///z CC Elite Multichannel Desktop /s 172.16.5.246 /p 29091 /a M=SERGEYG
            //section = pcfg.ConfigurationSections["Plug In Assembly List"].ConfigurationItems["ITNAVPROAppBar Section"].Value;
            var cs = pcfg.ConfigurationSections.GetEnumerator();

            emcconfig = new List<EMCConfigurationModel>();
            while (cs.MoveNext())
            {
                EMCConfigurationModel m = new EMCConfigurationModel();

                ConfigSection value = (ConfigSection)cs.Current;
                m.SectionName = value.SectionName;
                m.Items = new List<EMCConfigurationItemModel>();
                var y = value.ConfigurationItems.GetEnumerator();
                while (y.MoveNext())
                {
                    EMCConfigurationItemModel item = new EMCConfigurationItemModel();
                    AgileSoftware.Configuration.ConfigItem ci = ((AgileSoftware.Configuration.ConfigItem)y.Current);
                    //item.Formula = ci.Formula;
                    item.Key = ci.Key;
                    item.Value = ci.Value;
                    m.Items.Add(item);
                }
                emcconfig.Add(m);
            }
            Console.WriteLine($"emcconfig: {emcconfig.Count}");
            EMCConfigurationModel pluginlisttemp = emcconfig.Where(x => x.SectionName == "Plug In Assembly List" ).FirstOrDefault<EMCConfigurationModel>();
            EMCConfigurationModel general = emcconfig.Where(x => x.SectionName == "General").FirstOrDefault<EMCConfigurationModel>();
            pluginlist = pluginlisttemp?.Items?.Where(x=>x.Value.Trim().Length>0)?.ToList()?? null;
            if (pluginlist != null)
            {
                int c = emcconfig.RemoveAll(l => !pluginlist.Select(ll => ll.Value).Contains(l.SectionName));
                foreach (var plugin in pluginlist)
                {
                    var z1 = emcconfig.Where(x => x.SectionName == plugin.Value)?.FirstOrDefault() ?? null;
                    if (z1 != null)
                    {
                        var z2 = z1.Items.Where(x => x.Key == "Assembly File Name")?.FirstOrDefault() ?? null;
                        z1.AssemblyFileName = z2.Value;
                    }
                }
            }
            else
            {
                emcconfig = null;
            }
            emcconfig.Add(general);
            return emcconfig;
        }
        private string FillConfigParams(string[] args)
        {
            string swparams = "";
            string processArgs = string.Join(" ", args);
            //foreach (string a in args)
            //{
            //    processArgs += a + " ";
            //}

            foreach (string a in args)
            {
                switch (a)
                {
                    case "/z":
                        swparams = "z";
                        break;
                    case "/s":
                        swparams = "s";
                        break;
                    case "/p":
                        swparams = "p";
                        break;
                    case "/a":
                        swparams = "a";
                        break;
                    default:
                        if (a.Substring(0, 1).Equals("/"))
                        {
                            swparams = "";
                            break;
                        }
                        else
                        {
                            switch (swparams)
                            {
                                case "z":
                                    acc.ApplicationName += a + " ";
                                    break;
                                case "s":
                                    acc.ServerName = a;
                                    break;
                                case "p":
                                    int p;
                                    Int32.TryParse(a, out p);
                                    acc.ServerPort = p;
                                    break;
                                case "a":
                                    acc.ConfigFilter += a;
                                    break;
                            }
                        }
                        break;
                }
            }

            acc.ApplicationName = acc.ApplicationName.Trim();
            acc.ConfigFilter = ProcessingFilter(acc.ConfigFilter);
            acc.ConfigEnable = true;
            return processArgs;
        }
        private string ProcessingFilter(string filter)
        {
            string[] arr;
            if (filter.Contains("%%U") || filter.Contains("%%u"))
                filter = filter.Replace("%%U", Environment.UserName);
            else if (filter.Contains("%%M") || filter.Contains("%%m"))
                filter = filter.Replace("%%M", Environment.MachineName);
            else
            {
                arr = filter.Split(new char[] { '=' });
                string envVar = "";
                if (arr.Length > 1)
                {
                    envVar = arr[1].Replace("%", "");
                    string envVarValue = Environment.GetEnvironmentVariable(envVar);
                    filter = filter.Replace(arr[1], envVarValue);
                }

            }
            return filter;
        }
    }
}
