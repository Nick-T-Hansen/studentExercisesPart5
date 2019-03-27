using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentExercises5
{

    public class Cohort
    {
        /*
        //Constructor for Cohort object
        public Cohort(string cohortName) {
            CohortName = cohortName;
        }
        */

        //Cohort object properties
        public int Id { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 2)]
        public string CohortName { get; set; }

        //--------QUESTION------------should these be disctionaries because each instructor and student is their own object?
        public List<Instructor> InstructorList { get; set; }
        public List<Student> StudentList { get; set; }
    }
}