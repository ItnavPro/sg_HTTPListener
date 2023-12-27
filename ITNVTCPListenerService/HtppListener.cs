using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; 
using System.Net;
using static System.Net.WebRequestMethods;
using log4net;
using System.Web.UI;
using System.Runtime.Remoting.Contexts;
using PluginContracts;
using PluginModels;
using static System.Collections.Specialized.BitVector32;
using System.Net.Sockets;
using Newtonsoft.Json;
// RUN THIS COMMAND FROM CMD AS aDMINISTRATOR
// netsh http add urlacl url=http://+:8009/ user=Everyone
namespace ITNVHTTPListener
{
    //http://localhost:18008/RINGING?UCID=33333333&ConnId=1234567890&ConsultType=XYZ&CallType=1&CalledNumber=765764567&CallingNumbe
    //http://localhost:18008/RINGING?OtherPartyPhone=0522385127&UUI=qwerty;www;&UCID=1234567890&VDNName=VDNTest
    /// </summary>&CampaignNumber=qwerty&AcdNumber=1&UserData=ahahahahahah
    //public class RequestParameters
    //{
    //    public string command { get; set; }
    //    public List<KeyValue> Prms {get; set;}
    //}

    //public class KeyValue
    //{
    //    public string Name { get; set; }
    //    public string Value { get; set; }
    //}
    public class HtppListener
    {
        private static ILog log = Configuration.log;

        private HttpListener listener = null;
        private bool brun;
        public List<EMCConfigurationModel> emcconfig;
        //private Operation operation;
        private List<Operation> operations;
        public HtppListener(List<EMCConfigurationModel> emcconfig)
        {
            if (!System.Net.HttpListener.IsSupported)
            {
                log.Error("System.Net.HttpListener  is unsupported.");
                return;
            }

            this.emcconfig = emcconfig;
        }
        public void Listener()
        {
            operations = new List<Operation>();
            foreach (var emcc in emcconfig)
            {
                Operation operation = new Operation(emcc.SectionName, emcc.AssemblyFileName, emcc.Items);
                IPlugin plugin = operation.Initialize();
                if (plugin != null)
                 operations.Add(operation);
            }

            brun = true;
            CreateHttpListener();
            WaitForRequest();
        }
        public void Stop()
        {
            brun = false;
            ParallelLoopResult res = Parallel.ForEach(operations, async op =>
            {
                await Task.Run(
                    new Action(() => { op.Close(); })
                );
            });
        }
        private void CreateHttpListener()
        {
            if (this.listener == null) this.listener = new HttpListener();
            string urlprefix =  $"http://+:{Configuration.Instance.Port}/";
            log.Info($"urlprefix: {urlprefix}");
            if (this.listener.Prefixes.Count <= 0)
            {
                this.listener.Prefixes.Add(urlprefix);
            }
        }
        private void WaitForRequest()
        {
            string prevucid = "";
            string curucid = "";
            while (brun)
            {
                try
                {
                    listener.Start();
                    Console.WriteLine($"Listening... port: {Configuration.Instance.Port}");
                    log.Info($"Listening... port: {Configuration.Instance.Port}");

                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;

                    log.Info($"request.Url: {request.Url}");

                    PluginModels.RequestParametersModel rp = ParseRawUrl(request.RawUrl);

                    if (rp != null && rp.Prms.Count>0)
                    {
                        log.Info($"before rp: {JsonConvert.SerializeObject(rp, Formatting.Indented)}");
                        string value = rp.Prms?.FirstOrDefault(x => x.Name.ToUpper() == "UUI").Value ?? null;
                        if (value != null && value.Substring(0, 2) == "04")
                            rp.Prms.FirstOrDefault(x => x.Name.ToUpper() == "UUI").Value = KeyValue.HexString2Ascii(value.Substring(2));
                        else if (value ==  null || value.Substring(0, 2) == "00")
                            rp.Prms.FirstOrDefault(x => x.Name.ToUpper() == "UUI").Value = "";
                        curucid = rp.Prms.FirstOrDefault(x => x.Name.ToUpper() == "UCID")?.Value ?? "";
                        log.Info($"after rp: {JsonConvert.SerializeObject(rp, Formatting.Indented)}");
                    }

                    
                    if (rp.command == "favicon.ico")
                    {
                        listener.Stop();
                        //Console.WriteLine("favicon.ico");
                        continue;
                    }
                    Console.WriteLine($"{request.Url}");
                    if (prevucid != curucid)
                    {
                        ParallelLoopResult res = Parallel.ForEach(operations, async op =>
                        {
                            await Task.Run(
                                new Action(() => { op.PerformAction(rp); })
                            );
                        });
                        prevucid = curucid;
                    }
                    //foreach (Operation op in operations)
                    //{
                    //    op.PerformAction(rp);
                    //}
                    ResponseAndHidden(context);

                    listener.Stop();
                }
                catch (Exception exc)
                {
                    log.Error(exc.Message);
                    log.Error(exc.StackTrace);

                    this.listener = null;
                    CreateHttpListener();
                    if (exc.Message.ToLower() == "access is denied")
                    {
                        RunPortRegistration();
                    }
                }
            }
        }
        private void RunPortRegistration()
        {
            log.Info("-->");
            // netsh.exe http add urlacl url=http://+:8009/ user=Everyone
            Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = "netsh.exe";
            string urlreg = $"http://+:{Configuration.Instance.Port}/";

            prc.StartInfo.Arguments = $"http add urlacl url={urlreg} user=Everyone";
            prc.StartInfo.Verb = "runas";
            log.Info($"netsh.exe http add urlacl url={urlreg} user=Everyone");

            prc.Start();
            log.Info("<--");
        }
        private void ResponseAndHidden(HttpListenerContext context)
        {
            // response and hidden browser tab
            string htmltxt = "";
            htmltxt += $"<!DOCTYPE html>";
            htmltxt += $"<html>";
            htmltxt += $"<body onload='myFunction()'>";
            //htmltxt += $"connid: {connid}";
            htmltxt += $"<BR>";
            //htmltxt += $"from: {callingNumber}";
            htmltxt += $"<BR>";
            //htmltxt += $"to: {calledNumber}";
            htmltxt += $"<script>";
            htmltxt += "function myFunction() {";
            //htmltxt += $"alert('Page is loaded');";
            htmltxt += $"window.close();";
            htmltxt += "}";
            htmltxt += $"</script>";
            htmltxt += $"</body>";
            htmltxt += $"</html>";

            string responseString = htmltxt; // $"<HTML><BODY>connid: {connid}<BR>from: {callingNumber}<BR>to: {calledNumber}</BODY></HTML>"; //rcucid;

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            HttpListenerResponse response = context.Response;
            response.ContentLength64 = buffer.Length;
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
        private RequestParametersModel ParseRawUrl(string rawurl)
        {
            log.Info($"request.RawUrl: {rawurl}");

            RequestParametersModel rp = new RequestParametersModel();
            rp.Prms = new List<KeyValue>();
            rawurl = rawurl.Trim('/');
            string[] arr = rawurl.Split('?');
            if (arr.Length >= 1)
            {
                rp.command = arr[0];
                if (arr.Length > 1)
                {
                    string prms = arr[1];
                    arr = prms.Split('&');
                    foreach (string s in arr)
                    {
                        KeyValue kv = null;
                        string[] arr1 = s.Split('=');
                        if (arr1.Length >= 1)
                        {
                            kv = new KeyValue
                            {
                                Name = arr1[0],
                                Value = arr1.Length > 1 ? arr1[1] : ""
                            };

                        }
                        if (kv != null) rp.Prms.Add(kv);
                    }
                }
            }
            return rp;
        }

    }
}
