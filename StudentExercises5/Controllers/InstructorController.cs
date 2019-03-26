using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/*
Code for getting a list of instructors
Code for getting a single instructor
Code for creating an instructor
Code for editing an instructor
Code for deleting an instructor
*/

namespace StudentExercises5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        // GET: api/Instructor
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Instructor/5
        [HttpGet("{id}", Name = "GetInstructor")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Instructor
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Instructor/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
