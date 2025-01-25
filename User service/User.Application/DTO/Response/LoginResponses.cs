using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO.Response
{
    public class LoginResponses
    {
        public string Token { get; set; }
        public int EmployeeId { get; set; }
        public string Role { get; set; }
    }
}
