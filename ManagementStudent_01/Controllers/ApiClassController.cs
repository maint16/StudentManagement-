using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ManagementStudent_01.Models;

namespace ManagementStudent_01.Controllers
{
    [RoutePrefix("api/class")]
    public class ApiClassController : ApiController
    {
        public readonly StudentManagementEntities DbSet;
        public ApiClassController()
        {
            DbSet=new StudentManagementEntities();
        }
        [HttpPost]
        [Route("")]
        public  IHttpActionResult Create([FromBody] ClassModel model)
        {
            var objClass=new Class();
            objClass.Name = model.Name;
            DbSet.Classes.Add(objClass);
            DbSet.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetById([FromUri] int id)
        {
            var objClass = DbSet.Classes.Find(id);
           
            return Ok();
        }
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update([FromUri] int id,[FromBody] ClassModel model)
        {
            var objClass = DbSet.Classes.Find(id);
            if (objClass != null)
            {
                objClass.Name = model.Name;
                DbSet.SaveChanges();
            }
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var objClass = DbSet.Classes.Find(id);
            if (objClass != null)
            {
                DbSet.Classes.Remove(objClass);
                DbSet.SaveChanges();
            }
            return Ok();
        }

    }
}
