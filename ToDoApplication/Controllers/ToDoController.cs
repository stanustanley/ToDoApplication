using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoApplication;
using ToDoApplication.Models;

namespace ToDoApplication.Controllers
{
    public class ToDoController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();
        [HttpGet]
        [Route("Hello")]
        public string HelloWorld()
        {
            return "Hello Wrold";
        }
        [HttpGet]
        [Route("Get")]
        public List<Tasks> Get()
        {
            return db.Tasks.ToList();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public Tasks Get(int id)
        {
            return db.Tasks.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        [Route("Add")]
        public void Post([FromBody] string name)
        {
            if (!name.Equals(null))
            {
                Tasks t = new Tasks()
                {
                    Name = name,
                    Description = "task3 description",
                    Status = false
                };
                db.Tasks.Add(t);
                db.SaveChanges();
            }
        }
        [HttpPut]
        [Route("Change/{id}")]
        public void Put(int id, Tasks task)
        {
            if (!task.Equals(null) && id.Equals(task.Id))
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();

            }
        }

        [HttpDelete]
        [Route("Remove/{id}")]
        public IHttpActionResult Delete(int id)
        {
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(tasks);
            db.SaveChanges();

            return Ok(tasks);
        }
    }
}
