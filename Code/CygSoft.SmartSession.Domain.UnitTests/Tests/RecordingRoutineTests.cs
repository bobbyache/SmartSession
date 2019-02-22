﻿using CygSoft.SmartSession.Domain.RecordingRoutines;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class RecordingRoutineTests
    {
        [Test]
        public void Routine_When_Initialized_Reflects_Correct_State()
        {
            var exercises = new List<IExerciseRecorder> { };

            var existingRoutine = new RoutineRecorder(2, "Existing Routine", exercises);
            var newRoutine = new RoutineRecorder("New Routine", exercises);

            Assert.AreEqual(2, existingRoutine.Id);
            Assert.AreEqual("Existing Routine", existingRoutine.Title);

            Assert.AreEqual(0, newRoutine.Id);
            Assert.AreEqual("New Routine", newRoutine.Title);
        }

        [Test]
        public void Routine_Reflects_Total_Recorded_Seconds_Correctly()
        {
            var ex1 = new Mock<IExerciseRecorder>();
            ex1.Setup(obj => obj.RecordedSeconds).Returns(300);
            var ex2 = new Mock<IExerciseRecorder>();
            ex2.Setup(obj => obj.RecordedSeconds).Returns(300);
            var ex3 = new Mock<IExerciseRecorder>();
            ex3.Setup(obj => obj.RecordedSeconds).Returns(300);

            var exercises = new List<IExerciseRecorder>
            {
                ex1.Object,
                ex2.Object,
                ex3.Object
            };

            RoutineRecorder recordingRoutine = new RoutineRecorder("Recording Routine", exercises);
            Assert.That(recordingRoutine.RecordedSeconds, Is.EqualTo(900));
            Assert.That(recordingRoutine.RecordedSecondsDisplay, Is.EqualTo("00:15:00"));
        }
    }
}
