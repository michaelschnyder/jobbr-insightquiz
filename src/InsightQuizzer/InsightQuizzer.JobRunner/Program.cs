using InsightQuizzer.Jobs;
using Jobbr.Runtime;
using Jobbr.Runtime.ForkedExecution;
using log4net.Config;

namespace InsightQuizzer.JobRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var jobAssemblyToQueryJobs = typeof(QuizJob).Assembly;

            var runtime = new ForkedRuntime(new RuntimeConfiguration {JobTypeSearchAssembly = jobAssemblyToQueryJobs});

            runtime.Run(args);
        }
    }
}
