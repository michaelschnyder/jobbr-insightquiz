using InsightQuizzer.Core.Model;

namespace InsightQuizzer.Core.InsightQuiz
{
    public class Option
    {
        private ChoiceDto c;

        public string MaCode => c.Code;
        public string FullName => c.FullName;

        public Option(ChoiceDto c)
        {
            this.c = c;
        }
    }
}