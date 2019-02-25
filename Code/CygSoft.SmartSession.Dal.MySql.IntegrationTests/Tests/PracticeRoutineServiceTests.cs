﻿using CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Recording;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests.Tests
{
    [TestFixture]
    public class PracticeRoutineServiceTests
    {
        [Test]
        public void PracticeRoutineService_Creates_New_PracticeRoutineExerciseFor_Exercise()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var exerciseService = new ExerciseService(uow);
                var exercise = new Exercise
                {
                    Title = "Routine Exercise",
                    TargetPracticeTime = 5000,
                    TargetMetronomeSpeed = 120
                };
                exerciseService.Add(exercise);

                var service = new PracticeRoutineService(uow, exerciseService);
                var routineExercise = service.CreatePracticeRoutineExerciseFor(exercise.Id);

                Assert.That(routineExercise.Title, Is.EqualTo(exercise.Title));
                Assert.That(routineExercise.ExerciseId, Is.EqualTo(exercise.Id));
                Assert.That(routineExercise.AssignedPracticeTime, Is.EqualTo(300));
            }
        }

        private PracticeRoutine CreatePracticeRoutine()
        {
            return new PracticeRoutine()
            {
                Title = "New Practice Routine"
            };
        }

        private Exercise CreateExercise()
        {
            Exercise exercise = new Exercise
            {
                TargetMetronomeSpeed = 150,
                TargetPracticeTime = 50000,
                Title = "Created Exercise Title"
            };

            return exercise;
        }
    }
}
