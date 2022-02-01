using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.TestData
{
    public class Test
    {
        public List<TestExerciseSummary> ExerciseSummaries { get; }
        public DateTime TestDate { get; }

        public Test(List<TestExerciseSummary> exerciseSummaries, DateTime testDate)
        {
            ExerciseSummaries = exerciseSummaries;
            TestDate = testDate;
        }
    }
}
