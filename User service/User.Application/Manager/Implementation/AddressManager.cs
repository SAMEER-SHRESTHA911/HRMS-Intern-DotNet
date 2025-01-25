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
    public class AddressManager : IAddressManager
    {
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public AddressManager(IAddressService addressService, IMapper mapper, ICountryService countryService, ICityService cityService)
        {
            _addressService = addressService;
            _mapper = mapper;
           _countryService = countryService;
            _cityService = cityService;
        }

        public async Task<ServiceResult<List<GetAddressListResponseDTO>>> GetAddressList()
        {
            var allAddress = await _addressService.GetAddressList();
            if (allAddress.Any())
            {
                //var result = new List<GetAddressListResponseDTO>();
                //foreach (var address in allAddress)
                //{
                //    var cityDetail = await _cityService.GetCityDetailById(address.CityId);
                //    var countryDetail = await _countryService.GetCountryDetailById(address.CountryId);

                //    var dto = new GetAddressListResponseDTO
                //    {
                //        Id = address.Id,
                //        Name = address.Name,
                //        CountryId = address.CountryId,
                //        CityId = address.CityId,
                //        City = cityDetail?.Name,
                //        Country = countryDetail?.Name
                //    };
                //    result.Add(dto);
                //}

                var result = _mapper.Map<List<GetAddressListResponseDTO>>(allAddress);
                foreach (var item in result)
                {
                    var cityDetail = await _cityService.GetCityDetailById(item.CityId);
                    var countryDetail = await _countryService.GetCountryDetailById(item.CountryId);

                    // Add city and country names to DTO
                    item.City = cityDetail?.Name;
                    item.Country = countryDetail?.Name;
                }
                return new ServiceResult<List<GetAddressListResponseDTO>>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Address {ResponseMessage.FetchedSuccessfully}",
                    Data = result
                };
            }
            return new ServiceResult<List<GetAddressListResponseDTO>>()
            {
                Result = ResultStatus.Ok,
                Message = $"Address {ResponseMessage.FetchedFailed}",
                Data = new List<GetAddressListResponseDTO>()
            };
        }

        public async Task<ServiceResult<GetAddressDetailByIdResponseDTO>> GetAddressDetailById(int id)
        {
            var addDetail = await _addressService.GetAddressDetailById(id);
            if (addDetail != null)
            {
                var add = _mapper.Map<GetAddressDetailByIdResponseDTO>(addDetail);
                return new ServiceResult<GetAddressDetailByIdResponseDTO>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Address {ResponseMessage.FetchedSuccessfully}",
                    Data = add
                };
            }
            return new ServiceResult<GetAddressDetailByIdResponseDTO>()
            {
                Result = ResultStatus.unHandeledError,
                Message = $"Address {ResponseMessage.IdNotFound}",
                Data = new GetAddressDetailByIdResponseDTO()
            };
        }
        public async Task<ServiceResult<bool>> AddAddress(AddAddressRequest reqAdd)
        {
            try
            {
                var address = _mapper.Map<Address>(reqAdd);
                var result = await _addressService.AddAddress(address);
                if (result > 0)
                {
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.Ok,
                        Message = $"Address {ResponseMessage.AddedSuccessfully}",
                        Data = true,
                    };
                }
                return new ServiceResult<bool>()
                {
                    Result = ResultStatus.unHandeledError,
                    Message = $"Address {ResponseMessage.AddFailed}",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ServiceResult<bool>> UpdateAddress(UpdateAddressRequest reqAdd)
        {
            try
            {
                var getExisting = await _addressService.GetAddressDetailById(reqAdd.Id);
                if (getExisting == null)
                {
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = $"Address {ResponseMessage.IdNotFound}",
                        Data = false
                    };
                }
                var addDetail = _mapper.Map<Address>(reqAdd);
                var result = await _addressService.UpdateAddress(addDetail);
                if (!result)
                {
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = $"Address {ResponseMessage.UpdateFailed}",
                        Data = false
                    };
                }
                return new ServiceResult<bool>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Address {ResponseMessage.UpdatedSuccessfully}",
                    Data = true
                };
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> DeleteAddress(int id)
        {
            try
            {
                var getExisting = await _addressService.GetAddressDetailById(id);
                if (getExisting == null)
                {
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = $"Address {ResponseMessage.IdNotFound}",
                        Data = false
                    };
                }
                var result = await _addressService.UpdateAddress(getExisting);
                if (!result)
                {
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = $"Address {ResponseMessage.DeleteFailed}",
                        Data = false
                    };
                }
              
                return new ServiceResult<bool>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Address {ResponseMessage.DeletedSuccessfully}",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
