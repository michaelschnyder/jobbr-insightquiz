using InsightQuizzer.Core.Model;
using RestSharp;
using RestSharp.Authenticators;

namespace InsightQuizzer.Core
{
    namespace InsightQuiz
    {
        public class Quiz
        {
            public Question NextQuextion()
            {
                var client = new RestClient("http://insight.zuehlke.com");

                // client.Authenticator = new RestSharp.Authenticators.NtlmAuthenticator();
                client.Authenticator = new HttpBasicAuthenticator("ADS\\mis", Properties.Settings.Default.UserPassword);
                var request = new RestRequest("/api/v1/quiz/settings");
                request.Method = Method.POST;

                var response = client.Execute<QuestionDto>(request);

                return new Question(client, response.Data);
            }
        }
    }
}
