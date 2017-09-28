using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using InsightQuizzer.Core.InsightQuiz;
using InsightQuizzer.Core.Model;
using RestSharp;

namespace InsightQuizzer.Core
{
    public class Question
    {
        private RestClient client;
        private QuestionDto questionDto;

        public Question(RestClient client, QuestionDto questionDto)
        {
            this.client = client;
            this.questionDto = questionDto;

            this.Options = this.questionDto.Choices.Select(c => new Option(c)).ToList();
        }

        public bool Resolve(string maCode)
        {
            var url = "api/v1/quiz/resolve";

            var request = new RestRequest("api/v1/quiz/resolve");
            request.Method = Method.POST;

            var body = new AnswerDto()
            {
                Code = maCode,
                Question = this.questionDto
            };

            request.AddJsonBody(body);

            var answer = this.client.Execute<ResultDto>(request);

            return answer.Data.IsCorrect;
        }

        public List<Option> Options { get; set; }

        public byte[] ThumbHash =>
            new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(this.questionDto.UserThumb));

        public MemoryStream Thumb => new MemoryStream(Convert.FromBase64String(this.questionDto.UserThumb));
    }
}