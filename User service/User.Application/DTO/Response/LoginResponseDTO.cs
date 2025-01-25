using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO.Response
{
   public record LoginResponse(bool Flag, string Message=null,string Token = null,int EmployeeId=0);
}
