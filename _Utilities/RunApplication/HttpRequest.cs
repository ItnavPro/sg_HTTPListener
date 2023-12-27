using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using System.Net;
using System.Net.Security;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using log4net;

namespace PluginRunApplication
{
    public class HttpRequest
    {
        //public  HttpClient httpclient;
        private string user;
        private string pwd;
        private ILog log;
        public HttpRequest(ILog log)
        {
            //if (httpclient == null)
            //    httpclient = new HttpClient();
            this.log = log;
            this.user = "";
            this.pwd = "";
        }
        public HttpRequest(ILog log, string user, string pwd)
        {
            this.log = log;
            this.user = user;
            this.pwd = pwd;
        }
        public string Request(string url) => PostRequest(url, "");          // obsolete, use GetRequest
        public string GetRequest(string url) => PostRequest(url, "");
        public string PostRequest(string url, string contents)
        {
            Task<string> t = Task.Run(() => Request(url, contents));
            t.Wait(5000);
            return t.Result;
        }
        private async Task<string> Request(string url, string contents)
        {
            string res = "";

            try
            {
                using (HttpClient httpclient = GetHttpClient())
                {
                    Uri uri = new Uri(url);
                    HttpResponseMessage result;
                    if (contents.Length > 0)
                    {
                        HttpContent hcont = new StringContent(contents, Encoding.UTF8, "application/json");
                        result = await httpclient.PostAsync(uri, hcont);
                    }
                    else
                    {
                        result = await httpclient.GetAsync(uri);
                    }
                    res = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                        log.Info("" + result.StatusCode);
                    else
                    {
                        throw new Exception("StatusCode of HTTP Request is not success");
                    }
                }
            }
            catch (Exception exc)
            {
                log.Error($"url: {url} ");
                log.Error($"contents: {contents} ");

                log.Error(exc.Message);
                log.Error(exc.StackTrace);
            }
            return res;
        }
        private HttpClient GetHttpClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors) => true);

            HttpClient httpclient = new HttpClient();

            if (user.Length > 0)
            {
                var byteArray = Encoding.ASCII.GetBytes($"{user}:{pwd}");
                httpclient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }
            return httpclient;
        }

    }
}
