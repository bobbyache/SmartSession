﻿using System;

namespace CygSoft.SmartSession.Domain.Sessions
{
    public interface IExerciseRecorder
    {
        DateTime? EndTime { get; }
        bool Recording { get; }
        double Seconds { get; }
        DateTime? StartTime { get; }
        Action TickActionCallBack { set; }

        event EventHandler RecordingStatusChanged;

        void Reset();
        void Pause();
        void Resume();

        void AddSeconds(int seconds);
        void SubstractSeconds(int seconds);
    }
}