using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Helpers;
using User.Domain.Entities;

namespace User.Infrastructure.Http.Url
{
    public class HttpAttendanceLeaveServiceUrl
    {
        public static string LeaveBaseUrl => AppServiceHelper.Configuration["Url:LeaveBaseUrl"];
        //public static string body = JsonConvert.SerializeObject(new { EmpId = 123 });
        public static string AddLeaveBalanceOfEmp(int empId)
        {
            return "/api/HttpLeaveBalance/AddLevelBalanceOfEmp?id=" + empId;
        }
        public static string GetLeaveRequestByEmpId(int employeeId)
        {
            return "/api/HttpLeaveRequest/GetLeaveRequestByEmpId?id=" + employeeId;
        }
        public static string GetLeaveBalanceofEmp(int employeeId)
        {
            return "/api/HttpLeaveBalance/GetLeaveBalanceofEmp?empId=" + employeeId;
        }
    }
}
