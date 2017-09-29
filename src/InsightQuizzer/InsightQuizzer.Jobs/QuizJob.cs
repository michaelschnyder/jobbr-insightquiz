using System;
using InsightQuizzer.Core;

namespace InsightQuizzer.Jobs
{
    public class QuizJob
    {
        public void Run()
        {
            var solver = new InsightEmployeeQuizSolver();

            solver.Robot.ProgressChanged += Robot_ProgressChanged;

            solver.Start();
        }

        private void Robot_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("##jobbr[progress percent='{0:0.00}']", e.Current * 100);
        }
    }
}
