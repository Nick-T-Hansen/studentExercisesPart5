using System;
using System.Collections.Generic;

namespace StudentExercises5

{    public class Instructor
    {
        /*
        public Instructor(string firstN, string lastN, string slackHandle, int cohortId)
        {
            //constructor for Instructor object
            FirstName = firstN;
            LastName = lastN;
            Slack = slackHandle;
            CohortId = cohortId;
        }
        */

        //Instructor object properties
        public int Id { get; set; }
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Slack {get; set;}
        public int CohortId {get; set;}

        public Cohort Cohort { get; set; }
        public List<Exercise> ExerciseList { get; set; }

    }
}
