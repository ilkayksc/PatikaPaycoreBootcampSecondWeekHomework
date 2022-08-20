using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PaycoreBootcampHW2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {   // Staff class is created.
        public class Staff
        {   
            public int Id { get; set; }
            
            public string Name { get; set; }

            public string LastName { get; set; }

            public DateTime DateOfBirth { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }

            public decimal Salary { get; set; }

            public Staff() { }
        }

        // A static list is created.
        private List<Staff> GetList()
        {
            List<Staff> list = new() {
            new Staff { Id = 0, Name = "Kodluyoruz.org", LastName = "Kodlamıyoruz.org", DateOfBirth = new DateTime(2001, 01, 01), Email = "ilkayksc@gmail.com", PhoneNumber = "+905350304202",Salary = 3000 },
            new Staff { Id = 1, Name = "Kodluyoruz.org", LastName = "Kodlamıyoruz.org", DateOfBirth = new DateTime(1975, 01, 01), Email = "ilkayksc@gmail.com", PhoneNumber = "+905350304202",Salary = 2500 }};
            return list;
        }

        // GET: api/<StaffController>
        [HttpGet]
        public ActionResult<List<Staff>> Get()
        {
            return Ok(GetList());
        }

        // GET api/<StaffController>/5
        [HttpGet("{id}")]
        public ActionResult<Staff> GetStaffById(int id)
        {
            Staff staff = GetList().Where(x => x.Id == id).ToList().FirstOrDefault();
            if (staff == null)
            {
                return NotFound("Id is not valid.");
            }
            return Ok(staff);
        }

        // POST api/<StaffController>
        [HttpPost]
        public ActionResult<List<Staff>> Post([FromBody] Staff request)
        {
           StaffValidator validator = new StaffValidator();

            // validation check.
            try
            {
                validator.ValidateAndThrow(request);
                List<Staff> list = GetList();
                list.Add(request);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
           
        }

        // PUT api/<StaffController>/5
        [HttpPut("{id}")]
        public ActionResult<List<Staff>> Put(int id, [FromBody] Staff request)
        {
            List<Staff> list = GetList();
            Staff staff = list.Where(x => x.Id == id).ToList().FirstOrDefault();
            if (staff == null)
            {
                return NotFound("Id is not valid.");
            }
            list.Remove(staff);
            request.Id = id;
            // validation check.
            try
            {
                StaffValidator validator = new StaffValidator();
                validator.ValidateAndThrow(request);
                list.Add(request);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<StaffController>/5
        [HttpDelete("{id}")]
        public ActionResult<List<Staff>> Delete(int id)
        {
            List<Staff> list = GetList();
            Staff staff = list.Where(x => x.Id == id).ToList().FirstOrDefault();
            if (staff == null)
            {
                return NotFound("Id is not valid.");
            }
            list.Remove(staff);
            return Ok(list);
        }

        public class isValidDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                try
                {
                    DateTime date = Convert.ToDateTime(value);
                    DateTime max = new DateTime(2002, 10, 10);
                    DateTime min = new DateTime(1945, 11, 11);

                    var msg = string.Format($"Please enter a value between {min:MM/dd/yyyy} and {max:MM/dd/yyyy}");
                    if (date < min || date > max)
                        return new ValidationResult(msg);
                    else
                        return ValidationResult.Success;
                }
                catch (Exception)
                {
                    return new ValidationResult("Invalid Date of Birth");
                }
            }
        }

        
    }
}
