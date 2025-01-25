using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using User.Application.Manager.Interface;
using User.Domain.Entities;
using User.Domain.Enum;
using User.Domain.Services.Interface;
using static User.Application.Common.CommonUtilities;

namespace User.Application.Manager.Implementation
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentManager(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;

        }

        public async Task<ServiceResult<List<GetDepartmentListResponseDTO>>> GetDepartmentList()
        {
            var allDept = await _departmentService.GetDepartmentList();
            if (allDept.Any())
            {
                var result = _mapper.Map<List<GetDepartmentListResponseDTO>>(allDept);
                return new ServiceResult<List<GetDepartmentListResponseDTO>>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Department {ResponseMessage.FetchedSuccessfully}",
                    Data = result.ToList()
                };
            }
            return new ServiceResult<List<GetDepartmentListResponseDTO>>()
            {
                Result = ResultStatus.Ok,
                Message = $"Department {ResponseMessage.FetchedFailed}",
                Data = new List<GetDepartmentListResponseDTO>()
            };
        }

        public async Task<ServiceResult<GetDepartmentDetailByIdResponseDTO>> GetDepartmentDetailById(int id)
        {
            var deptDetail = await _departmentService.GetDepartmentDetailById(id);
            if (deptDetail != null)
            {
                var dept = _mapper.Map<GetDepartmentDetailByIdResponseDTO>(deptDetail);
                return new ServiceResult<GetDepartmentDetailByIdResponseDTO>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Department {ResponseMessage.FetchedSuccessfully}",
                    Data = dept
                };
            }
            return new ServiceResult<GetDepartmentDetailByIdResponseDTO>()
            {
                Result = ResultStatus.unHandeledError,
                Message = $"Department {ResponseMessage.IdNotFound}",
                Data = new GetDepartmentDetailByIdResponseDTO()
            };
        }
    }
}
