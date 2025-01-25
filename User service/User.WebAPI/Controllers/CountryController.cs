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
    public class CountryController : ControllerBase
    {
        private readonly ICountryManager _countryManager;
        private readonly IMapper _mapper;
        public CountryController(ICountryManager countryManager, IMapper mapper)
        {
            _countryManager = countryManager;
            _mapper = mapper;
        }
        [HttpGet("GetCountryList")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetCountryListResponseDTO>>> GeCountryList()
        {
            var country = await _countryManager.GetCountryList();
            return country;
        }
        [HttpGet("GetCountryDetailById")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<GetCountryDetailByIdResponseDTO>> GetCountryDetailById(int id)
        {
            var country = await _countryManager.GetCountryDetailById(id);
            return country;
        }
    }
}
