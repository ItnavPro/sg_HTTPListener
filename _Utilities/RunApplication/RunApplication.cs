using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginRunApplication
{
    public static class RunApplication
    {
        private static Process prc = null;

        public static Process Run(string application, string arg)
        {
            return Run(application, arg, ProcessWindowStyle.Normal);
        }

        public static Process Run(string application, string arg, ProcessWindowStyle winstyle)
        {
            try
            {
                if (application.Length <= 0) return null;

                prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = application;
                prc.StartInfo.Arguments = arg;
                prc.StartInfo.WindowStyle = winstyle;
                prc.Start();

                return prc;
            }
            catch (Exception exc)
            {
                return null;
            }

        }
        public static void Kill(Process prc)
        {
            try
            {
                if (prc != null)
                {
                    prc.Kill();
                    prc = null;
                }
            }
            catch { }
        }
    }

}
