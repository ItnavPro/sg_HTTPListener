using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EhllapiWrapper
{
    public class EhllapiDll
    {
        //@"C:\SG\VSProjectsPopupEMC4.5\EHLLAPI\dll\BSHLL32"
        const string _dllLocation = @"C:\Program Files\BOSaNOVA\BOSaNOVA Secure\BSHLL32.dll";
        [DllImport(_dllLocation)]
        //[DllImport("pcshll32.dll")]
        public static extern UInt32 hllapi(out UInt32 Func, StringBuilder Data, out UInt32 Length, out UInt32 RetC);
    }
}
