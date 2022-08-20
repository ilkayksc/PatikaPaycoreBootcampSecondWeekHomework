using FluentValidation;
using System;
using static PaycoreBootcampHW2.Controllers.StaffController;

namespace PaycoreBootcampHW2.Controllers
{
    public class StaffValidator : AbstractValidator<Staff>
    {
        public StaffValidator()
        {
            RuleFor(staff => staff.Name.Length + staff.LastName.Length).GreaterThanOrEqualTo(20).WithMessage("Name and surname length must be between 20-120").LessThanOrEqualTo(120).WithMessage("Name and surname length must be between 20-120");
            RuleFor(staff => staff.DateOfBirth).GreaterThanOrEqualTo(new DateTime(1945, 11, 11)).WithMessage("Date of Birth must be between 11-11-1945 and 10-10-2002").LessThanOrEqualTo(new DateTime(2002,10,10)).WithMessage("Date of Birth must be between 11-11-1945 and 10-10-2002");
            RuleFor(staff => staff.Salary).LessThanOrEqualTo(9000).GreaterThanOrEqualTo(2000).WithMessage("Salary must be between 2000-9000");
            string EmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            string PhoneRegex = @"^\+[1-9]{1}[0-9]{3,14}$";
            RuleFor(staff => staff.Email).Matches(EmailRegex).WithMessage("E-mail address is not valid.");
            RuleFor(staff => staff.PhoneNumber).Matches(PhoneRegex).WithMessage("Phone number is not valid.(eg. +90555 555 5555)"); 
        }
    }
}