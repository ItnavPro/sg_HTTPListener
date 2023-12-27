using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using log4net;

namespace PluginRunApplication
{
    public class IEBrowser
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("User32.dll")]
        static extern int ShowWindow(IntPtr point, int nCmdSh);
        private const int SW_HIDE = 0;
        private const int SW_MAXIMIZED = 3;
        private const int SW_SHOW = 5;
        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        private System.Diagnostics.Process prc = null;
        private ILog log = null;
        private int HWND = 0;

        private bool closeAllIEinstart;
        private string explorerExe;
        private ProcessWindowStyle explorerStyle;
        private double explorerOpenDelay;
        public IEBrowser(ILog log)
        {
            this.log = log; 
            this.closeAllIEinstart = false;
            this.explorerExe = "";

            this.explorerStyle = ProcessWindowStyle.Normal;
            this.explorerOpenDelay = 0;
        }
        public IEBrowser(ILog log, bool closeAllIEinstart, string explorerExe, int explorerStyle, double explorerOpenDelay)
        {
            this.log = log;
            this.closeAllIEinstart = closeAllIEinstart;
            this.explorerExe = explorerExe;
            this.explorerStyle = (explorerStyle < 0 || explorerStyle > 3) ? ProcessWindowStyle.Normal : (ProcessWindowStyle)explorerStyle;
            this.explorerOpenDelay = explorerOpenDelay;
        }
        public bool IEOpenOnURL(string sURL, int popuptype)
        {
            //1- new ie, 2 - new tab in ie; 3 - in the same tab
            bool brc = false;
            log.Info($"popuptype {popuptype}, URL: {sURL}");
            log.Info("popuptype: 1 - new ie, 2 - new tab in ie; 3 - in the same tab");
            switch (popuptype)
            {
                case 1:
                case 2:
                case 3:
                    log.Info($"popuptype, URL:" + sURL);
                    HWND = (int)Run(explorerExe, sURL, (ProcessWindowStyle)explorerStyle);
                    log.Info($"this.HWND: {HWND}");
                    brc = true;
                    break;
            }
            return brc;
        }
        public IntPtr Run(string application, string arg)
        {
            IntPtr ptr = Run(application, arg, ProcessWindowStyle.Normal);
            return ptr;
        }
        public IntPtr Run(string application, string arg, ProcessWindowStyle winstyle)
        {
            try
            {
                prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = application;
                prc.StartInfo.Arguments = arg;
                prc.StartInfo.WindowStyle = winstyle;

                bool brc = prc.Start();

                IntPtr ptr = prc.MainWindowHandle;
                log.Info($"process,Id: {prc.Id}");

                ptr = WaitForHWND();

                return ptr;
            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
                return IntPtr.Zero;
            }
        }
        private IntPtr WaitForHWND ()
        {
            int i = 0;
            IntPtr ptr = prc.MainWindowHandle;

            DateTime n = DateTime.Now.AddSeconds(explorerOpenDelay);
            log.Info($"Run-n:{n}; now:{DateTime.Now}");

            while (ptr == IntPtr.Zero && n > DateTime.Now)
            {
                Thread.Sleep(50);
                ptr = prc.MainWindowHandle;
                i++;
                log.Info($"Run-ptr: {ptr}");
            }
            return ptr;
        }
        private void KilAllIexplore()
        {
            Process[] arrProcesses;
            arrProcesses = Process.GetProcessesByName("iexplore");
            foreach (Process p in arrProcesses)
            {
                p.Kill();
            }

        }
   }
}
