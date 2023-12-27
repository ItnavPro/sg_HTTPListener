using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ITNVHTTPListener
{
    public class Test
    {
        private static ILog log = Configuration.log;

        public static void RunTest()
        {
            Thread thr = new Thread(MainService.ExecuteServer);
            thr.Start();

            Console.WriteLine(">>>");
            Console.ReadKey();
            MainService.Stop();
            Environment.Exit(0);
        }
    }
}
