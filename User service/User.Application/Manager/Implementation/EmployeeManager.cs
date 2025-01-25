using AutoMapper;
using Newtonsoft.Json;
using User.Application.Common;
using User.Application.DTO.Http;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using User.Application.Http.Interface;
using User.Application.Kafka.Interface;
using User.Application.Manager.Interface;
using User.Domain.Entities;
using User.Domain.Enum;
using User.Domain.Services.Interface;
using static User.Application.Common.CommonUtilities;

namespace User.Application.Manager.Implementation
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        private readonly IHttpAttendanceLeaveService _httpAttendanceLeaveService;
        private readonly ILeaveKafkaProducer _producer;
        private readonly IDepartmentService _departmentService;
        private readonly ICityService _cityService;
        private readonly IUserServiceFactory _userServiceFactory;

        public EmployeeManager(IEmployeeService employeeService, IAddressService addressService, ICountryService countryService, IMapper mapper, IHttpAttendanceLeaveService httpAttendanceLeaveService, ILeaveKafkaProducer producer, IDepartmentService departmentService, ICityService cityService, IUserServiceFactory userServiceFactory)
        {
            _employeeService = employeeService;
            _addressService = addressService;
            _countryService = countryService;
            _mapper = mapper;
            _httpAttendanceLeaveService = httpAttendanceLeaveService;
            _producer = producer;
            _departmentService = departmentService;
            _cityService = cityService;
            _userServiceFactory = userServiceFactory;

        }

        public async Task<ServiceResult<List<GetRegisterEmployeeListResponse>>> GetRegisterEmployeeList()
        {
            var allEmp = await _employeeService.GetRegisterEmployeeList();
            if (allEmp.Count>0)
            {
                var result = _mapper.Map<List<GetRegisterEmployeeListResponse>>(allEmp);
                return new ServiceResult<List<GetRegisterEmployeeListResponse>>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Employee {ResponseMessage.FetchedSuccessfully}",
                    Data = result.ToList()
                };
            }
            return new ServiceResult<List<GetRegisterEmployeeListResponse>>()
            {
                Result = ResultStatus.Ok,
                Message = $"Employee {ResponseMessage.FetchedFailed}",
                Data = new List<GetRegisterEmployeeListResponse>()
            };
        }
        public async Task<ServiceResult<List<GetRegisterEmployeeListWithDetailsResponse>>> GetRegisterEmployeeListWithDetails()
        {
            var allEmp = await _employeeService.GetRegisterEmployeeList();
            var allDept = await _departmentService.GetDepartmentList();
            var allAddress = await _addressService.GetAddressList();
            var allCountry = await _countryService.GetCountryList();
            var allCity = await _cityService.GetCityList();

            var result = from emp in allEmp
                         join dept in allDept on emp.DepartmentId equals dept.Id into empDept
                         from dept in empDept.DefaultIfEmpty()
                         join addr in allAddress on emp.AddressId equals addr.Id into empAddr
                         from addr in empAddr.DefaultIfEmpty()
                         join country in allCountry on addr.CountryId equals country.Id into addrCountry
                         from country in addrCountry.DefaultIfEmpty()
                         join city in allCity on addr.CityId equals city.Id into addrCity
                         from city in addrCity.DefaultIfEmpty()
                         select new GetRegisterEmployeeListWithDetailsResponse
                         {
                             Id = emp.Id,
                             FirstName = emp.FirstName,
                             MiddleName = emp.MiddleName,
                             LastName = emp.LastName,
                             Email = emp.Email,
                             MobileNo = emp.MobileNo,
                             AddressId = emp.AddressId,
                             CitizenshipNo = emp.CitizenshipNo,
                             DOB = emp.DOB.ToString("yyyy-MM-dd"),
                             DepartmentId = emp.DepartmentId,
                             Department = dept.Name,
                             Role = emp.Roles.ToString(),
                             GenderId = (int)emp.Gender,
                             Gender = emp.Gender.ToString(),
                             Address = addr.Name,
                             Country = country?.Name,
                             City = city?.Name,
                             StartDate = emp.StartDate.ToString("yyyy-MM-dd"),
                             Nationality = emp.Nationality
                         };

            if (result.Any())
            {

                return new ServiceResult<List<GetRegisterEmployeeListWithDetailsResponse>>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Employee {ResponseMessage.FetchedSuccessfully}",
                    Data = result.ToList()
                };
            }
            return new ServiceResult<List<GetRegisterEmployeeListWithDetailsResponse>>()
            {
                Result = ResultStatus.Ok,
                Message = $"Employee {ResponseMessage.FetchedFailed}",
                Data = new List<GetRegisterEmployeeListWithDetailsResponse>()
            };
        }

        public async Task<ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>> AllLeaveDetailsOfEmp(int id)
        {
            var leaveDetails = await _httpAttendanceLeaveService.GetLeaveRequestByEmpId(id);
            if (leaveDetails != null)
            {
                return new ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>
                {
                    Result = ResultStatus.Ok,
                    Message = $"Leave Details {ResponseMessage.FetchedSuccessfully}",
                    Data = leaveDetails.Data
                };
            }
            return new ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>
            {
                Result = ResultStatus.unHandeledError,
                Message = $"Leave Details {ResponseMessage.FetchedFailed}",
                Data = new List<GetLeaveRequestListBasedOnEmployeeResponse>()
            };
        }
        public async Task<ServiceResult<List<GetLeaveBalanceOfEmpResponse>>> AllLeaveBalanceDetailsOfEmp(int id)
        {
            var leaveBalanceDetails = await _httpAttendanceLeaveService.GetLeaveBalanceofEmp(id);
            if (leaveBalanceDetails != null)
            {
                return new ServiceResult<List<GetLeaveBalanceOfEmpResponse>>
                {
                    Result = ResultStatus.Ok,
                    Message = $"Leave Details {ResponseMessage.FetchedSuccessfully}",
                    Data = leaveBalanceDetails.Data
                };
            }
            return new ServiceResult<List<GetLeaveBalanceOfEmpResponse>>
            {
                Result = ResultStatus.unHandeledError,
                Message = $"Leave Details {ResponseMessage.FetchedFailed}",
                Data = new List<GetLeaveBalanceOfEmpResponse>()
            };

        }
        public async Task<ServiceResult<GetRegisterEmployeeDetailByIdResponse>> GetRegisterEmployeeDetailById(int id)
        {
            var empDetail = await _employeeService.GetRegisterEmployeeDetailById(id);
            if (empDetail != null)
            {
                var emp = _mapper.Map<GetRegisterEmployeeDetailByIdResponse>(empDetail);
                return new ServiceResult<GetRegisterEmployeeDetailByIdResponse>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Employee {ResponseMessage.FetchedSuccessfully}",
                    Data = emp
                };
            }
            return new ServiceResult<GetRegisterEmployeeDetailByIdResponse>()
            {
                Result = ResultStatus.unHandeledError,
                Message = $"Employee {ResponseMessage.IdNotFound}",
                Data = new GetRegisterEmployeeDetailByIdResponse()
            };
        }
        public async Task<ServiceResult<bool>> RegisterEmployee(RegisterEmployeeRequest reqEmp)
        {
            try
            {
                _userServiceFactory.BeginTransaction();
                var checkDuplicateEmail = await _employeeService.GetEmployeeByEmail(reqEmp.Email);
                if (checkDuplicateEmail != null)
                {
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = $"Employee {ResponseMessage.AddFailed}. Email already exists",
                        Data = false,
                    };
                }
                if(reqEmp.Password!= reqEmp.ConfirmPassword)
                {
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = $"Employee {ResponseMessage.AddFailed}. The password and confirm password doesnt match",
                        Data = false
                    };
                }
                var employee = _mapper.Map<Employee>(reqEmp);
                employee.Password = CommonUtilities.HashPassword(reqEmp.Password);
                employee.ConfirmPassword = employee.Password;

                //var address = _mapper.Map<Address>(reqEmp.Address);
                //int addAddress = await _addressService.AddAddress(address);
                //employee.AddressId = addAddress;
                //employee.Address.Id = addAddress;
                var result = await _employeeService.RegisterEmployee(employee);

                if (result > 0)
                {
                    employee.Id = result;
                    await _producer.AddLeaveBalanceProduce("", employee);

                    //await _httpAttendanceLeaveService.AddLeaveBalanceOfEmp(result);

                    _userServiceFactory.CommitTransaction();
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.Ok,
                        Message = $"Employee {ResponseMessage.AddedSuccessfully}",
                        Data = true,
                    };
                }
                _userServiceFactory.RollBack();
                return new ServiceResult<bool>()
                {
                    Result = ResultStatus.unHandeledError,
                    Message = $"Employee {ResponseMessage.AddFailed}",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                _userServiceFactory.RollBack();
                throw new Exception(ex.Message);
            }
        }


        public async Task<ServiceResult<bool>> UpdateRegisterEmployee(UpdateRegisterEmployeeRequest reqEmp)
        {
            try
            {
                _employeeService._factoryIn.BeginTransaction();
                var getExisting = await _employeeService.GetRegisterEmployeeDetailById(reqEmp.Id);
                if (getExisting == null)
                {
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = $"Employee {ResponseMessage.IdNotFound}",
                        Data = false
                    };
                }

                var empDetail = _mapper.Map(reqEmp, getExisting); // Map to the existing object
                empDetail.Email= getExisting.Email;
                empDetail.Address.Id = getExisting.AddressId;
                bool addAddress = await _addressService.UpdateAddress(empDetail.Address);
                
                var result = await _employeeService.UpdateRegisterEmployee(empDetail);
                if (!result)
                {
                    _employeeService._factoryIn.RollBack();
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = $"Employee {ResponseMessage.UpdateFailed}",
                        Data = false
                    };
                }
                
                _employeeService._factoryIn.CommitTransaction();
                return new ServiceResult<bool>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Employee {ResponseMessage.UpdatedSuccessfully}",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _employeeService._factoryIn.RollBack();
                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> DeleteRegisterEmployee(int id)
        {
            try
            {
                var getExisting = await _employeeService.GetRegisterEmployeeDetailById(id);
                if (getExisting == null)
                {
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = $"Employee {ResponseMessage.IdNotFound}",
                        Data = false
                    };
                }
                var result = await _employeeService.DeleteEmployee(getExisting);
                if (!result)
                {
                    return new ServiceResult<bool>()
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = $"Employee {ResponseMessage.DeleteFailed}",
                        Data = false
                    };
                }
                return new ServiceResult<bool>()
                {
                    Result = ResultStatus.Ok,
                    Message = $"Employee {ResponseMessage.DeletedSuccessfully}",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResult<GetRegisterEmployeeDetailAloongWithOtherDetailsByIdResponse>> GetRegisterEmployeeDetailAloongWithOtherDetailsById(int id)
        {
            var empDetail = await _employeeService.GetRegisterEmployeeDetailById(id);

            if (empDetail == null)
            {
                return new ServiceResult<GetRegisterEmployeeDetailAloongWithOtherDetailsByIdResponse>()
                {
                    Result = ResultStatus.unHandeledError,
                    Message = $"Employee {ResponseMessage.IdNotFound}",
                    Data = new GetRegisterEmployeeDetailAloongWithOtherDetailsByIdResponse()
                };
            }

            var addressDetail = await _addressService.GetAddressDetailById(empDetail.AddressId);

            var response = _mapper.Map<GetRegisterEmployeeDetailAloongWithOtherDetailsByIdResponse>(empDetail);
            response.Address = addressDetail;
            response.AddressName = addressDetail.Name;
            response.Address.Country = await _countryService.GetCountryDetailById(addressDetail.CountryId);
            response.Address.City = await _cityService.GetCityDetailById(addressDetail.CityId);
            return new ServiceResult<GetRegisterEmployeeDetailAloongWithOtherDetailsByIdResponse>()
            {
                Result = ResultStatus.Ok,
                Message = $"Employee {ResponseMessage.FetchedSuccessfully}",
                Data = response
            };
        }

        public async Task<ServiceResult<List<EnumDataResponseDto>>> GetAllRoles()
        {
            List<EnumDataResponseDto> data = new();
            foreach(var item in Enum.GetValues<Role>())
            {
                data.Add(new EnumDataResponseDto { Key = (int)item, Value = item.ToString() });
            }
            return new ServiceResult<List<EnumDataResponseDto>>()
            {
                Result = ResultStatus.Ok,
                Message = "Displaying All Roles",
                Data = data
            };
        }
    }
}
