using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBRTModels;
using ITNVConfiguration;
using ITNVDatabaseLib;

namespace ITNVRTSocketListener
{
    public class RTSocketParser
    {


        public IRTReport Parse(string rcvString)
        {
            IRTReport report = null;

            string reporttype = Configuration.Instance.Report;
            if (reporttype == ReportTypes.AgentReport)
            {
                report = new Agent();
            }
            else if (reporttype == ReportTypes.SkillReport)
            {
                report = new Skill();
            }
            else if (reporttype == ReportTypes.VDNReport)
            {
                report = new VDN();

            }
            return report.Deserialize(rcvString);
        }
    }
}
