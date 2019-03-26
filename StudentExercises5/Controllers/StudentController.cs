using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

/*
Code for getting a list of students (y)
Code for getting a single student (y)
Code for creating a student (N- 500 error)
Code for editing a student
Code for deleting a student (N - not tested)
*/

namespace StudentExercises5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _config;

        public StudentController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: api/Student
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT s.id, s.FirstName, s.LastName, s.Slack, s.Cohort_id, c.Id, c.CohortName " +
                                        "FROM Student s " +
                                        "LEFT JOIN Cohort c " +
                                        "ON s.Cohort_id = c.Id " +
                                        "INNER JOIN StudentExercise x " +
                                        "ON s.Id = x.Student_id " +
                                        "LEFT JOIN Exercise e " +
                                        "ON x.Exercise_id = e.Id";


                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Student> students = new List<Student>();

                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Slack = reader.GetString(reader.GetOrdinal("Slack")),
                            //ExerciseList = reader.GetString(reader.GetOrdinal("ExerciseName")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("Cohort_id")),
                            Cohort = new Cohort
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                CohortName = reader.GetString(reader.GetOrdinal("CohortName"))
                            }

                        };

                        students.Add(student);
                    }
                    reader.Close();

                    return Ok(students);
                }
            }
        }

        // GET: api/Student/5
        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.id, s.FirstName, s.LastName, s.Slack, s.Cohort_id, c.Id AS cohortId, c.CohortName, e.ExerciseName
                                        FROM Student s
                                        LEFT JOIN Cohort c
                                        ON s.Cohort_id = c.Id
                                        INNER JOIN StudentExercise x
                                        ON s.Id = x.Student_id
                                        LEFT JOIN Exercise e
                                        ON x.Exercise_id = e.Id
                                        WHERE s.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Student student = null;

                    if (reader.Read())
                    {
                        student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Slack = reader.GetString(reader.GetOrdinal("Slack")),
                            //ExerciseList = reader.GetString(reader.GetOrdinal("ExerciseName")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("Cohort_id")),
                            Cohort = new Cohort
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("cohortId")),
                                CohortName = reader.GetString(reader.GetOrdinal("CohortName"))
                            }
                        };
                    }
                    reader.Close();
                    return Ok(student);
                }
            }
        }

        // POST: api/Student
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Student (FirstName, LastName, Slack, CohortId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@FirstName, @LastName, @Slack, @CohortId)";
                    cmd.Parameters.Add(new SqlParameter("@FirstName", student.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", student.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Slack", student.Slack));
                    cmd.Parameters.Add(new SqlParameter("@CohortId", student.CohortId));

                    int newId = (int)cmd.ExecuteScalar();
                    student.Id = newId;
                    return CreatedAtRoute("GetStudent", new { id = newId }, student);
                }
            }
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM Student WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return new StatusCodeResult(StatusCodes.Status204NoContent);
                        }
                        throw new Exception("No rows affected");
                    }
                }
            }
            catch (Exception)
            {
                if (!ObjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ObjectExists(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" SELECT Id, FirstName, LastName, Slack, CohortId
                                         FROM Student
                                         WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.Read();
                }
            }
        }
    }
}

