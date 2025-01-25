using AttendanceAndLeave.Application.DTO.Response;
using AttendanceAndLeave.Application.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using static AttendanceAndLeave.Application.Common.CommonUtilities;

namespace AttendanceAndLeave.WebAPI.Controllers.Http
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpLeaveBalanceController : ControllerBase
    {
        private readonly ILeaveBalanceManager _leaveBalanceManager;

        public HttpLeaveBalanceController(ILeaveBalanceManager leaveBalanceManager)
        {
                _leaveBalanceManager = leaveBalanceManager;
        }
        [HttpPost("AddLevelBalanceOfEmp")]
        public async Task<ServiceResult<bool>> AddLevelBalanceOfEmp([FromBody] Domain.Entities.ELeaveBalance request)
        {
            var result = await _leaveBalanceManager.AddLeaveBalanceOfEmp(request.EmployeeId);
            return result;
        }
        [HttpGet("GetLeaveBalanceofEmp")]
        public async Task<ServiceResult<List<GetLeaveBalanceOfEmpResponse>>> GetLeaveBalanceofEmp(int empId)
        {
            var result = await _leaveBalanceManager.GetLeaveBalanceofEmp(empId);
            return result;
        }

    }
}
