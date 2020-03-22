using qa_engine_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qa_engine_api.ViewModels
{
    public class UserVM
    {
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<QuestionVM> Questions { get; set; }
        public IEnumerable<AnswerVM> Answers { get; set; }
    }
}
