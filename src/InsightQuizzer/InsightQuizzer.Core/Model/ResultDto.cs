namespace InsightQuizzer.Core.Model
{
    public class ResultDto
    {
        public bool IsCorrect { get; set; }
        public SolutionDto Solution { get; set; }
    }
}