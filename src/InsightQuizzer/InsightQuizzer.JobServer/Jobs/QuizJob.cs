using InsightQuizzer.Core;

namespace InsightQuizzer.JobServer.Jobs
{
    public class QuizJob
    {
        public void Run()
        {
            var solver = new InsightEmployeeQuizSolver();

            solver.Start();
        }
    }
}
