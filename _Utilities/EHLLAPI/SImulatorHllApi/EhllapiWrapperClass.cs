using System;
using System.Runtime.InteropServices;
using System.Text;


namespace EhllapiWrapper
{
    public class EhllapiWrapperClass
    {
        private static string Screen = "";
        private static string TextFile = "";
        public static uint Connect(string sessionID)
        {
            TextFile = $"{Environment.CurrentDirectory}\\Tests\\{sessionID}.txt";

            try
            {
                Screen = System.IO.File.ReadAllText(TextFile);
                Screen = Screen.Replace(System.Environment.NewLine, "");
            }
            catch (Exception exc)
            {

            }

            return 0;
        }
        public static uint Disconnect(string sessionID)
        {
            string outputfile = $"{Environment.CurrentDirectory}\\Tests\\{sessionID}XX.txt";
            //TextFile.Replace(".txt", "XX.txt");
            string[] array = new string[24];
            try
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = Screen.Substring(80*i, 80);
                }
                Screen = String.Join(System.Environment.NewLine, array);

                System.IO.File.WriteAllText(outputfile, Screen);
            }
            catch (Exception exc)
            {

            }
            return 0;
        }
        public static uint SetCursorPos(int p)
        {
            try
            {
                System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(Screen);
                strBuilder[p-1] = 'M';
                Screen = strBuilder.ToString();
            }
            catch (Exception exc)
            {

            }

            return 0;
        }
        public static int GetCursorPos()
        {
            int ixn = 0;
            try
            {
                ixn = Screen.IndexOf("M")+1;
            }
            catch (Exception exc)
            {
                ixn = 0;
            }
            return ixn;
        }
        public static uint SendStr(string cmd)
        {
            try
            {
                int ixn = Screen.IndexOf("M");
                Screen = Screen.Remove(ixn, cmd.Length).Insert(ixn, cmd);
            }
            catch (Exception exc)
            {

            }
            return 0;
        }
        public static string ReadScreen(int position, int len)
        {
            string s = "";
            try
            {
                s = Screen.Substring(position-1, len);
                return s;
            }
            catch (Exception exc)
            {
                s = "%%";
            }
            return s;
        }
        public static uint Wait()
        {
            try
            {
                return 0;
            }
            catch (Exception exc)
            {
            }
            return 0;
        }

        public static uint Pause(int halfsecs)
        {
            try
            {
                return 0;
            }
            catch (Exception exc)
            {
            }
            return 0;
        }


    }
}
