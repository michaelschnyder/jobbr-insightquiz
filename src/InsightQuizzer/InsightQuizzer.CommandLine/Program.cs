using InsightQuizzer.Core;
using log4net.Config;

namespace InsightQuizzer.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var solver = new InsightEmployeeQuizSolver();

            solver.Start();
        }
    }
}
