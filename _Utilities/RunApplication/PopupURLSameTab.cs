using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace PluginRunApplication
{
    public enum SHOW_WINDOW
    {
        SW_HIDE = 0,
        SW_SHOWNORMAL = 1,
        SW_NORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_SHOWMAXIMIZED = 3,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA = 8,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_FORCEMINIMIZE = 11
    }
    public class PopupURLSameTab
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref Windowplacement lpwndpl);

        private struct Windowplacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        private  string browserexe = "";
        private string processname = "";
        private log4net.ILog log;
        public PopupURLSameTab(log4net.ILog log, string browserexe)
        {
            this.browserexe = browserexe;
            this.log = log;
            string[] arr = browserexe.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length > 0)
                this.processname = arr[arr.Length - 1].Replace(".exe", "");
            else
            {
                this.processname = "msedge";
                this.browserexe = "msedge.exe";
            }
        }
        public void Run(string popupurl, string matchurl)
        {
            try
            {
                bool handlefound = false;

                Process[] procsEdge = System.Diagnostics.Process.GetProcessesByName(processname);

                foreach (Process proc in procsEdge)
                {
                    Windowplacement placement = new Windowplacement();
                    GetWindowPlacement(proc.MainWindowHandle, ref placement);

                    // Check if window is minimized
                    if (placement.showCmd == (int)SHOW_WINDOW.SW_SHOWMINIMIZED)
                    {
                        //the window is hidden so we restore it
                        ShowWindow(proc.MainWindowHandle.ToInt32(), (int)SHOW_WINDOW.SW_RESTORE);
                    }

                    if (proc.MainWindowHandle == IntPtr.Zero)
                    {
                        continue;
                    }
                    handlefound = true;
                    AutomationElement SearchBar = SearchTab(procsEdge.Length, proc, matchurl);
                    if (SearchBar != null)
                        LaunchURL(proc, SearchBar, popupurl);
                    else
                        RunNewBrowserwithURL(popupurl);
                }
                if (!handlefound)   // browser is not running
                {
                    RunNewBrowserwithURL(popupurl);
                }
            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
            }
        }
        private void LaunchURL(Process proc, AutomationElement SearchBar, string popupurl)
        {
            try
            {
                SetForegroundWindow(proc.MainWindowHandle);
                if (!SearchBar.TryGetCurrentPattern(ValuePattern.Pattern, out object valuePattern))
                {
                    SearchBar.SetFocus();
                    SendKeys.SendWait("^l");
                    SendKeys.SendWait(popupurl);
                }
                else
                {
                    SearchBar.SetFocus();
                    ((ValuePattern)valuePattern).SetValue(popupurl);
                }

                SendKeys.SendWait("{Enter}");
            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
            }
        }
        private AutomationElement SearchTab(int procs, Process proc, string matchurl)
        {
            AutomationElement SearchBar = null;
            bool found = false;
            int numTabs = procs;
            int index = 1;
            //loop all tabs in Edge
            try
            {
                while (index <= numTabs)
                {
                    //get the url of tab
                    AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
                    SearchBar = root.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));

                    if (SearchBar != null)
                    {
                        string str = (string)SearchBar.GetCurrentPropertyValue(ValuePatternIdentifiers.ValueProperty);

                        //Determine whether the url matches and redirect
                        if (str.Contains(matchurl))
                        {
                            found = true;
                            break;
                        }

                    }
                    index++;
                    SetForegroundWindow(proc.MainWindowHandle);
                    SendKeys.SendWait("^{TAB}"); // change focus to next tab
                }

                if (!found)
                {
                    ////matchurl not found at all, open popurl in the 1-st tab
                    //SetForegroundWindow(proc.MainWindowHandle);
                    //SendKeys.SendWait("^1");
                    //AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
                    //SearchBar = root.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));
                    SearchBar = null;
                }
            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
            }
            return SearchBar;
        }
        private void RunNewBrowserwithURL(string popupurl)
        {
            try
            {
                Process prc = null;
                prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = browserexe;
                prc.StartInfo.Arguments = popupurl;
                prc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                prc.Start();
            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
            }
        }
    }
}