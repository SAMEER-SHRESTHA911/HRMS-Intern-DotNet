using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO.Http
{
    public class GetLeaveBalanceOfEmpResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public double RemainingCount { get; set; }
        public double TotalCount { get; set; }
        public int LeaveTypeEnum { get; set; }

    }
}
