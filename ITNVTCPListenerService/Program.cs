using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net;
using PluginModels;

namespace ITNVHTTPListener
{
    public static class ExecutionType
    {
        public const string EXE = "exe";
        public const string SERVICE = "service";
        public const string DEBUG = "debug";
    }
    static class Program
    {
        private static ILog log;

        static void Main(string[] args)
        {
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1)
            {
                //MessageBox.Show("Another instance of this program is already running. Cannot proceed further.", "Warning!");
                return;
            }
            Configuration.Initialize(args);
            log = Configuration.log;


            //EMCConfiguration config = new EMCConfiguration();
            //List<EMCConfigurationModel> emccfglst =   config.Start(args);
            //string[] imagePathArgs = Environment.GetCommandLineArgs();

            switch (Configuration.Instance.AppExecutionType)
            {
                case ExecutionType.SERVICE:
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                    {
                        new Service()
                    };
                    ServiceBase.Run(ServicesToRun);
                    break;
                case ExecutionType.DEBUG:
                case ExecutionType.EXE:
                default:
                    ////////for test 
                    Test.RunTest();
                    break;
            }
        }
    }
}
