/*
 You must define a type for representing a student in code. A student can only be in one cohort at a time. A student can be working on many exercises at a time.
 */

using System;
using System.Collections.Generic;

namespace StudentExercises5

{
    public class Student
    {
        /*
        public Student (string firstN, string lastN, string slackHandle, int cohortId )
        {
            //constructor for Student object
            FirstName = firstN;
            LastName = lastN;
            Slack = slackHandle;
            CohortId = cohortId;
            ExerciseList = new List<Exercise>();
        }
        */
        //Student object properties
        public int Id { get; set; }
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Slack {get; set;}
        public  int CohortId {get; set;}

        public List<Exercise> ExerciseList {get; set;}


    }
}
