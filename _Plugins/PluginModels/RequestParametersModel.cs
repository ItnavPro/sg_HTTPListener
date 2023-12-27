using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PluginModels
{
    public class RequestParametersModel
    {
        public string command { get; set; }
        public List<KeyValue> Prms { get; set; }
    }

    public class KeyValue
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static string HexString2Ascii(string hexString)
        {
            // example of hexString: C81031323334353637383930313233343536FA0800151D79658A82E4
            // C8 - this means what follows is the UUI , after C8 is length of the UUI
            // in this example 10 in hex = 16 in dec
            // 3132... - 12...

            int ln = 0;
            StringBuilder sb = new StringBuilder();
            try
            {
                if (hexString.Substring(0, 2) == "C8")
                {
                    ln = Convert.ToInt32(hexString.Substring(2, 2), 16);
                    hexString = hexString.Substring(4, ln * 2);

                    for (int i = 0; i < ln * 2; i += 2)
                    {
                        sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
                    }
                }
            }
            catch { }
            return sb.ToString();
        }
    }

}
