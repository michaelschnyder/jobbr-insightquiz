﻿using InsightQuizzer.Core;
using log4net.Config;

namespace InsightQuizzer.CommandLine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var solver = new InsightEmployeeQuizSolver();

            solver.Start();
        }
    }
}
