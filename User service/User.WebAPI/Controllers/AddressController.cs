using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using User.Application.Manager.Interface;
using static User.Application.Common.CommonUtilities;

namespace User.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager _addressManager;
        private readonly IMapper _mapper;
        public AddressController(IAddressManager addressManager, IMapper mapper)
        {
            _addressManager = addressManager;
            _mapper = mapper;
        }
        [HttpGet("GetAddressList")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<List<GetAddressListResponseDTO>>> GetAddressList()
        {
            var address = await _addressManager.GetAddressList();
            return address;
        }
        [HttpGet("GetAddressDetailById")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<GetAddressDetailByIdResponseDTO>> GetAddressDetailById(int id)
        {
            var address = await _addressManager.GetAddressDetailById(id);
            return address;
        }
        [HttpPost("AddAddress")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<bool>> AddAddress(AddAddressRequest reqEmp)
        {
            var address = await _addressManager.AddAddress(reqEmp);
            return address;
        }
        [HttpPut("UpdateAddress")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<bool>> UpdateAddress(UpdateAddressRequest reqEmp)
        {
            var address = await _addressManager.UpdateAddress(reqEmp);
            return address;
        }
        [HttpDelete("DeleteAddress")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<bool>> DeleteAddress(int id)
        {
            var address = await _addressManager.DeleteAddress(id);
            return address;
        }


    }
}
