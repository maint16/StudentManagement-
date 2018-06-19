using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ManagementStudent_01.Atrributes;
using ManagementStudent_01.ViewModels;

namespace ManagementStudent_01.Controllers
{
    [RoutePrefix("api/student")]
    public class ApiStudentController : ApiBaseController
    {
        public readonly StudentManagementEntities DbSet;

        public ApiStudentController()
        {
            DbSet = new StudentManagementEntities();
        }


        [HttpPost]
        [Route("")]
        [ApiRole(new[] { "User" })]
        public IHttpActionResult Create([FromBody] AddStudentViewModel model)
        {
            //if (!CheckRole("Admin"))
            //    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, ""));
            if (model == null)
            {
                model = new AddStudentViewModel();
                Validate(model);
            }
            //Validate
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var objectClass = DbSet.Classes.Find(model.ClassId);
            if (objectClass == null)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "CLASS_IS_NOT_FOUND"));
            //Check duplicate student
            var checkDuplicate = DbSet.Students.Any(c => c.Code == model.Code);
            if (checkDuplicate)
                return Conflict();
            var student = new Student();
            student.Code = model.Code;
            student.Name = model.Name;
            student.ClassId = model.ClassId;
            DbSet.Students.Add(student);
            DbSet.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Insert students list include multiple attachment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert-list")]

        public IHttpActionResult CreateStudents(CreateListStudentViewModel model)
        {
            if (model == null)
            {
                model = new CreateListStudentViewModel();
                Validate(model);
            }

            //Validate
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transaction = DbSet.Database.BeginTransaction();
            try
            {

                //Insert students
                if (model.Students != null)
                {
                    foreach (var student in model.Students)
                    {
                        //if (student.Name == "A")
                        //    throw new Exception(" Name exists");
                        var attachment = student.Attachment;
                        var studentEnt = new Student();
                        studentEnt.Name = student.Name;
                        studentEnt.ClassId = student.ClassId;
                        studentEnt.Code = student.Code;

                        if (attachment != null)
                        {
                            //Insert attachment
                            var attachEnt = new Attachment();
                            attachEnt.FileName = attachment.Name;
                            if (student.Name != "A")
                                attachEnt.Extension = "jpg";
                            attachEnt.File = attachment.Buffer;
                            attachEnt.Mime = attachment.MediaType;

                            attachEnt = DbSet.Attachments.Add(attachEnt);
                            DbSet.SaveChanges();
                            studentEnt.AttachmentId = attachEnt.Id;
                        }

                        //Insert student
                        DbSet.Students.Add(studentEnt);

                    }
                }
                DbSet.SaveChanges();
                transaction.Commit();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
            }

            return Ok();
        }

        [HttpPost]
        [Route("student-insert-multiple")]
        public IHttpActionResult InsertStudents( [FromBody] CreateListStudentViewModel model)
        {
            if (model == null)
            {
                model = new CreateListStudentViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest();
          
            var transaction = DbSet.Database.BeginTransaction();
            try
            {
                //Insert studens
                foreach (var student in model.Students)
                {
                    //if(student.Name=="M")
                    //    throw new Exception("Name can't M");
                    var studentEnt = new Student();
                    studentEnt.Name = student.Name;
                    studentEnt.ClassId = student.ClassId;
                    studentEnt.Code = student.Code;

                    var m = new Attachment();
                    var attachModel = student.Attachment;
                    if (attachModel != null)
                    {
                        //Insert attachment
                        var attachEnt = new Attachment();
                        attachEnt.FileName = attachModel.Name;
                        attachEnt.Mime = attachModel.MediaType;
                        attachEnt.Extension = Path.GetExtension(attachModel.Name);
                        attachEnt.File = attachModel.Buffer;
                        m = DbSet.Attachments.Add(attachEnt);
                        DbSet.SaveChanges();
                        studentEnt.AttachmentId = m.Id;
                    }
                    DbSet.Students.Add(studentEnt);
                }
                DbSet.SaveChanges();
                //Finish transaction
                transaction.Commit();
            }
            catch (Exception e)
            {
                //Roll back database  if any error in try
                transaction.Rollback();
                return Conflict();
            }
            return Ok();

        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update([FromUri] int id, [FromBody] UpdateStudentModel model)
        {
            //Init model if model null
            if (model == null)
            {
                model = new UpdateStudentModel();
                Validate(model);
            }
            //Validate
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = DbSet.Students.Find(id);
            if (student != null)
            {
                student.Name = model.Name;
                student.Code = model.Code;
                student.ClassId = model.ClassId;
                DbSet.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetById([FromUri] int id)
        {

            var student = DbSet.Students.Find(id);
            if (student == null)
                return NotFound();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri] int id)
        {

            var student = DbSet.Students.Find(id);
            if (student != null)
            {
                DbSet.Students.Remove(student);
                DbSet.SaveChanges();
                return Ok();
            }
            return NotFound();

        }

    }
}
