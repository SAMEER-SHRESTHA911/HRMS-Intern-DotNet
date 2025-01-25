using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using User.Domain.Entities;

namespace User.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, RegisterEmployeeRequest>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles));
            CreateMap<RegisterEmployeeRequest,Employee > ().ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Role));
            CreateMap<Employee, UpdateRegisterEmployeeRequest>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles)); 
            CreateMap<UpdateRegisterEmployeeRequest, Employee>().ForMember(dest => dest.Password, opt => opt.Ignore()).ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore()).ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Role)); 
            CreateMap<Employee, GetRegisterEmployeeDetailByIdResponse>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles)); 
            CreateMap<Employee, GetRegisterEmployeeListResponse>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles)); 

            CreateMap<Department, GetDepartmentListResponseDTO>();
            CreateMap<Department, GetDepartmentDetailByIdResponseDTO>();

            CreateMap<Country, GetCountryListResponseDTO>();
            CreateMap<Country, GetCountryDetailByIdResponseDTO>();

            CreateMap<City, GetCityListResponseDTO>().ReverseMap();
            CreateMap<City, GetCityDetailByIdResponseDTO>().ReverseMap();
            CreateMap<City, GetCityByCountryIdResponseDTO>();

            CreateMap<Address, GetAddressListResponseDTO>().ReverseMap();
            CreateMap<Address, GetAddressDetailByIdResponseDTO>().ReverseMap();
            CreateMap<Address, AddAddressRequest>().ReverseMap();
            CreateMap<Address, UpdateAddressRequest>().ReverseMap();

            CreateMap<Employee, GetRegisterEmployeeDetailAloongWithOtherDetailsByIdResponse>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles));
            CreateMap< GetRegisterEmployeeDetailAloongWithOtherDetailsByIdResponse, Employee>().ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Role));

        }
    }

}
