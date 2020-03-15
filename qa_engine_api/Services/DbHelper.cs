using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace qa_engine_api.Services
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }

        // Child
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }

    public class Question
    {
        [Key] // Enables auto-increment.
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserName { get; set; }

        // Parernt
        public virtual User User { get; set; }
        // Child
        public virtual ICollection<Answer> Answers { get; set; }
    }

    public class Answer
    {
        [Key] // Enables auto-increment.
        public int Id { get; set; }
        public string Description { get; set; }
        public int Vote { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserName { get; set; }
        public int QuestionId { get; set; }

        // Parernt
        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
    }

    public class QaEngineContext : DbContext
    {
        public QaEngineContext(DbContextOptions<QaEngineContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(fk => fk.UserName)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany(u => u.Answers)
                .HasForeignKey(fk => fk.UserName)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(fk => fk.QuestionId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}
