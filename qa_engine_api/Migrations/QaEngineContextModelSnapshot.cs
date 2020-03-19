﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using qa_engine_api.Services;

namespace qa_engine_api.Migrations
{
    [DbContext(typeof(QaEngineContext))]
    partial class QaEngineContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("qa_engine_api.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<int>("QuestionId");

                    b.Property<string>("UserName");

                    b.Property<int>("Vote");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserName");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("qa_engine_api.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("UserName");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("qa_engine_api.Models.User", b =>
                {
                    b.Property<string>("UserName")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Password");

                    b.HasKey("UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("qa_engine_api.Models.Answer", b =>
                {
                    b.HasOne("qa_engine_api.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qa_engine_api.Models.User", "User")
                        .WithMany("Answers")
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qa_engine_api.Models.Question", b =>
                {
                    b.HasOne("qa_engine_api.Models.User", "User")
                        .WithMany("Questions")
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
