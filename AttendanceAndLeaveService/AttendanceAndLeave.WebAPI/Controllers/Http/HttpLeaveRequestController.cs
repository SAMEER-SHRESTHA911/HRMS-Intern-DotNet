using AttendanceAndLeave.Application.DTO.Response;
using AttendanceAndLeave.Application.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using static AttendanceAndLeave.Application.Common.CommonUtilities;

namespace AttendanceAndLeave.WebAPI.Controllers.Http
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpLeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestManager _leaveRequestManager;
        private readonly ILeaveBalanceManager _leaveBalanceManager;

        public HttpLeaveRequestController(ILeaveRequestManager leaveRequestManager, ILeaveBalanceManager leaveBalanceManager)
        {
            _leaveRequestManager = leaveRequestManager;
            _leaveBalanceManager = leaveBalanceManager;
        }
        [HttpGet("GetLeaveRequestByEmpId")]
        public async Task<ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>> GetLeaveRequestByEmpId(int id)
        {
            var leaveRequest = await _leaveRequestManager.GetLeaveRequestByEmpId(id);
            return leaveRequest;
        }
       
    }
}
