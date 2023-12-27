using System;
using System.IO;
using System.Collections.Generic;
using EhllapiWrapper;

namespace BasicSharp.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //EhllapiWrapperClass.Connect("A");
            //EhllapiWrapperClass.SetCursorPos(10);

            //int x = EhllapiWrapperClass.GetCursorPos();
            //Console.WriteLine($"position: {x}");

            //EhllapiWrapperClass.SendStr("QWERTY");
            //string sss = EhllapiWrapperClass.ReadScreen(10, 20);
            //EhllapiWrapperClass.Disconnect("A");
            foreach (string file in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Tests"), "*.bas"))
            {
                //Interpreter basic = new Interpreter(File.ReadAllText(file));
                string p1 = "qqq1";
                //string p2 = "2";
                //string p3 = "3";
                //Interpreter basic = new Interpreter(File.ReadAllText(file), p1, p2, p3);

                List<string> prms = new List<string>();
                prms.Add(p1);
                //prms.Add(p2);
                //prms.Add(p3);
                Interpreter basic = new Interpreter(File.ReadAllText(file), prms);


                //List<ParameterModel> prms = new List<ParameterModel>();
                //prms.Add(new ParameterModel { name = "p1", value = "qqq2"});
                //prms.Add(new ParameterModel { name = "p2", value = "2" });
                //prms.Add(new ParameterModel { name = "p3", value = "3" });
                //Interpreter basic = new Interpreter(File.ReadAllText(file), prms);

                basic.printHandler += Console.WriteLine;
                basic.inputHandler += Console.ReadLine; 
                try
                {
                    basic.Exec();
                }
                catch (Exception e)
                {
                    Console.WriteLine("BAD");
                    Console.WriteLine(e.Message);
                    continue;
                }
                Console.WriteLine("OK");
            }
            Console.Read();
        }
    }
}
