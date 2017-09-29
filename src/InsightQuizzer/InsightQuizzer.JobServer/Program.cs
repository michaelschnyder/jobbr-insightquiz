using System;
using Jobbr.Server.Builder;
using Jobbr.Server.ForkedExecution;
using Jobbr.Server.JobRegistry;
using log4net.Config;

namespace InsightQuizzer.JobServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var builder = new JobbrBuilder();

            builder.AddForkedExecution(config =>
            {
                config.JobRunDirectory = "C:/temp";
                config.JobRunnerExecutable = "../../../InsightQuizzer.JobRunner/bin/Debug/InsightQuizzer.JobRunner.exe";
            });

            builder.AddJobs(jobRepo =>
            {
                jobRepo.Define("QuizJob", "InsightQuizzer.Jobs.QuizJob")
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
