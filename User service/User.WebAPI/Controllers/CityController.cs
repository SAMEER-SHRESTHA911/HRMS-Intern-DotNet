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
    public class CityController : ControllerBase
    {
        private readonly ICityManager _cityManager;
        private readonly IMapper _mapper;
        public CityController(ICityManager cityManager, IMapper mapper)
        {
            _cityManager = cityManager;
            _mapper = mapper;
        }
        [HttpGet("GetCityList")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetCityListResponseDTO>>> GeCityList()
        {
            var City = await _cityManager.GetCityList();
            return City;
        }
        [HttpGet("GetCityDetailById")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<GetCityDetailByIdResponseDTO>> GetCityDetailById(int id)
        {
            var City = await _cityManager.GetCityDetailById(id);
            return City;
        }
        [HttpGet("GetCityByCountryId")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetCityByCountryIdResponseDTO>>> GetCityByCountryId(int countryId)
        {
            var cities = await _cityManager.GetCityByCountryId(countryId);
            return cities;
        }
    }
}
