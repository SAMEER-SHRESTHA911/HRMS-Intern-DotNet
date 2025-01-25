using AttendanceAndLeave.Application.DTO.Response;
using AttendanceAndLeave.Application.Manager.Interface;
using AttendanceAndLeave.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AttendanceAndLeave.Application.Common.CommonUtilities;

namespace AttendanceAndLeave.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveBalanceController : ControllerBase
    {
        private readonly ILeaveBalanceManager _leaveBalanceManager;

        public LeaveBalanceController(ILeaveBalanceManager leaveBalanceManager)
        {
            _leaveBalanceManager = leaveBalanceManager;
        }
        [HttpGet("GetLeaveBalanceList")]
        //[Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetLeaveBalanceListResponse>>> GetLeaveBalanceList()
        {
            var result = await _leaveBalanceManager.GetLeaveBalanceList();
            return result;
        }


        //[HttpPost("AddLevelBalanceByLeaveType")]
        //public async Task<ServiceResult<bool>> AddLevelBalanceByLeaveType(int empId, int leavetype)
        //{
        //    var result = await _leaveBalanceManager.AddLeaveBalanceByLeaveType(empId, leavetype);
        //    return result;
        //}
        [HttpPost("AddLevelBalanceOfEmp")]
        //[Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<bool>> AddLevelBalanceOfEmp(int empId)
        {
            var result = await _leaveBalanceManager.AddLeaveBalanceOfEmp(empId);
            return result;
        }

        [HttpPut("UpdateLeaveBalance")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<bool>> UpdateLeaveBalance(int id, int statusId)
        {
            var result = await _leaveBalanceManager.UpdateLeaveBalance(id, statusId);
            return result;
        }
        [HttpGet("GetLeaveBalanceofEmp")]
        //[Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetLeaveBalanceOfEmpResponse>>> GetLeaveBalanceofEmp(int empId)
        {
            var result = await _leaveBalanceManager.GetLeaveBalanceofEmp(empId);
            return result;
        }
        [HttpGet("GetTotalLeavesFromEachDepartment")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<TotalLeavesFromEachDepartmentResponse>>> GetTotalLeavesFromEachDepartment()
        {
            var result = await _leaveBalanceManager.GetTotalLeavesFromEachDepartment();
            return result;
        }
    }
}
