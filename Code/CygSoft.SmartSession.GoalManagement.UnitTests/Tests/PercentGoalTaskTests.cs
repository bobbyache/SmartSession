﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.UnitTests.Tests
{
    [TestFixture]
    public class PercentGoalTaskTests
    {
        [Test]
        public void New_PercentGoalTask_StartDate_IsNull()
        {
            PercentGoalTask task = new PercentGoalTask();
            Assert.That(task.StartDate, Is.Null);
        }

        [Test]
        public void New_PercentGoalTask_CreateDate_IsSet()
        {
            PercentGoalTask task = new PercentGoalTask();
            Assert.That(task.CreateDate, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void New_PercentGoalTask_Has_0_MinutesPracticed()
        {
            PercentGoalTask task = new PercentGoalTask();
            Assert.That(task.MinutesPracticed, Is.EqualTo(0));
        }

        [Test]
        public void New_PercentGoalTask_Has_0_PercentCompleted()
        {
            PercentGoalTask task = new PercentGoalTask();
            Assert.That(task.PercentCompleted, Is.EqualTo(0));
        }

        [Test]
        public void New_PercentGoalTask_Has_0_EffortWeighting()
        {
            PercentGoalTask task = new PercentGoalTask();
            Assert.That(task.Weighting, Is.EqualTo(0));
        }
    }
}
