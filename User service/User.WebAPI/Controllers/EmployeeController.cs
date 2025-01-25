using AutoMapper;
using Confluent.Kafka;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using User.Application.DTO.Http;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using User.Application.Manager.Interface;
using static User.Application.Common.CommonUtilities;

namespace User.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeManager employeeManager, IMapper mapper)
        {
            _employeeManager = employeeManager;
            _mapper = mapper;
        }
        [HttpGet("GetRegisterEmployeeList")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetRegisterEmployeeListResponse>>> GetRegisterEmployeeList()
        {
            var employees = await _employeeManager.GetRegisterEmployeeList();
            return employees;
        }
        [HttpGet("GetRegisterEmployeeDetailById")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<GetRegisterEmployeeDetailByIdResponse>> GetRegisterEmployeeDetailById(int id)
        {
            var employee = await _employeeManager.GetRegisterEmployeeDetailById(id);
            return employee;
        }
        [HttpPost("RegisterEmployee")]
        public async Task<ServiceResult<bool>> RegisterEmployee(RegisterEmployeeRequest reqEmp)
        {
            var employee = await _employeeManager.RegisterEmployee(reqEmp);

            return employee;
        }
        [HttpPut("UpdateRegisterEmployee")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<bool>> UpdateRegisterEmployee(UpdateRegisterEmployeeRequest reqEmp)
        {
            var employee = await _employeeManager.UpdateRegisterEmployee(reqEmp);
            return employee;
        }
        [HttpDelete("DeleteRegisterEmployee")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<bool>> DeleteRegisterEmployee(int id)
        {
            var employee = await _employeeManager.DeleteRegisterEmployee(id);
            return employee;
        }
        //[HttpGet("GetRegisterEmployeeDetailAloongWithOtherDetailsById")]
        //public async Task<ServiceResult<GetRegisterEmployeeDetailAloongWithOtherDetailsByIdResponse>> GetRegisterEmployeeDetailAloongWithOtherDetailsById(int id)
        //{
        //    var employee = await _employeeManager.GetRegisterEmployeeDetailAloongWithOtherDetailsById(id);
        //    return employee;
        //}

        [HttpGet("GetRegisterEmployeeListWithDetails")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetRegisterEmployeeListWithDetailsResponse>>> GetRegisterEmployeeListWithDetails()
        {
            var employee = await _employeeManager.GetRegisterEmployeeListWithDetails();
            return employee;
        }
        [HttpGet("GetLeaveRequestByEmpId")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>> GetLeaveRequestByEmpId(int empId)
        {
            var leaveRequest = await _employeeManager.AllLeaveDetailsOfEmp(empId);
            return leaveRequest;
        }
        [HttpGet("GetLeaveBalanceByEmpId")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetLeaveBalanceOfEmpResponse>>> GetLeaveBalanceByEmpId(int empId)
        {
            var leaveRequest = await _employeeManager.AllLeaveBalanceDetailsOfEmp(empId);
            return leaveRequest;
        }
        [HttpGet("GetAllRoles")]
        public async Task<ServiceResult<List<EnumDataResponseDto>>> GetAllRoles()
        {
            var roles = await _employeeManager.GetAllRoles();
            return roles;
        }
    }
}
