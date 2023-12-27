using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using EhllapiWrapper;
namespace BasicSharp
{
    class BuiltIns
    {
        public static void InstallAll(Interpreter interpreter)
        {
            interpreter.AddFunction("str", Str);
            interpreter.AddFunction("num", Num);
            interpreter.AddFunction("abs", Abs);
            interpreter.AddFunction("min", Min);
            interpreter.AddFunction("max", Max);
            interpreter.AddFunction("not", Not);

            interpreter.AddFunction("connect", Connect);
            interpreter.AddFunction("disconnect", Disconnect);
            interpreter.AddFunction("setcursorpos", SetCursorPos);
            interpreter.AddFunction("getcursorpos", GetCursorPos);
            interpreter.AddFunction("sendstr", SendStr);
            interpreter.AddFunction("readscreen", ReadScreen);
            interpreter.AddFunction("wait", Wait);
        }
        public static Value Str(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return args[0].Convert(ValueType.String);
        }
        public static Value Num(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return args[0].Convert(ValueType.Real);
        }
        public static Value Abs(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(Math.Abs(args[0].Real));
        }
        public static Value Min(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 2)
                throw new ArgumentException();

            return new Value(Math.Min(args[0].Real, args[1].Real));
        }
        public static Value Max(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(Math.Max(args[0].Real, args[1].Real));
        }
        public static Value Not(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(args[0].Real == 0 ? 1 : 0);
        }
        public static Value Connect(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(EhllapiWrapperClass.Connect(args[0].ToString()));
        }
        public static Value Disconnect(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(EhllapiWrapperClass.Disconnect(args[0].ToString()));
        }
        public static Value SetCursorPos(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(EhllapiWrapperClass.SetCursorPos(((int)args[0].Real)));
        }
        public static Value GetCursorPos(Interpreter interpreter, List<Value> args)
        {
            return new Value(EhllapiWrapperClass.GetCursorPos());
        }
        public static Value SendStr(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(EhllapiWrapperClass.SendStr(args[0].ToString()));
        }
        public static Value ReadScreen(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(EhllapiWrapperClass.ReadScreen(((int)args[0].Real), ((int)args[1].Real)));
        }
        public static Value Wait(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(EhllapiWrapperClass.Wait());
        }
    }
}
