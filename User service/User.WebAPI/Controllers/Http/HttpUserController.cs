using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Application.DTO.Response;
using User.Application.Manager.Interface;
using static User.Application.Common.CommonUtilities;

namespace User.WebAPI.Controllers.Http
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpUserController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IDepartmentManager _departmentManager;
        public HttpUserController(IEmployeeManager employeeManager, IDepartmentManager departmentManager)
        {
            _employeeManager = employeeManager;
            _departmentManager = departmentManager;
        }
        [HttpGet("GetRegisterEmployeeList")]
        public async Task<ServiceResult<List<GetRegisterEmployeeListResponse>>> GetRegisterEmployeeList()
        {
            var employees = await _employeeManager.GetRegisterEmployeeList();
            return employees;
        }
        [HttpGet("GetRegisterEmployeeDetailById")]
        public async Task<ServiceResult<GetRegisterEmployeeDetailByIdResponse>> GetRegisterEmployeeDetailById(int id)
        {
            var employee = await _employeeManager.GetRegisterEmployeeDetailById(id);
            return employee;
        } 
        [HttpGet("GetDepartmentList")]
        public async Task<ServiceResult<List<GetDepartmentListResponseDTO>>> GetDepartmentList()
        {
            var department = await _departmentManager.GetDepartmentList();
            return department;
        }
    }
}
