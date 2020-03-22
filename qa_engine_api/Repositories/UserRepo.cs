using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using qa_engine_api.Models;
using qa_engine_api.Services;
using qa_engine_api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace qa_engine_api.Repositories
{
    public class UserRepo
    {
        private readonly QaEngineContext _context;

        public UserRepo(QaEngineContext context)
        {
            _context = context;
        }

        public User Get(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName.ToLower());
        }

        public UserVM GetProfile(string userName)
        {
            UserVM userVM = _context.Users.Where(u => u.UserName == userName.ToLower()).Select(user => new UserVM()
            {
                UserName = user.UserName,
                CreatedOn = user.CreatedOn,
                Questions = user.Questions.Select(q => new QuestionVM()
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
                }),
                Answers = user.Answers.Select(a => new AnswerVM()
                {
                    Id = a.Id,
                    Description = a.Description,
                    User = new UserVM()
                    {
                        UserName = a.User.UserName,
                        CreatedOn = a.User.CreatedOn
                    },
                    CreatedOn = a.CreatedOn,
                    Question = new QuestionVM()
                    {
                        Id = a.Question.Id,
                        Description = a.Question.Description,
                        User = new UserVM()
                        {
                            UserName = a.Question.User.UserName,
                            CreatedOn = a.Question.User.CreatedOn
                        },
                        CreatedOn = a.Question.CreatedOn,
                        Answers = a.Question.Answers.Select(ans => new AnswerVM()
                        {
                            Id = ans.Id,
                            Description = ans.Description,
                            CreatedOn = ans.CreatedOn,
                            User = new UserVM()
                            {
                                UserName = ans.User.UserName,
                                CreatedOn = ans.User.CreatedOn
                            }
                        })
                    }
                })
            }).FirstOrDefault();

            return userVM;
        }

        public bool Add(User newUser)
        {
            if (Get(newUser.UserName) != null)
            {
                return false;
            }

            try
            {
                _context.Users.Add(new User
                {
                    UserName = newUser.UserName.ToLower(),
                    Password = newUser.Password,
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
    }
}
