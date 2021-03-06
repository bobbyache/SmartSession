﻿using System;

namespace CygSoft.SmartSession.Domain.Sessions
{
    public abstract class SessionResult
    {
        private DateTime startTime;
        private DateTime endTime;

        public int Minutes { get { return (int)Math.Round(endTime.Subtract(startTime).TotalMinutes, 0); } }
        public DateTime StartTime { get { return this.startTime; } }

        public SessionResult(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
                throw new ArgumentOutOfRangeException("Session minutes cannot be a negative value.");

            this.startTime = startTime;
            this.endTime = endTime;
        }
    }
}
