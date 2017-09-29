﻿using System;
using Jobbr.Execution.InProcess;
using Jobbr.Server.Builder;
using Jobbr.Server.JobRegistry;

namespace InsightQuizzer.JobServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new JobbrBuilder();

            builder.AddInProcessExecutor();

            builder.AddJobs(jobRepo =>
            {
                jobRepo.Define("QuizJob", "InsightQuizzer.JobServer.Jobs.QuizJob")
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
