using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.TestData
{
    public struct TestExerciseSummary
    {
        public string ExerciseId { get; }
        public int ExerciseTimeInSeconds { get; }
        public int Reps { get; }
        public BottleVolume BottleUsed { get; }
        public MovementCapacity MovementCapacity { get; }

        public TestExerciseSummary(string exerciseId, int exerciseTimeInSeconds, int reps, BottleVolume bottleUsed,
            MovementCapacity movementCapacity)
        {
            ExerciseId = exerciseId;
            ExerciseTimeInSeconds = exerciseTimeInSeconds;
            Reps = reps;
            BottleUsed = bottleUsed;
            MovementCapacity = movementCapacity;
        }
    }
}
