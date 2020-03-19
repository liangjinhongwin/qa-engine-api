using qa_engine_api.Models;
using qa_engine_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qa_engine_api.Repositories
{
    public class AnswerRepo
    {
        private readonly QaEngineContext _context;

        public AnswerRepo(QaEngineContext context)
        {
            _context = context;
        }

        public Answer Get(long id)
        {
            return _context.Answers.FirstOrDefault(a => a.Id == id);
        }

        public bool Add(Answer answer)
        {
            try
            {
                _context.Answers.Add(new Answer
                {
                    UserName = answer.UserName,
                    Description = answer.Description,
                    CreatedOn = DateTime.Now,
                    QuestionId = answer.QuestionId,
                    Vote = 0
                    
                });
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool Update(Answer updatedAnswer)
        {
            try
            {
                Answer answer = Get(updatedAnswer.Id);
                if (answer != null)
                {
                    answer.Description = updatedAnswer.Description;
                    answer.CreatedOn = DateTime.Now;
                    _context.Answers.Update(answer);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Delete(long id)
        {
            try
            {
                Answer answer = Get(id);
                _context.Answers.Remove(answer);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
