using Microsoft.EntityFrameworkCore;
using qa_engine_api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace qa_engine_api.Services
{
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
                .OnDelete(DeleteBehavior.Cascade); // Prevent cascade delete

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany(u => u.Answers)
                .HasForeignKey(fk => fk.UserName)
                .OnDelete(DeleteBehavior.Cascade); // Prevent cascade delete

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(fk => fk.QuestionId)
                .OnDelete(DeleteBehavior.Cascade); // Prevent cascade delete
        }
    }
}
