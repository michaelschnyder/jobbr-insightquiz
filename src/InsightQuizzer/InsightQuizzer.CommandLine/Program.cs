using InsightQuizzer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;

namespace InsightQuizzer.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var solver = new BackgroundResolver();

            solver.Run();
        }
    }
}
