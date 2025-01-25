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
    public class CityManager : ICityManager
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityManager(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;

        }

        public async Task<ServiceResult<List<GetCityListResponseDTO>>> GetCityList()
        {
            var allCity = await _cityService.GetCityList();
            if (allCity.Any())
            {
                var result = _mapper.Map<List<GetCityListResponseDTO>>(allCity);
                return new ServiceResult<List<GetCityListResponseDTO>>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"City {ResponseMessage.FetchedSuccessfully}",
                    Data = result.ToList()
                };
            }
            return new ServiceResult<List<GetCityListResponseDTO>>()
            {
                Result = ResultStatus.Ok,
                Message = $"City {ResponseMessage.FetchedFailed}",
                Data = new List<GetCityListResponseDTO>()
            };
        }

        public async Task<ServiceResult<GetCityDetailByIdResponseDTO>> GetCityDetailById(int id)
        {
            var cityDetail = await _cityService.GetCityDetailById(id);
            if (cityDetail != null)
            {
                var city = _mapper.Map<GetCityDetailByIdResponseDTO>(cityDetail);
                return new ServiceResult<GetCityDetailByIdResponseDTO>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"City {ResponseMessage.FetchedSuccessfully}",
                    Data = city
                };
            }
            return new ServiceResult<GetCityDetailByIdResponseDTO>()
            {
                Result = ResultStatus.unHandeledError,
                Message = $"City {ResponseMessage.IdNotFound}",
                Data = new GetCityDetailByIdResponseDTO()
            };
        }

        public async Task<ServiceResult<List<GetCityByCountryIdResponseDTO>>> GetCityByCountryId(int countryId)
        {
            var cities = await _cityService.GetCityByCountryId(countryId);
            if(cities.Count() > 0)
            {
                var city = _mapper.Map<List<GetCityByCountryIdResponseDTO>>(cities);
                return new ServiceResult<List<GetCityByCountryIdResponseDTO>>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"City {ResponseMessage.FetchedSuccessfully}",
                    Data = city
                };
            }
            return new ServiceResult<List<GetCityByCountryIdResponseDTO>>()
            {
                Result = ResultStatus.unHandeledError,
                Message = $"City {ResponseMessage.IdNotFound}",
                Data = new List<GetCityByCountryIdResponseDTO>()
            };
        }
    }
}
