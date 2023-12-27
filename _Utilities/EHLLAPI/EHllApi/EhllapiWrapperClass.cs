using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EhllapiWrapper
{
    public class EhllapiWrapperClass
    {
        public static uint Connect(string sessionID)
        {
            uint rcf = 9999;
            try
            {
                StringBuilder Data = new StringBuilder(4);
                Data.Append(sessionID);
                UInt32 rc = 0;
                UInt32 f = EhllapiConstants.HA_CONNECT_PS;
                UInt32 l = 4;
                rcf = EhllapiDll.hllapi(out f, Data, out l, out rc);
            }
            catch (Exception exc)
            {

            }
            return rcf;
        }
        public static uint Disconnect(string sessionID)
        {
            uint rcf = 9999;
            try
            {
                StringBuilder Data = new StringBuilder(4);
                Data.Append(sessionID);
                UInt32 rc = 0;
                UInt32 f = EhllapiConstants.HA_DISCONNECT_PS;
                UInt32 l = 4;
                rcf = EhllapiDll.hllapi(out f, Data, out l, out rc);
            }
            catch (Exception exc)
            {
            }
            return rcf;
        }
        public static uint SetCursorPos(int p)
        {
            uint rcf = 9999;
            try
            {
                StringBuilder Data = new StringBuilder(0);
                UInt32 rc = (UInt32)p;
                UInt32 f = EhllapiConstants.HA_SET_CURSOR;
                UInt32 l = 0;
                rcf = EhllapiDll.hllapi(out f, Data, out l, out rc);
            }
            catch (Exception exc)
            {
            }
            return rcf;
        }
        public static int GetCursorPos()
        {
            uint rcf = 9999;
            int p = 0;
            try
            {
                StringBuilder Data = new StringBuilder(0);
                UInt32 rc = 0;
                UInt32 f = EhllapiConstants.HA_QUERY_CURSOR_LOC;
                UInt32 l = 0; //return position
                rcf = EhllapiDll.hllapi(out f, Data, out l, out rc);
                p = (int)l;
            }
            catch (Exception exc)
            {
                p = 0;
            }
            return p;
        }
        public static uint SendStr(string cmd)
        {
            uint rcf = 9999;
            try
            {
                StringBuilder Data = new StringBuilder(cmd.Length);
                Data.Append(cmd);
                UInt32 rc = 0;
                UInt32 f = EhllapiConstants.HA_SENDKEY;
                UInt32 l = (UInt32)cmd.Length;
                rcf = EhllapiDll.hllapi(out f, Data, out l, out rc);
            }
            catch (Exception exc)
            {
            }
            return rcf;
        }
        public static string ReadScreen(int position, int len)
        {
            uint rcf = 9999;
            string txt = "";
            try
            {
                StringBuilder Data = new StringBuilder(3000);
                UInt32 rc = (UInt32)position;
                UInt32 f = EhllapiConstants.HA_COPY_PS_TO_STR;
                UInt32 l = (UInt32)len;
                rcf = EhllapiDll.hllapi(out f, Data, out l, out rc);
                txt = Data.ToString();
            }
            catch (Exception exc)
            {
                txt = "";
            }
            return txt;
        }
        public static uint Wait()
        {
            uint rcf = 9999;
            try
            {
                StringBuilder Data = new StringBuilder(0);
                UInt32 rc = 0;
                UInt32 f = EhllapiConstants.HA_WAIT;
                UInt32 l = 0;
                rcf = EhllapiDll.hllapi(out f, Data, out l, out rc);
                return rcf;
            }
            catch (Exception exc)
            {
            }
            return rcf;
        }
        public static uint Pause(int halfsecs)
        {
            uint rcf = 9999;
            try
            {
                StringBuilder Data = new StringBuilder(0);
                UInt32 rc = 0;
                UInt32 f = EhllapiConstants.HA_PAUSE;
                UInt32 l = (UInt32)halfsecs;
                rcf = EhllapiDll.hllapi(out f, Data, out l, out rc);
                return rcf;
            }
            catch (Exception exc)
            {
            }
            return rcf;
        }

    }
}
