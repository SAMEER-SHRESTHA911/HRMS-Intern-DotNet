using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Application.DTO.Response;
using User.Application.Manager.Interface;
using static User.Application.Common.CommonUtilities;

namespace User.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentManager _departmentManager;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentManager departmentManager, IMapper mapper)
        {
            _departmentManager = departmentManager;
            _mapper = mapper;
        }
        [HttpGet("GetDepartmentList")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetDepartmentListResponseDTO>>> GetDepartmentList()
        {
            var dept = await _departmentManager.GetDepartmentList();
            return dept;
        }
        [HttpGet("GetDepartmentDetailById")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<GetDepartmentDetailByIdResponseDTO>> GetDepartmentDetailById(int id)
        {
            var dept = await _departmentManager.GetDepartmentDetailById(id);
            return dept;
        }
    }
}
