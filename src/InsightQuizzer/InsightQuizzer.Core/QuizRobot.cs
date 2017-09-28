using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using InsightQuizzer.Core.InsightQuiz;

namespace InsightQuizzer.Core
{
    public class QuizRobot
    {
        public event EventHandler<UnknownEmployeeEventArgs> NewEmployee; 
        public event EventHandler<ReturningEmployeeEventArgs> KnownEmployee; 
        public event EventHandler<QuestionResolvedEventArgs> QuestionResolved;
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public void AnswerQuestions(int limitMaxMatches = 10)
        {
            var matches = new Dictionary<string, Dictionary<string, bool>>();

            var quiz = new Quiz();

            while (matches.Count(m => m.Value.Any(c => c.Value)) < 10)
            {
                var isNew = true;

                // Check if question is already in store
                var q = quiz.NextQuextion();

                var stringHash = Encoding.UTF8.GetString(q.ThumbHash);

                Dictionary<string, bool> storedResult = null;

                if (matches.ContainsKey(stringHash))
                {
                    isNew = false;
                    storedResult = matches[stringHash];
                    var possibleSoltion = storedResult.FirstOrDefault(kvp => kvp.Value);

                    OnReturningEmployee(new ReturningEmployeeEventArgs()
                    {
                        IsSolutionKnown = possibleSoltion.Value,
                        Solution = possibleSoltion.Key
                    });
                }
                else
                {
                    storedResult = new Dictionary<string, bool>();
                    matches.Add(stringHash, storedResult);

                    OnUnknownEmployee(new UnknownEmployeeEventArgs());
                }

                // find that ma code that was recorded
                var pastMatch = storedResult.Where(kvp => kvp.Value);

                var keyValuePairs = pastMatch as KeyValuePair<string, bool>[] ?? pastMatch.ToArray();
                bool wasFound = false;
                bool wasSolutionKnown = false;
                string maCode = string.Empty;

                if (keyValuePairs.Any())
                {
                    wasFound = q.Resolve(keyValuePairs.First().Key);

                    if (!wasFound)
                    {
                        // Debugger.Break();
                    }
                    wasSolutionKnown = true;
                    maCode = keyValuePairs.FirstOrDefault().Key;
                }
                else
                {
                    // Answer is not yet known
                    var possibleAnswers = q.Options.Select(o => o.MaCode).ToList();
                    possibleAnswers.RemoveAll(a => storedResult.ContainsKey(a));

                    var random = new Random().Next(0, possibleAnswers.Count - 1);

                    var selected = possibleAnswers[random];

                    wasFound = q.Resolve(selected);

                    storedResult.Add(selected, wasFound);
                    maCode = selected;
                }

                if (wasFound)
                {
                    OnQuestionResolved(new QuestionResolvedEventArgs()
                    {
                        EmpCode = maCode,
                        WasEmptKnown = isNew,
                        WasSolutionKnown = wasSolutionKnown,
                        WasSuccessful = true,
                        Image = q.Thumb
                    });
                }

                OnProgressChanged(new ProgressChangedEventArgs()
                {
                    Current = (double)matches.Count(m => m.Value.Any(c => c.Value)) / 10
                });
            }
        }

        protected virtual void OnQuestionResolved(QuestionResolvedEventArgs e)
        {
            QuestionResolved?.Invoke(this, e);
        }

        protected virtual void OnReturningEmployee(ReturningEmployeeEventArgs e)
        {
            KnownEmployee?.Invoke(this, e);
        }

        protected virtual void OnUnknownEmployee(UnknownEmployeeEventArgs e)
        {
            NewEmployee?.Invoke(this, e);
        }

        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }

    public class ProgressChangedEventArgs
    {
        public double Current { get; set; }
    }

    public class UnknownEmployeeEventArgs : EventArgs
    {
    }

    public class QuestionResolvedEventArgs : EventArgs
    {
        public string EmpCode { get; set; }

        public bool WasEmptKnown { get; set; }

        public bool WasSolutionKnown { get; set; }

        public bool WasSuccessful { get; set; }
        public MemoryStream Image { get; set; }
    }

    public class ReturningEmployeeEventArgs : EventArgs
    {
        public bool IsSolutionKnown { get; set; }

        public string Solution { get; set; }
    }
}
