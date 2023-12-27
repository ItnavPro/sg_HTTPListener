using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhllapiWrapper
{
    public class EhllapiConstants
    {
        //******************* EHLLAPI FUNCTIONS ***************************/ 
        #region EHLLAPI_FUNCTIONS
        public const UInt32 HA_CONNECT_PS = 1; /* 000 Connect PS*/
        public const UInt32 HA_DISCONNECT_PS = 2; /* 000 Disconnect PS*/
        public const UInt32 HA_SENDKEY = 3; /* 000 Sendkey function*/
        public const UInt32 HA_WAIT = 4; /* 000 Wait function*/
        public const UInt32 HA_COPY_PS = 5; /* 000 Copy PS function*/
        public const UInt32 HA_SEARCH_PS = 6; /* 000 Search PS function*/
        public const UInt32 HA_QUERY_CURSOR_LOC = 7; /* 000 Query Cursor*/
        public const UInt32 HA_COPY_PS_TO_STR = 8; /* 000 Copy PS to String*/
        public const UInt32 HA_SET_SESSION_PARMS = 9; /* 000 Set Session*/
        public const UInt32 HA_QUERY_SESSIONS = 10; /* 000 Query Sessions*/
        public const UInt32 HA_RESERVE = 11; /* 000 Reserve function*/
        public const UInt32 HA_RELEASE = 12; /* 000 Release function*/
        public const UInt32 HA_COPY_OIA = 13; /* 000 Copy OIA function*/
        public const UInt32 HA_QUERY_FIELD_ATTR = 14; /* 000 Query Field*/
        public const UInt32 HA_COPY_STR_TO_PS = 15; /* 000 Copy string to PS*/
        public const UInt32 HA_STORAGE_MGR = 17; /* 000 Storage Manager*/
        public const UInt32 HA_PAUSE = 18; /* 000 Pause function*/
        public const UInt32 HA_QUERY_SYSTEM = 20; /* 000 Query System*/
        public const UInt32 HA_RESET_SYSTEM = 21; /* 000 Reset System*/
        public const UInt32 HA_QUERY_SESSION_STATUS = 22; /* 000 Query Session*/
        public const UInt32 HA_START_HOST_NOTIFY = 23; /* 000 Start Host*/
        public const UInt32 HA_QUERY_HOST_UPDATE = 24; /* 000 Query Host Update*/
        public const UInt32 HA_STOP_HOST_NOTIFY = 25; /* 000 Stop Host*/
        public const UInt32 HA_SEARCH_FIELD = 30; /* 000 Search Field*/
        public const UInt32 HA_FIND_FIELD_POS = 31; /* 000 Find Field*/
        public const UInt32 HA_FIND_FIELD_LEN = 32; /* 000 Find Field Length*/
        public const UInt32 HA_COPY_STR_TO_FIELD = 33; /* 000 Copy String to*/
        public const UInt32 HA_COPY_FIELD_TO_STR = 34; /* 000 Copy Field to*/
        public const UInt32 HA_SET_CURSOR = 40; /* 000 Set Cursor*/
        public const UInt32 HA_START_CLOSE_INTERCEPT = 41; /* 000 Start Close Intercept*/
        public const UInt32 HA_QUERY_CLOSE_INTERCEPT = 42; /* 000 Query Close Intercept*/
        public const UInt32 HA_STOP_CLOSE_INTERCEPT = 43; /* 000 Stop Close Intercept*/
        public const UInt32 HA_START_KEY_INTERCEPT = 50; /* 000 Start Keystroke*/
        public const UInt32 HA_GET_KEY = 51; /* 000 Get Key function*/
        public const UInt32 HA_POST_INTERCEPT_STATUS = 52; /* 000 Post Intercept*/
        public const UInt32 HA_STOP_KEY_INTERCEPT = 53; /* 000 Stop Keystroke*/
        public const UInt32 HA_LOCK_PS = 60; /* 000 Lock Presentation*/
        public const UInt32 HA_LOCK_PMSVC = 61; /* 000 Lock PM Window*/
        public const UInt32 HA_SEND_FILE = 90; /* 000 Send File function*/
        public const UInt32 HA_RECEIVE_FILE = 91; /* 000 Receive file*/
        public const UInt32 HA_CONVERT_POS_ROW_COL = 99; /* 000 Convert Position*/
        public const UInt32 HA_CONNECT_PM_SRVCS = 101; /* 000 Connect For*/
        public const UInt32 HA_DISCONNECT_PM_SRVCS = 102; /* 000 Disconnect From*/
        public const UInt32 HA_QUERY_WINDOW_COORDS = 103; /* 000 Query Presentation*/
        public const UInt32 HA_PM_WINDOW_STATUS = 104; /* 000 PM Window Status*/
        public const UInt32 HA_CHANGE_SWITCH_NAME = 105; /* 000 Change Switch List*/
        public const UInt32 HA_CHANGE_WINDOW_NAME = 106; /* 000 Change PS Window*/
        public const UInt32 HA_START_PLAYING_MACRO = 110; /* 000 Start playing macro*/
        public const UInt32 HA_START_STRUCTURED_FLD = 120; /* 000 Start Structured*/
        public const UInt32 HA_STOP_STRUCTURED_FLD = 121; /* 000 Stop Structured*/
        public const UInt32 HA_QUERY_BUFFER_SIZE = 122; /* 000 Query Communications*/
        public const UInt32 HA_ALLOCATE_COMMO_BUFF = 123; /* 000 Allocate*/
        public const UInt32 HA_FREE_COMMO_BUFF = 124; /* 000 Free Communications*/
        public const UInt32 HA_GET_ASYNC_COMPLETION = 125; /* 000 Get Asynchronous*/
        public const UInt32 HA_READ_STRUCTURED_FLD = 126; /* 000 Read Structured Field*/
        public const UInt32 HA_WRITE_STRUCTURED_FLD = 127; /* 000 Write Structured*/
        #endregion EHLLAPI_FUNCTIONS
        //******************* EHLLAPI RETURN CODES***************************/ 
        #region EHLLAPI_RETURN_CODES
        public const UInt32 HARC_SUCCESS = 0; /* 000 Good return code.*/
        public const UInt32 HARC99_INVALID_INP = 0; /* 000 Incorrect input*/
        public const UInt32 HARC_INVALID_PS = 1; /* 000 Invalid PS, Not*/
        public const UInt32 HARC_BAD_PARM = 2; /* 000 Bad parameter, or*/
        public const UInt32 HARC_BUSY = 4; /* 000 PS is busy return*/
        public const UInt32 HARC_LOCKED = 5; /* 000 PS is LOCKed, or*/
        public const UInt32 HARC_TRUNCATION = 6; /* 000 Truncation*/
        public const UInt32 HARC_INVALID_PS_POS = 7; /* 000 Invalid PS*/
        public const UInt32 HARC_NO_PRIOR_START = 8; /* 000 No prior start*/
        public const UInt32 HARC_SYSTEM_ERROR = 9; /* 000 A system error*/
        public const UInt32 HARC_UNSUPPORTED = 10; /* 000 Invalid or*/
        public const UInt32 HARC_UNAVAILABLE = 11; /* 000 Resource is*/
        public const UInt32 HARC_SESSION_STOPPED = 12; /* 000 Session has*/
        public const UInt32 HARC_BAD_MNEMONIC = 20; /* 000 Illegal mnemonic*/
        public const UInt32 HARC_OIA_UPDATE = 21; /* 000 A OIA update*/
        public const UInt32 HARC_PS_UPDATE = 22; /* 000 A PS update*/
        public const UInt32 HARC_PS_AND_OIA_UPDATE = 23; /* A PS and OIA update*/
        public const UInt32 HARC_STR_NOT_FOUND_UNFM_PS = 24; /* 000 String not found,*/
        public const UInt32 HARC_NO_KEYS_AVAIL = 25; /* 000 No keys available*/
        public const UInt32 HARC_HOST_UPDATE = 26; /* 000 A HOST update*/
        public const UInt32 HARC_FIELD_LEN_ZERO = 28; /* 000 Field length = 0*/
        public const UInt32 HARC_QUEUE_OVERFLOW = 31; /* 000 Keystroke queue*/
        public const UInt32 HARC_ANOTHER_CONNECTION = 32; /* 000 Successful. Another*/
        public const UInt32 HARC_INBOUND_CANCELLED = 34; /* 000 Inbound structured*/
        public const UInt32 HARC_OUTBOUND_CANCELLED = 35; /* 000 Outbound structured*/
        public const UInt32 HARC_CONTACT_LOST = 36; /* 000 Contact with the*/
        public const UInt32 HARC_INBOUND_DISABLED = 37; /* 000 Host structured field*/
        public const UInt32 HARC_FUNCTION_INCOMPLETE = 38; /* 000 Requested Asynchronous*/
        public const UInt32 HARC_DDM_ALREADY_EXISTS = 39; /* 000 Request for DDM*/
        public const UInt32 HARC_ASYNC_REQUESTS_OUT = 40; /* 000 Disconnect successful.*/
        public const UInt32 HARC_MEMORY_IN_USE = 41; /* 000 Memory cannot be freed*/
        public const UInt32 HARC_NO_MATCH = 42; /* 000 No pending*/
        public const UInt32 HARC_OPTION_INVALID = 43; /* 000 Option requested is*/
        public const UInt32 HARC99_INVALID_PS = 9998; /* 000 An invalid PS id*/
        public const UInt32 HARC99_INVALID_CONV_OPT = 9999; /* 000 Invalid convert*/
        #endregion EHLLAPI_RETURN_CODES

    }
}
