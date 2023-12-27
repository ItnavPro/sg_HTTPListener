using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ITNVHTTPListener
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread thr = new Thread(MainService.ExecuteServer);
            thr.Start();
        }

        protected override void OnStop()
        {
            MainService.Stop();
        }
    }
}
