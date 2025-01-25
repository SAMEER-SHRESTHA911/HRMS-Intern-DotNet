using AttendanceAndLeave.Application.DTO.Request;
using AttendanceAndLeave.Application.DTO.Response;
using AttendanceAndLeave.Application.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static AttendanceAndLeave.Application.Common.CommonUtilities;

namespace AttendanceAndLeave.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyManager _manager;

        public PolicyController(IPolicyManager manager)
        {
            _manager = manager;
        }
        [HttpGet("GetAllPolicy")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<PolicyResponseDto>>> GetAllPolicy()
        {
            var result = await _manager.GetAllPolicy();
            return result;
        }
        [HttpPost("CreatePolicy")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResult<int>> CreatePolicy(CreatePolicyRequestDto request)
        {
            var result = await _manager.CreatePolicy(request);
            return result;
        }
        [HttpPut("UpdatePolicy")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResult<bool>> UpdatePolicy(UpdatePolicyRequestDto request)
        {
            var result = await _manager.UpdatePolicy(request);
            return result;
        }
        [HttpDelete("DeletePolicy")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResult<bool>> DeletePolicy(int id)
        {
            var result = await _manager.DeletePolicy(id);
            return result;
        }



    }
}
