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
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestManager _leaveRequestManager;

        public LeaveRequestController(ILeaveRequestManager leaveRequestManager)
        {
            _leaveRequestManager = leaveRequestManager;
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet("GetLeaveRequestList")]
        public async Task<ServiceResult<List<GetLeaveRequestListResponse>>> GetLeaveRequestList()
        {
            var leaveRequest = await _leaveRequestManager.GetLeaveRequestList();
            return leaveRequest;
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet("GetLeaveRequestDetailById")]
        public async Task<ServiceResult<GetLeaveRequestDetailByIdResponse>> GetLeaveRequestDetailById(int id)
        {
            var leaveRequest = await _leaveRequestManager.GetLeaveRequestDetailById(id);
            return leaveRequest;
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpPost("AddLeaveRequest")]
        public async Task<ServiceResult<bool>> AddLeaveRequest(AddLeaveRequestRequest reqEmp)
        {
            var leaveRequest = await _leaveRequestManager.AddLeaveRequest(reqEmp);
            return leaveRequest;
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpPut("UpdateLeaveRequest")]
        public async Task<ServiceResult<bool>> UpdateLeaveRequest(UpdateLeaveRequestRequest reqEmp)
        {
            var leaveRequest = await _leaveRequestManager.UpdateLeaveRequest(reqEmp);
            return leaveRequest;
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpDelete("DeleteLeaveRequest")]
        public async Task<ServiceResult<bool>> DeleteLeaveRequest(int id)
        {
            var leaveRequest = await _leaveRequestManager.DeleteLeaveRequest(id);
            return leaveRequest;
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet("GetLeaveRequestByEmpId")]
        public async Task<ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>> GetLeaveRequestByEmpId(int id)
        {
            var leaveRequest = await _leaveRequestManager.GetLeaveRequestByEmpId(id);
            return leaveRequest;
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("ApproveRejectLeaveRequest")]
        public async Task<ServiceResult<bool>> ApproveRejectLeaveRequest(int id,int status)
        {
            var leaveRequest = await _leaveRequestManager.ApproveRejectLeaveRequest(id,status);
            return leaveRequest;
        }
        //[HttpGet("DisplayLeaveRequestBasedOnStatus")]
        //public async Task<ServiceResult<List<DisplayLeaveRequestBasedOnStatusResponse>>> DisplayLeaveRequestBasedOnStatus(int id)
        //{
        //    var leaveRequest = await _leaveRequestManager.DisplayLeaveRequestBasedOnStatus(id);
        //    return leaveRequest;
        //}
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet("GetLeaveRequestAccordingToDateAndStatus")]
        public async Task<ServiceResult<List<GetLeaveRequestAccordingToDateResponse>>> GetLeaveRequestAccordingToDateAndStatus(string date, int id)
        {
            var leaveRequest = await _leaveRequestManager.GetLeaveRequestAccordingToDateAndStatus(date, id);
            return leaveRequest;
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet("GetLeaveRequestBasedOnEmpIdAndStatus")]
        public async Task<ServiceResult<List<GetLeaveRequestBasedOnEmpIdAndStatusResponse>>> GetLeaveRequestBasedOnEmpIdAndStatus(int empId, int statusId)
        {
            var leaveRequest = await _leaveRequestManager.GetLeaveRequestBasedOnEmpIdAndStatus(empId, statusId);
            return leaveRequest;
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpPost("GetEmployeeLeaveRequestList")]
        public async Task<ServiceResult<ListOfEmployeeLeaveRequestResponse>> GetEmployeeLeaveRequestList(EmployeeLeaveRequestListRequest filter)
        {
            return await _leaveRequestManager.GetListOfEmployeeLeaveRequest(filter);
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet("GetEmployeeLeaveRequestListWithoutFilter")]
        public async Task<ServiceResult<List<ListOfEmployeeLeaveRequestWithoutFilterResponse>>> GetEmployeeLeaveRequestListWithoutFilter()
        {
            return await _leaveRequestManager.GetListOfEmployeeLeaveRequestWithoutFilter();
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet("TotalLeaveToday")]
        public async Task<int> TotalLeaveToday()
        {
            //var result = (await GetLeaveRequestAccordingToDateAndStatus(DateTime.Today.ToString("yyyy-MM-dd"), 2));
            var result = (await GetLeaveRequestAccordingToDateAndStatus(DateTime.Today.ToString("yyyy-MM-dd"), 2));
            return result.Data.Count();
        }
    }
}
