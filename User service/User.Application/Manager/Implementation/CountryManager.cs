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
    public class CountryManager : ICountryManager
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryManager(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;

        }

        public async Task<ServiceResult<List<GetCountryListResponseDTO>>> GetCountryList()
        {
            var allDept = await _countryService.GetCountryList();
            if (allDept.Any())
            {
                var result = _mapper.Map<List<GetCountryListResponseDTO>>(allDept);
                return new ServiceResult<List<GetCountryListResponseDTO>>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Country {ResponseMessage.FetchedSuccessfully}",
                    Data = result.ToList()
                };
            }
            return new ServiceResult<List<GetCountryListResponseDTO>>()
            {
                Result = ResultStatus.Ok,
                Message = $"Country {ResponseMessage.FetchedFailed}",
                Data = new List<GetCountryListResponseDTO>()
            };
        }

        public async Task<ServiceResult<GetCountryDetailByIdResponseDTO>> GetCountryDetailById(int id)
        {
            var deptDetail = await _countryService.GetCountryDetailById(id);
            if (deptDetail != null)
            {
                var country = _mapper.Map<GetCountryDetailByIdResponseDTO>(deptDetail);
                return new ServiceResult<GetCountryDetailByIdResponseDTO>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Country {ResponseMessage.FetchedSuccessfully}",
                    Data = country
                };
            }
            return new ServiceResult<GetCountryDetailByIdResponseDTO>()
            {
                Result = ResultStatus.unHandeledError,
                Message = $"Country {ResponseMessage.IdNotFound}",
                Data = new GetCountryDetailByIdResponseDTO()
            };
        }
    }
}
