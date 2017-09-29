using System;
using Jobbr.Server.Builder;
using Jobbr.Server.ForkedExecution;
using Jobbr.Server.JobRegistry;

namespace InsightQuizzer.JobServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new JobbrBuilder();

            //builder.AddInProcessExecution();
            builder.AddForkedExecution(config =>
            {
                config.JobRunDirectory = "C:/temp";
                config.JobRunnerExecutable = "TODO";
            });

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
