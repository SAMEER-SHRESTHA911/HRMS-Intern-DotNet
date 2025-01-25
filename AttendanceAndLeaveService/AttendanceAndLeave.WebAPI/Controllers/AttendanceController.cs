using AttendanceAndLeave.Application.DTO.Request;
using AttendanceAndLeave.Application.DTO.Response;
using AttendanceAndLeave.Application.Http.Interface;
using AttendanceAndLeave.Application.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AttendanceAndLeave.Application.Common.CommonUtilities;

namespace AttendanceAndLeave.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceManager _attendanceManager;
        public AttendanceController(IAttendanceManager attendanceManager)
        {
            _attendanceManager = attendanceManager;
        }

        //[HttpGet("GetAllAttendance")]
        //public async Task<ServiceResult<List<AttendanceResponseDto>>> GetAllAttendance()
        //{
        //    var result = await _attendanceManager.GetAllAttendance();
        //    return result;
        //}
        //[HttpPost("GetAllAttendanceByQuery")]
        //public async Task<ServiceResult<List<AttendanceResponseDto>>> GetAllAttendanceByQuery(AttendanceRequestFilter query)
        //{
        //    var result = await _attendanceManager.GetAllAttendanceByQuery(query);
        //    return result;
        //}

        [HttpPost("ListAllAttendance")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResult<DataListResponseDto<List<AttendanceListResponseDto>>>> ListAllAttendance(AttendanceListRequestFilterDto request)
        {
            var result = await _attendanceManager.ListAllAttendance(request);
            return result;
        }

        [HttpPost("GetAllAttendanceByEmployeeId")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<DataListResponseDto<List<AttendanceResponseDto>>>> GetAllAttendanceByEmployeeId(EmployeeAttendanceRequestFilterDto request)
        {
            var result = await _attendanceManager.GetAllAttendanceByEmployeeId(request);
            return result;
        }

        [HttpGet("GetAttendanceById")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResult<AttendanceResponseDto>> GetAttendanceById(int id)
        {
            var result = await _attendanceManager.GetAttendanceById(id);
            return result;
        }

        [HttpPost("CheckIn")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<string>> CheckIn(CheckInAttendanceRequestDto request)
        {
            var result = await _attendanceManager.CheckIn(request);
            return result;
        }

        [HttpPost("CheckOut")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<string>> CheckOut(CheckOutAttendanceRequestDto request)
        {
            var result = await _attendanceManager.CheckOut(request);
            return result;
        }

        [HttpPost("CreateAttendance")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResult<int>> CreateAttendance(CreateAttendanceRequestDto request)
        {
            var result = await _attendanceManager.CreateAttendance(request);
            return result;
        }

        [HttpPut("UpdateAttendance")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResult<bool>> UpdateAttendance(UpdateAttendanceRequestDto request)
        {
            var result = await _attendanceManager.UpdateAttendance(request);
            return result;
        }

        [HttpDelete("DeleteAttendance")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResult<bool>> DeleteAttendance(int id)
        {
            var result = await _attendanceManager.DeleteAttendance(id);
            return result;
        }

        [HttpGet("GetCurrentDayAttendanceSummary")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ServiceResult<CurrentDayAttendanceSummaryDTO>> GetCurrentDayAttendanceSummary()
        {
            var result = await _attendanceManager.GetCurrentDayAttendanceSummary();
            return result;
        }
        [HttpGet("CheckCheckInStatus")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ServiceResult<bool>> CheckCheckInStatus()
        {
            var result = await _attendanceManager.CheckCheckInStatus();
            return result;
        }
        //[HttpDelete("PermanentDeleteAttendance")]
        //public async Task<ServiceResult<bool>> PermanentDeleteAttendance(int id)
        //{
        //    var result = await _attendanceManager.PermanentDeleteAttendance(id);
        //    return result;
        //}



        //[HttpGet("TESTDisplayEmployees")]
        //public async Task<ServiceResult<List<EmployeeBriefDetailDto>>> DisplayEmployees()
        //{
        //    var data = await _httpUserService.GetAllEmployeeBriefDetails();
        //    return data;
        //}



        // public class CustomValidationAttribute : ActionFilterAttribute
        // {
        //     public override void OnActionExecuting(ActionExecutingContext context)
        //     {
        //         if (!context.ModelState.IsValid)
        //         {
        //             var errors = context.ModelState
        //                 .Where(e => e.Value.Errors.Count > 0)
        //                 .Select(e => new
        //                 {
        //                     Field = e.Key,
        //                     Errors = e.Value.Errors.Select(err => err.ErrorMessage).ToArray()
        //                 }).ToArray();

        //             var customResponse = new
        //             {
        //                 Message = "Validation failed.",
        //                 Errors = errors
        //             };

        //             context.Result = new BadRequestObjectResult(customResponse);
        //         }
        //     }
        // }

        // public class ValidateModelAttribute : ActionFilterAttribute
        // {
        //     public override void OnActionExecuting(ActionExecutingContext context)
        //     {
        //         if (!context.ModelState.IsValid)
        //         {
        //             context.Result = new BadRequestObjectResult(context.ModelState);
        //         }
        //     }
        // }
    }
}
