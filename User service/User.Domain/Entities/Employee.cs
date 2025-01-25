using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Common;
using User.Domain.Enum;

namespace User.Domain.Entities
{
    public class Employee:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string MobileNo { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public string CitizenshipNo { get; set; }
        public DateTime DOB { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public Role Roles { get; set; } 
        public Gender Gender { get; set; }

        public string Nationality { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
