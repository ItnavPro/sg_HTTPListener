using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PluginModels;

namespace PluginCRMPopup
{
    /*
            //Examples of UUIs
            //ABBBBBQQQQQQQQQQ;TZ=XXXXXXXXX;Y;VDN;SMSTEMPLATE
            //ABBBBBQQQQQQQQQQ;LD=XXXXXXXXX;Y;VDN;SMSTEMPLATE
            //ABBBBBQQQQQQQQQQ;ANI=XXXXXXXXXX;Y;VDN;SMSTEMPLATE
            //where Y - 0 - cust unident; 1 - ident by ANI; 2 - ident by TZ; 3 - ident by TZ and bank account
            //TZ - teudat zehut; LD - lead; ANI - caller number
            //ABBBBBQQQQQQQQQQ - usually 16 spaces
     */
    public static class CALLTYPE
    {
        static readonly public string TZ = "TZ";
        static readonly public string ANI = "ANI";
        static readonly public string LEAD = "LD";
    }
    public class UUIClass
    {
        public string pbx;
        public string calltype;
        public string ivrparam;
        public string otp;
        public string vdn;

        private string UUI;
        private string ani;
        private Configuration configuration;
        public UUIClass(Configuration configuration, StationCallModel arg)
        {
            this.configuration = configuration;
            UUI = arg.UUI;
            ani = arg.CallerDN;

            if (configuration.calledDNforURL.Length > 0)
                vdn = configuration.calledDNforURL + arg.CalledDN;
            else
                vdn = "";
            configuration.log.Info($"UUIClass, uui: {UUI}, ani: {ani}, vdn: {vdn}");
        }
        public void BuildParameters()
        {
            try
            {
                if (UUI.Length <= 0)
                {
                    BuildDefaultParams(ani);
                }
                else
                {
                    string[] arr = UUI.Split(new char[] { ';' });

                    if (arr.Length > 0)
                        pbx = arr[0];
                    if (arr.Length > 1)
                        ParseTypeValueUUI(arr[1]);
                    else
                        BuildDefaultParams(ani);

                    if (arr.Length > 2)
                        otp = arr[2].Trim();
                    else
                        otp = "";

                    if (calltype == CALLTYPE.ANI)
                    {
                        ivrparam = (ivrparam.Length == 7) ? configuration.prefix + ivrparam : ivrparam;
                    }
                }
            }
            catch (Exception exc)
            {
                configuration.log.Error("UUIClass, BuildParameters: " + exc.Message);
                configuration.log.Error("UUIClass, BuildParameters: " + exc.StackTrace);
            }
        }
        public string BuildURL()
        {
            string url = "";
            if (calltype == CALLTYPE.TZ)
                url = configuration.URL;
            if (calltype == CALLTYPE.LEAD)
                url = configuration.URLlead;
            if (calltype == CALLTYPE.ANI)
                url = configuration.URLani;
            url = url.Replace("<VALUE>", ivrparam);
            url = url.Replace("<Y>", otp);
            url = url + vdn;
            return url;
        }
        private void ParseTypeValueUUI(string typestr)
        {
            try
            {
                string[] arrprm = typestr.Split(new char[] { '=' });
                calltype = arrprm[0].Trim();
                ivrparam = arrprm[1].Trim();
            }
            catch (Exception exc)
            {
                configuration.log.Error("UUIClass, ParseTypeValueUUI: " + exc.Message);
                configuration.log.Error("UUIClass, ParseTypeValueUUI: " + exc.StackTrace);
                BuildDefaultParams(ani);
            }
        }
        private void BuildDefaultParams(string callerdn)
        {
            configuration.log.Info($"UUIClass, BuildDefaultParams, callerdn: {callerdn}");
            if (long.TryParse(callerdn, out long xxx))
            {
                ivrparam = callerdn;
                ivrparam = (ivrparam.Length == 7) ? configuration.prefix + ivrparam : ivrparam;
            }
            else
            {
                ivrparam = "";
            }
            calltype = CALLTYPE.ANI;
            otp = "";
        }
    }

}
