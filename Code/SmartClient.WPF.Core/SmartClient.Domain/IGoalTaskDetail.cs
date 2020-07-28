﻿using SmartClient.Domain.Common;

namespace SmartClient.Domain
{
    public interface IGoalTaskDetail
    {
        string Id { get; }

        string Title { get; }
        string GoalTitle { get; }

        public double Weighting { get; }

        public double Start { get; }
        public double Target { get; }

        public TaskUnitOfMeasure UnitOfMeasure { get; }

        public int PercentProgress { get; }

        IGoalTaskProgressSnapshot[] TaskProgressSnapshots { get; }
    }
}