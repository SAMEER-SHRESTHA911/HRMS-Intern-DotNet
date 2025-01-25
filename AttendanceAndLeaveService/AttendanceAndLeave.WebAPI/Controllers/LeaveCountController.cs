using AttendanceAndLeave.Application.DTO.Request;
using AttendanceAndLeave.Application.DTO.Response;
using AttendanceAndLeave.Application.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AttendanceAndLeave.Application.Common.CommonUtilities;

namespace AttendanceAndLeave.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveCountController : ControllerBase
    {
        private readonly ILeaveCountManager _leaveCountManager;
        public LeaveCountController(ILeaveCountManager leaveCountManager)
        {
                _leaveCountManager = leaveCountManager;
        }
        [HttpGet("GetLeaveCountList")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetLeaveCountListResponse>>> GetLeaveCountList()
        {
            var result = await _leaveCountManager.GetLeaveCountList();
            return result;
        }
        [HttpGet("GetLeaveCountDetailsById")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<GetLeaveCountDetailsByIdResponse>> GetLeaveCountDetailsById(int id)
        {
            var result = await _leaveCountManager.GetLeaveCountDetailsById(id);
            return result;
        }
        [HttpPost("UpdateleaveCount")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<bool>> UpdateleaveCount(UpdateLeaveCountRequest request)
        {
            var result = await _leaveCountManager.UpdateLeaveCount(request);
            return result;
        }
    }
}
