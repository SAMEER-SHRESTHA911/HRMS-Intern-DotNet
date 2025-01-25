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
    // [Authorize(Roles = "Employee, Admin")]

    public class EnumDataController : ControllerBase
    {
        private readonly IGetEnumDataManager _manager;

        public EnumDataController(IGetEnumDataManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAllDayLeave")]
        public async Task<ServiceResult<List<EnumDataResponseDto>>> GetAllDayLeave()
        {
            var response = await _manager.GetAllDayLeave();
            return response;
        }
        [HttpGet("GetAllLeaveRequestStatus")]
        public async Task<ServiceResult<List<EnumDataResponseDto>>> GetAllLeaveRequestStatus()
        {
            var response = await _manager.GetAllLeaveRequestStatus();
            return response;
        }
        [HttpGet("GetAllLeaveType")]
        public async Task<ServiceResult<List<EnumDataResponseDto>>> GetAllLeaveType()
        {
            var response = await _manager.GetAllLeaveType();
            return response;
        }
        [HttpGet("GetAllResultStatus")]
        public async Task<ServiceResult<List<EnumDataResponseDto>>> GetAllResultStatus()
        {
            var response = await _manager.GetAllResultStatus();
            return response;
        }
        [HttpGet("GetAllWorkLocation")]
        public async Task<ServiceResult<List<EnumDataResponseDto>>> GetAllWorkLocation()
        {
            var response = await _manager.GetAllWorkLocation();
            return response;
        }
    }
}
