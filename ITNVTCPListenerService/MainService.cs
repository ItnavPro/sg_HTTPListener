using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net; // For IPAddress
using System.Net.Sockets; // For TcpListener, TcpClient
using log4net;


namespace ITNVHTTPListener
{
    public static class MainService
    {
        private static ILog log = Configuration.log;
        private static HtppListener htppListener;
        public static void ExecuteServer(object args)
        {
            htppListener = new HtppListener(EMCConfiguration.emcconfig);

            var action = new Action(() =>
            {
                htppListener.Listener();
            });

            Task myTask = new Task(action);
            myTask.Start();

        }
        public static void  Stop()
        {
            log.Info("========>");
            htppListener.Stop();
        }
    }
}