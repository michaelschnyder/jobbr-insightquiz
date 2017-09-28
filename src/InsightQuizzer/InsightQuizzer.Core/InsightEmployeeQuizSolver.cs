using log4net;

namespace InsightQuizzer.Core
{
    public class InsightEmployeeQuizSolver
    {
        private static int MaxResults = 10;

        private static ILog Logger = LogManager.GetLogger(nameof(InsightEmployeeQuizSolver));
        private QuizRobot robot;

        public InsightEmployeeQuizSolver()
        {
            this.robot = new QuizRobot();

            this.robot.NewEmployee += RobotOnNewEmployee;
            this.robot.KnownEmployee += RobotOnKnownEmployee;
            this.robot.QuestionResolved += RobotOnQuestionResolved;
        }

        private void RobotOnQuestionResolved(object sender, QuestionResolvedEventArgs args)
        {
            if (args.WasSuccessful)
            {
                if (!args.WasEmptKnown)
                {
                    Logger.Info("Whohuu. Found the employee in the first shot!");
                    return;
                }

                if (args.WasSolutionKnown)
                {
                    Logger.Info("Found correct solution by using a previous solution");
                    return;
                }

                if (!args.WasSolutionKnown)
                {
                    Logger.Info("Found a solution by probing the possible options");
                    return;
                }
            }
            else
            {
                Logger.Warn($"Didn't found a solution for the {(args.WasEmptKnown ?  "new" : "known")} employee");                
            }
        }

        private void RobotOnKnownEmployee(object sender, ReturningEmployeeEventArgs returningEmployeeEventArgs)
        {
            Logger.Debug("Already known employee showed up again!");
        }

        private void RobotOnNewEmployee(object sender, UnknownEmployeeEventArgs unknownEmployeeEventArgs)
        {
            Logger.Debug("Got a new, yet unknown employee!");
        }

        public void Start()
        {
            this.robot.AnswerQuestions(MaxResults);
        }
    }
}
