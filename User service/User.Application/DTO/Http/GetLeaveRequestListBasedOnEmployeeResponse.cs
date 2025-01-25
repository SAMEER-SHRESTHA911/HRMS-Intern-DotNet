using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO.Http
{
    public class GetLeaveRequestListBasedOnEmployeeResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public string LeaveFrom { get; set; }
        public string LeaveTo { get; set; }
        public int LeaveType { get; set; }
        public int DayLeave { get; set; }
        public string ReasonForLeave { get; set; }
        public int LeaveRequestStatus { get; set; }
    }
}
