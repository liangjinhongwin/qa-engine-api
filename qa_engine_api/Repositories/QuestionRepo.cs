using Microsoft.EntityFrameworkCore;
using qa_engine_api.Services;
using qa_engine_api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qa_engine_api.Repositories
{
    public class QuestionRepo
    {
        private readonly QaEngineContext _context;

        public QuestionRepo(QaEngineContext context)
        {
            _context = context;
        }

        public IEnumerable<QuestionVM> GetAll()
        {
            IEnumerable<QuestionVM> qList = _context.Questions.Select(q => new QuestionVM()
            {
                Id = q.Id,
                Description = q.Description,
                CreatedOn = q.CreatedOn,
                User = new UserVM()
                {
                    UserName = q.User.UserName,
                    CreatedOn = q.User.CreatedOn
                },
                Answers = q.Answers.Select(a => new AnswerVM()
                {
                    Id = a.Id,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn,
                    User = new UserVM()
                    {
                        UserName = a.User.UserName,
                        CreatedOn = a.User.CreatedOn
                    },
                    Vote = a.Vote
                })
            });

            return qList;
        }

        public Question Get(long id)
        {
            return _context.Questions.FirstOrDefault(q => q.Id == id);
        }

        public QuestionVM GetById(long id)
        {
            QuestionVM question = _context.Questions.Where(q=>q.Id==id).Select(q => new QuestionVM()
            {
                Id = q.Id,
                Description = q.Description,
                CreatedOn = q.CreatedOn,
                User = new UserVM()
                {
                    UserName = q.User.UserName,
                    CreatedOn = q.User.CreatedOn
                },
                Answers = q.Answers.Select(a => new AnswerVM()
                {
                    Id = a.Id,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn,
                    User = new UserVM()
                    {
                        UserName = a.User.UserName,
                        CreatedOn = a.User.CreatedOn
                    },
                    Vote = a.Vote
                })
            }).FirstOrDefault();

            return question;
        }

        public bool Add(Question question)
        {
            try
            {
                _context.Questions.Add(new Question
                {
                    UserName = question.UserName.ToLower(),
                    Description = question.Description,
                    CreatedOn = DateTime.Now
                });
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool Update(Question updatedQuestion)
        {
            try
            {
                Question question = Get(updatedQuestion.Id);
                if (question != null)
                {
                    question.Description = updatedQuestion.Description;
                    question.CreatedOn = DateTime.Now;
                    _context.Questions.Update(question);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool removeQuestion(long id)
        {
            try
            {
                Question question = Get(id);
                _context.Questions.Remove(question);
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
