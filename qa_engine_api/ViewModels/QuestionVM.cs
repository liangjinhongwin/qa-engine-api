﻿using qa_engine_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qa_engine_api.ViewModels
{
    public class QuestionVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserVM User { get; set; }
        public IEnumerable<AnswerVM> Answers { get; set; }
    }
}
