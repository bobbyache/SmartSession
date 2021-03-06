﻿using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Recording;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.UnitTests.Infrastructure
{
    public class EntityFactory
    {
        public static PracticeRoutine GetBasicPracticeRoutine()
        {
            List<TimeSlotExercise> timeSlotExercises = new List<TimeSlotExercise>
            {
                new TimeSlotExercise(1, 1, "Exercise 1", 3),
                new TimeSlotExercise(2, 1, "Exercise 2", 3),
                new TimeSlotExercise(3, 1, "Exercise 3", 3)
            };

            List<PracticeRoutineTimeSlot> timeSlots = new List<PracticeRoutineTimeSlot>
            {
                new PracticeRoutineTimeSlot(1, "Time Slot 1", 300, timeSlotExercises)
            };

            return new PracticeRoutine(1, "Practice Routine", timeSlots);
        }

        public static TimeSlotExercise GetTimeSlotExercise(int id = 1, string title = "Existing Exercise",
            int frequencyRating = 50)
        {
            var exercise = new TimeSlotExercise(id, title, frequencyRating);
            return exercise;
        }

        public static List<TimeSlotExercise> CreateSingleTimeSlotExerciseList()
        {
            return new List<TimeSlotExercise> { GetTimeSlotExercise() };
        }

        public static List<PracticeRoutineTimeSlot> CreateSingleTimeSlotList()
        {
            List<TimeSlotExercise> exercises = new List<TimeSlotExercise> { GetTimeSlotExercise() };
            var timeSlots = new List<PracticeRoutineTimeSlot> { new PracticeRoutineTimeSlot("New TimeSlot Title", 120, exercises) };

            return timeSlots;
        }

        public static List<Exercise> CreateSingleExerciseList()
        {
            return new List<Exercise> { GetExercise() };
        }

        public static PracticeRoutineTimeSlot CreateSingleTimeSlot(string title = "New TimeSlot Title")
        {
            List<TimeSlotExercise> exercises = new List<TimeSlotExercise> { GetTimeSlotExercise() };
            return new PracticeRoutineTimeSlot(title, 120, exercises);
        }

        public static PracticeRoutineTimeSlot GetSingleTimeSlot(string title = "Existing TimeSlot Title")
        {
            List<TimeSlotExercise> exercises = new List<TimeSlotExercise> { GetTimeSlotExercise() };
            return new PracticeRoutineTimeSlot(1, title, 120, exercises);
        }

        public static PracticeRoutine CreateEmptyPracticeRoutine(string title = "New Empty Practice Routine")
        {
            return new PracticeRoutine(title, new List<PracticeRoutineTimeSlot>());
        }

        public static PracticeRoutine GetEmptyPracticeRoutine(int id = 1, string title = "Existing Empty Practice Routine")
        {
            var practiceRoutine = new PracticeRoutine(id, title, new List<PracticeRoutineTimeSlot>());
            practiceRoutine.DateCreated = DateTime.Parse("2018/07/03");
            practiceRoutine.DateModified = null;

            return practiceRoutine;
        }

        public static Exercise GetExerciseWithNoActivity(int id, string title,
            int? targetSpeed = null,int? speedProgressWeighting = null,
            int? targetpracticeTime = null, int? practiceTimeProgressWeighting = null,
            int? manualProgressWeighting = null,
            int? frequencyWeighting = null, int? difficultyRating = null,
            List<ExerciseActivity> exerciseActivity = null,
            string dateCreated = null, string dateModified = null)
        {
            var exercise = new Exercise
            {
                Id = 1,
                Title = title,
                DateCreated = !string.IsNullOrEmpty(dateCreated) ? DateTime.Parse(dateCreated) : DateTime.Parse("2018-03-01 12:15:00"),
                DateModified = !string.IsNullOrEmpty(dateModified) ? DateTime.Parse(dateModified) : DateTime.Parse("2018-03-01 12:15:00"),
                SpeedProgressWeighting = speedProgressWeighting ?? 0,
                TargetMetronomeSpeed = targetSpeed ?? 0,
                PracticeTimeProgressWeighting = practiceTimeProgressWeighting ?? 0,
                ManualProgressWeighting = manualProgressWeighting ?? 0,
                TargetPracticeTime = targetpracticeTime,
                ExerciseActivity = exerciseActivity ?? new List<ExerciseActivity>()
            };
            return exercise;
        }

        public static Exercise GetExercise(int id = 1, string title = "Existing Exercise",
            int? targetSpeed = null, int? speedProgressWeighting = null,
            int? targetpracticeTime = null, int? practiceTimeProgressWeighting = null,
            int? manualProgressWeighting = null,
            int? frequencyWeighting = null, int? difficultyRating = null,
            List<ExerciseActivity> exerciseActivity = null,
            string dateCreated = null, string dateModified = null)
        {
            var exercise = new Exercise
            {
                Title = title,
                SpeedProgressWeighting = speedProgressWeighting ?? 0,
                TargetMetronomeSpeed = targetSpeed ?? 0,
                PracticeTimeProgressWeighting = practiceTimeProgressWeighting ?? 0,
                ManualProgressWeighting = manualProgressWeighting ?? 0,
                TargetPracticeTime = targetpracticeTime,
                ExerciseActivity = exerciseActivity ?? new List<ExerciseActivity>()
            };
            return exercise;
        }

        public static Exercise CreateExercise(string title,
            int? targetSpeed = null, int? speedProgressWeighting = null,
            int? targetpracticeTime = null, int? practiceTimeProgressWeighting = null,
            int? manualProgressWeighting = null,
            int? frequencyWeighting = null, int? difficultyRating = null,
            List<ExerciseActivity> exerciseActivity = null,
            string dateCreated = null, string dateModified = null)
        {
            var exercise = new Exercise
            {
                Title = title,
                SpeedProgressWeighting = speedProgressWeighting ?? 0,
                TargetMetronomeSpeed = targetSpeed ?? 0,
                PracticeTimeProgressWeighting = practiceTimeProgressWeighting ?? 0,
                ManualProgressWeighting = manualProgressWeighting ?? 0,
                TargetPracticeTime = targetpracticeTime,
                ExerciseActivity = exerciseActivity ?? new List<ExerciseActivity>()
            };
            return exercise;
        }

        public static ExerciseActivity CreateExerciseActivity(int? exerciseId = null, int? speed = null, int? manualProgress = null, int? seconds = null, string dateCreated = null)
        {
            var exerciseActivity = new ExerciseActivity
            {
                Id = 0, // we're creating an exercise, not gettings it.
                MetronomeSpeed = speed ?? 80,
                ManualProgress = manualProgress ?? 0,
                Seconds = seconds ?? 3000,
                ExerciseId = exerciseId ?? 0,
                DateCreated = !string.IsNullOrEmpty(dateCreated) ? DateTime.Parse(dateCreated) : DateTime.Now,
                DateModified = null
            };
            return exerciseActivity;
        }

        public static ExerciseActivity GetExerciseActivity(int id, int exerciseId, int? speed = null, int? manualProgress = null, int? seconds = null, string dateCreated = null)
        {
            var exerciseActivity = new ExerciseActivity
            {
                Id = id, // we're getting an existing exercise.
                MetronomeSpeed = speed ?? 80,
                ManualProgress = manualProgress ?? 0,
                Seconds = seconds ?? 3000,
                ExerciseId = exerciseId,
                DateCreated = !string.IsNullOrEmpty(dateCreated) ? DateTime.Parse(dateCreated) : DateTime.Now,
                DateModified = null
            };
            return exerciseActivity;
        }

        public static TimeSlotExerciseRecorder CreateTimeSlotExerciseRecorder(int assignedTime)
        {
            var recorder = new Recorder();
            var manualProgress = new ManualProgress(0, 50);
            var speedProgress = new SpeedProgress(0, 0, 120, 50);
            var practiceTimeProgress = new PracticeTimeProgress(0, 600, 50);
            return new TimeSlotExerciseRecorder(recorder, 1, "Exercise Title", speedProgress, practiceTimeProgress, manualProgress, assignedTime);
        }

        public static ExerciseRecorder CreateSpeedProgressExerciseRecorder(int initialSpeed, int currentSpeed, int targetSpeed)
        {
            var recorder = new Recorder();
            var speedProgress = new SpeedProgress(initialSpeed, currentSpeed, targetSpeed, 100);
            var practiceTimeProgress = new PracticeTimeProgress(0, 0, 0);
            var manualProgress = new ManualProgress(0, 0);

            return new ExerciseRecorder(recorder, 1, "Speed Exercise", speedProgress, practiceTimeProgress, manualProgress);
        }

        public static ExerciseRecorder CreateTimeProgressExerciseRecorder(int currentTime, int targetTime)
        {
            var recorder = new Recorder();
            var speedProgress = new SpeedProgress(0, 0, 0, 0);
            var practiceTimeProgress = new PracticeTimeProgress(currentTime, targetTime, 100);
            var manualProgress = new ManualProgress(0, 0);

            return new ExerciseRecorder(recorder, 1, "Time Exercise", speedProgress, practiceTimeProgress, manualProgress);
        }

        public static ExerciseRecorder CreateManualProgressExerciseRecorder(int value)
        {
            var recorder = new Recorder();
            var speedProgress = new SpeedProgress(0, 0, 0, 0);
            var practiceTimeProgress = new PracticeTimeProgress(0, 0, 0);
            var manualProgress = new ManualProgress(value, 100);

            return new ExerciseRecorder(recorder, 1, "Time Exercise", speedProgress, practiceTimeProgress, manualProgress);
        }


        public static TimeSlotExerciseRecorder CreateSpeedProgressTimeSlotExerciseRecorder(int initialSpeed, int currentSpeed, int targetSpeed, int assignedTime)
        {
            var recorder = new Recorder();
            var speedProgress = new SpeedProgress(initialSpeed, currentSpeed, targetSpeed, 100);
            var practiceTimeProgress = new PracticeTimeProgress(0, 0, 0);
            var manualProgress = new ManualProgress(0, 0);

            return new TimeSlotExerciseRecorder(recorder, 1, "Speed Exercise", speedProgress, practiceTimeProgress, manualProgress, assignedTime);
        }

        public static TimeSlotExerciseRecorder CreateTimeProgressTimeSlotExerciseRecorder(int currentTime, int targetTime, int assignedTime)
        {
            var recorder = new Recorder();
            var speedProgress = new SpeedProgress(0, 0, 0, 0);
            var practiceTimeProgress = new PracticeTimeProgress(currentTime, targetTime, 100);
            var manualProgress = new ManualProgress(0, 0);

            return new TimeSlotExerciseRecorder(recorder, 1, "Time Exercise", speedProgress, practiceTimeProgress, manualProgress, assignedTime);
        }

        public static TimeSlotExerciseRecorder CreateManualProgressTimeSlotExerciseRecorder(int value, int assignedTime)
        {
            var recorder = new Recorder();
            var speedProgress = new SpeedProgress(0, 0, 0, 0);
            var practiceTimeProgress = new PracticeTimeProgress(0, 0, 0);
            var manualProgress = new ManualProgress(value, 100);

            return new TimeSlotExerciseRecorder(recorder, 1, "Time Exercise", speedProgress, practiceTimeProgress, manualProgress, assignedTime);
        }
    }
}
