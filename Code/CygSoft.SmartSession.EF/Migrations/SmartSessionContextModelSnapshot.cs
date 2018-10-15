﻿// <auto-generated />
using CygSoft.SmartSession.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CygSoft.SmartSession.EF.Migrations
{
    [DbContext(typeof(SmartSessionContext))]
    partial class SmartSessionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Attachments.FileAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("FileId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("FileAttachments");
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Exercises.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<int>("DifficultyRating");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("OptimalDuration");

                    b.Property<int>("PracticalityRating");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Goals.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.GoalTasks.GoalTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<int?>("MinutesPracticed");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Weighting");

                    b.HasKey("Id");

                    b.ToTable("GoalTasks");
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Keywords.ExerciseKeyword", b =>
                {
                    b.Property<int>("ExerciseId");

                    b.Property<int>("KeywordId");

                    b.HasKey("ExerciseId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("ExerciseKeyword");
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Keywords.FileAttachmentKeyword", b =>
                {
                    b.Property<int>("FileAttachmentId");

                    b.Property<int>("KeywordId");

                    b.HasKey("FileAttachmentId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("FileAttachmentKeyword");
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Keywords.GoalKeyword", b =>
                {
                    b.Property<int>("GoalId");

                    b.Property<int>("KeywordId");

                    b.HasKey("GoalId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("GoalKeyword");
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Keywords.GoalTaskKeyword", b =>
                {
                    b.Property<int>("GoalTaskId");

                    b.Property<int>("KeywordId");

                    b.HasKey("GoalTaskId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("GoalTaskKeyword");
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Keywords.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Sessions.PracticeSessionResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("EndTime");

                    b.Property<int?>("GoalTaskId");

                    b.Property<int?>("Speed");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("GoalTaskId");

                    b.ToTable("PracticeSessionResults");
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Keywords.ExerciseKeyword", b =>
                {
                    b.HasOne("CygSoft.SmartSession.Domain.Exercises.Exercise", "Exercise")
                        .WithMany("ExerciseKeywords")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CygSoft.SmartSession.Domain.Keywords.Keyword", "Keyword")
                        .WithMany("KeywordExercises")
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Keywords.FileAttachmentKeyword", b =>
                {
                    b.HasOne("CygSoft.SmartSession.Domain.Attachments.FileAttachment", "Attachment")
                        .WithMany("FileAttachmentKeywords")
                        .HasForeignKey("FileAttachmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CygSoft.SmartSession.Domain.Keywords.Keyword", "Keyword")
                        .WithMany()
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Keywords.GoalKeyword", b =>
                {
                    b.HasOne("CygSoft.SmartSession.Domain.Goals.Goal", "Goal")
                        .WithMany("GoalKeywords")
                        .HasForeignKey("GoalId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CygSoft.SmartSession.Domain.Keywords.Keyword", "Keyword")
                        .WithMany()
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Keywords.GoalTaskKeyword", b =>
                {
                    b.HasOne("CygSoft.SmartSession.Domain.GoalTasks.GoalTask", "GoalTask")
                        .WithMany("GoalTaskKeywords")
                        .HasForeignKey("GoalTaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CygSoft.SmartSession.Domain.Keywords.Keyword", "Keyword")
                        .WithMany()
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CygSoft.SmartSession.Domain.Sessions.PracticeSessionResult", b =>
                {
                    b.HasOne("CygSoft.SmartSession.Domain.GoalTasks.GoalTask")
                        .WithMany("PracticeSessionResults")
                        .HasForeignKey("GoalTaskId");
                });
#pragma warning restore 612, 618
        }
    }
}
