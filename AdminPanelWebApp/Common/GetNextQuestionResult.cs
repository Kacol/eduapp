using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class GetNextQuestionResult
    {
        public string QuestionContent { get; private set; }
        public int Id { get; private set; }
        public GetNextQuestionResult(string questionContent, int id)
        {
            QuestionContent = questionContent;
            Id = id;
        }
    }
}
