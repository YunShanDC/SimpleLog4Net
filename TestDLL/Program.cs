using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleLog4Net;

namespace TestDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            //SimpleLogger test = new SimpleLogger("Program");
            SimpleLogger test = new SimpleLogger(typeof(Program));
            test.Debug("Debug");
            test.Info("Info");
            test.Warn("Warn");
            test.Error("Error");
            test.Fatal("Fatal");

            test.Debug("Debug", new Exception("debug"));
            test.Info("Info", new Exception("info"));
            test.Warn("Warn", new Exception("Warn"));
            test.Error("Error", new Exception("Error"));
            test.Fatal("Fatal", new Exception("Fatal"));
            Console.ReadKey();
        }
    }
}
