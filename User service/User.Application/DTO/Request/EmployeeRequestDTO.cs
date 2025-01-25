using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Common.DataAnnotation;
using User.Domain.Common.DataAnnotations;
using User.Domain.Entities;

namespace User.Application.DTO.Request
{

    public class RegisterEmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string MobileNo { get; set; }
        public AddAddressRequest Address { get; set; }
        //public int AddressId { get; set; }
        [EmailAddressCustomValidation]
        public string Email { get; set; }
        public string CitizenshipNo { get; set; }
        [DOBCustomValidation(18)]
        public string DOB { get; set; }
        public int DepartmentId { get; set; }
        public int Role { get; set; }
        public int Gender { get; set; }
        public string Nationality { get; set; }
        public string StartDate { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
    public class UpdateRegisterEmployeeRequest
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string MobileNo { get; set; }
        public AddAddressRequest Address { get; set; }
        //[EmailAddressCustomValidation]
        //public string Email { get; set; }
        public string CitizenshipNo { get; set; }
        [DOBCustomValidation(18)]
        public string DOB { get; set; }
        public int DepartmentId { get; set; }
        public int Role { get; set; }
        public int Gender { get; set; }
        public string Nationality { get; set; }
        public DateTime StartDate { get; set; }
      
    }
}
