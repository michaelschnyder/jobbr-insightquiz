using System.Collections.Generic;

namespace InsightQuizzer.Core.Model
{
    public class QuestionDto
    {
        public List<ChoiceDto> Choices { get; set; }
        public string Solution { get; set; }
        public string UserThumb { get; set; }
    }
}