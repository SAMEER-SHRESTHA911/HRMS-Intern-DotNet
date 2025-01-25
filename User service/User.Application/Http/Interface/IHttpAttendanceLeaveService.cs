using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTO.Http;
using static User.Application.Common.CommonUtilities;

namespace User.Application.Http.Interface
{
    public interface IHttpAttendanceLeaveService
    {
        Task<ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>> GetLeaveRequestByEmpId(int empId);
        Task<ServiceResult<List<GetLeaveBalanceOfEmpResponse>>> GetLeaveBalanceofEmp(int empId);
        Task<ServiceResult<bool>> AddLeaveBalanceOfEmp(int empId);
    }
}
