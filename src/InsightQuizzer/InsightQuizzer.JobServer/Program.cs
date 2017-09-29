using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobbr.Server.Builder;
using Jobbr.Server.JobRegistry;

namespace InsightQuizzer.JobServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new JobbrBuilder();

            //JobbrBuilder.AddInProcessExecution(config =>
            //{
            //    config.IsRuntimeWaitingForDebugger = true;
            //});

            builder.AddJobs(r =>
            {
                r.Define("QuizJob", "InsightQuizzer.JobServer.Jobs.QuizJob")
                    .WithTrigger("* * * * *", noParallelExecution: true);
            });

            using (var server = builder.Create())
            {
                server.Start();

                Console.ReadLine();

                server.Stop();
            }
        }
    }
}
