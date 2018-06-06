﻿using CygSoft.SmartSession.GoalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    internal class Goal
    {
        private WeightingCalculator weightingCalculator;
        private List<IGoalFile> goalFiles = new List<IGoalFile>();
        private List<IGoalTask> goalTasks = new List<IGoalTask>();

        public double PercentComplete { get { return 0; } }

        public Goal(int maxTaskWeighting)
        {
            weightingCalculator = new WeightingCalculator(maxTaskWeighting);
            CreateDate = DateTime.Now;
        }

        internal Goal(int maxTaskWeighting, IGoalTask[] goalTasks, IGoalFile[] goalFiles)
        {
            weightingCalculator = new WeightingCalculator(maxTaskWeighting);

            if (goalTasks != null)
                this.goalTasks = new List<IGoalTask>(goalTasks);

            if (goalFiles != null)
                this.goalFiles = new List<IGoalFile>(goalFiles);
        }

        public double FileCount => goalFiles.Count();

        internal void AddTask(GoalTask goalTask)
        {
            weightingCalculator.Update(goalTask.Id, goalTask.Weighting);
            goalTasks.Add(goalTask);
            goalTask.WeightingChanged += GoalTask_WeightingChanged;
        }

        private void GoalTask_WeightingChanged(object sender, EventArgs e)
        {
            GoalTask goalTask = ((GoalTask)sender);
            weightingCalculator.Update(goalTask.Id, goalTask.Weighting);
        }

        public IGoalFile[] Files => goalFiles.ToArray();
        public int MinutesPracticed { get; private set; }

        public DateTime CreateDate { get; private set; }

        public int Weighting => 0;

        public bool IsConsideredComplete => false;

        public int TaskCount
        {
            get
            {
                if (goalTasks != null)
                    return goalTasks.Count();
                return 0;
            }
        }

        public IGoalTask[] Tasks => goalTasks.ToArray();
    }
}
