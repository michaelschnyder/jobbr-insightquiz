using System;
using Jobbr.ArtefactStorage.RavenFS;
using Jobbr.Server.Builder;
using Jobbr.Server.ForkedExecution;
using Jobbr.Server.JobRegistry;
using Jobbr.Storage.RavenDB;
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
                config.JobRunnerExecutable = "../../InsightQuizzer.JobRunner/bin/Debug/InsightQuizzer.JobRunner.exe";
            });

            builder.AddJobs(jobRepo =>
            {
                jobRepo.Define("QuizJob", "InsightQuizzer.Jobs.QuizJob")
                    .WithTrigger("* * * * *", noParallelExecution: true);
            });

            builder.AddRavenDbStorage(config =>
            {
                config.Url = "http://localhost:8080";
                config.Database = "Jobbr";
            });

            builder.AddRavenFsArtefactStorage(config =>
            {
                config.Url = "http://localhost:8080";
                config.FileSystem = "Jobbr";
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
