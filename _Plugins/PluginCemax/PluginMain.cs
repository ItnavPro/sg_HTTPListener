using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginContracts;

using log4net;
using System.Xml.Linq;
using PluginModels;

namespace PluginCemax
{
    //http://localhost:18008/RINGING?ConnId=1234567890&ConsultType=XYZ&CallType=1&CalledNumber=765764567&CallingNumber=0522385127&CampaignNumber=qwerty&AcdNumber=1&UserData=ahahahahahah
    public class PluginMain : IPlugin
    {
        private  ILog log;

        public string PluginName { get ; set; }
        public string PluginDll { get; set; }
        private Configuration configuration;
        //public void Initialize(string name, ILog log)
        public void Initialize(List<EMCConfigurationItemModel> Items, ILog log)
        {
            //Configuration.Initialize(name, log);
            configuration = new Configuration();
            configuration.Initialize(Items, log);
            this.log = log;
            ITNavMessengerEventHandler.StartWorking(configuration, log);
        }
        public void Close()
        {
            ITNavMessengerEventHandler.StopWorking();
        }
        public void Execute(RequestParametersModel rp)
        {
            string callingNumber = (rp.Prms.Where(x => x.Name == "CallingNumber")?.FirstOrDefault()?.Value ?? "");
            callingNumber = callingNumber.Trim().Length >= 15 ? "0" + callingNumber.Replace("tel:+972", "") : callingNumber;
            Console.WriteLine($"callingNumber: {callingNumber}");

            string calledNumber = (rp.Prms.Where(x => x.Name == "CalledNumber")?.FirstOrDefault()?.Value ?? "");
            calledNumber = calledNumber.Trim().Length >= 12 ? "0" + calledNumber.Replace("+972", "") : calledNumber;
            Console.WriteLine($"calledNumber: {calledNumber}");

            string connid = rp.Prms.Where(x => x.Name == "ConnId")?.FirstOrDefault()?.Value ?? "";
            Console.WriteLine($"connid: {connid}");

            string prms = (connid) + ";" +
                (rp.Prms.Where(x => x.Name == "ConsultType")?.FirstOrDefault()?.Value ?? "") + ";" +
                (rp.Prms.Where(x => x.Name == "CallType")?.FirstOrDefault()?.Value ?? "") + ";" +
                (calledNumber) + ";" +
                (callingNumber) + ";" +
                (rp.Prms.Where(x => x.Name == "CampaignNumber")?.FirstOrDefault()?.Value ?? "") + ";" +
                (rp.Prms.Where(x => x.Name == "AcdNumber")?.FirstOrDefault()?.Value ?? "") + ";" +
                (rp.Prms.Where(x => x.Name == "UserData")?.FirstOrDefault()?.Value ?? "");
            //http://localhost:18008/RINGING?ConnId=1234567890&ConsultType=XYZ&CallType=1&CalledNumber=765764567&CallingNumber=0522385127&CampaignNumber=qwerty&AcdNumber=1&UserData=ahahahahahah

            ITNavMessengerEventHandler.SendCommand(configuration.ClientName, rp.command, prms);
            log.Info($"Sent to CEMAX: ClientName: {configuration.ClientName}; Command: {rp.command}");
            log.Info($"Sent to CEMAX: Parameters: {prms}");
        }
    }
}
