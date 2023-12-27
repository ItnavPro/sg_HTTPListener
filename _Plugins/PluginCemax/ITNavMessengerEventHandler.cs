using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using log4net;
using System.Runtime.CompilerServices;

namespace PluginCemax

{
    public class ITNavMessengerEventHandler
    {
        private static ILog log;
        private Configuration configuration;

        static ITNavMessenger.Interface itnavMessenger = null;

        delegate void ITNavMessengerCommand(string cmd, string prms, ref string retvalue);
        //It hooks the  events fired by ITNavMessenger com object
        public static void StartWorking(Configuration configuration, ILog plog)
        {
            log = plog;
            try
            {
                log.Info("->");
                itnavMessenger = new ITNavMessenger.Interface();
                itnavMessenger.Initialize(configuration.ThisApp);
                //Handle the ITNavMessenger event
                itnavMessenger.CommandEvent += new ITNavMessenger.__Interface_CommandEventEventHandler(ITNavMessenger_Command);
            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
            }
            finally
            {
                log.Info("<-");
            }
        }
        public static void StopWorking()
        {
            try
            {
                log.Info("->");
                if (itnavMessenger != null)
                {
                    itnavMessenger.SendCommandToClient("", "EXIT", "");
                    System.Threading.Thread.Sleep(2000);
                    itnavMessenger.Terminate();
                    itnavMessenger = null;
                }
            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
            }
            finally
            {
                log.Info("<-");
            }
        }

        public static string SendCommand(string clientName, string cmd, string prms)
        {
            string rcs = "";
            log.Info("clientName=" + clientName + "; cmd=" + cmd + "; prms=" + prms);
            if (itnavMessenger != null)
                rcs = itnavMessenger.SendCommandToClient(clientName, cmd, prms);
            log.Info("rcs: " + rcs);
            return rcs;
        }
        private static string FromXMLToKVPairString(string input)
        {
            string result = "";
            try
            {
                XDocument xdoc = XDocument.Parse(input);

                foreach (XElement xe in xdoc.Elements(xdoc.Root.Name.LocalName).Elements())
                {
                    result += xe.Name.LocalName + "=" + xe.Value + ";";
                }
            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
            }

            return result;
        }
        static private void ITNavMessenger_Command(string cmd, string prms, ref string retvalue)
        {
            log.Info("=> ");
            cmd = (cmd == null ? "" : cmd.Trim());
            prms = (prms == null ? "" : prms.Trim());

            ITNavInterfaceExecuteCommand(cmd, prms, ref retvalue);
            log.Info("<=");
            //}
        }
        static private void ITNavInterfaceExecuteCommand(string cmd, string prms, ref string retvalue)
        {
            try
            {
                log.Info("=> ");

                cmd = cmd.ToUpper();
                log.Info("cmd=" + cmd + "; prms=" + prms);

                string ucid = "";
                switch (cmd)
                {
                    case "AVAILABLE":
                        break;
                    case "UNAVAILABLE":
                        break;
                    case "ACW":
                        break;
                    case "ALTERNATE":
                        break;
                    case "ANSWER":
                        break;
                    case "HOLD":
                        break;
                    case "UNHOLD":
                        break;
                    case "DISCONNECT":
                        break;
                    case "TRANSFER":
                        break;
                    case "BLINDTRANSFER":
                        break;
                    case "CONFERENCE":
                        break;
                    case "BLINDCONFERENCE":
                        break;
                    case "DTMF":
                        break;
                    case "DIAL":
                        break;
                }
                retvalue = ucid;

            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
                retvalue = "";
            }
            finally
            {
                log.Info($"retvalue: {retvalue}");
            }
        }
   }
}
