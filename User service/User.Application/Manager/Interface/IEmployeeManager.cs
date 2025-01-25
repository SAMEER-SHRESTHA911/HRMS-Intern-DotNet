using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTO.Http;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using static User.Application.Common.CommonUtilities;

namespace User.Application.Manager.Interface
{
    public interface IEmployeeManager
    {
        Task<ServiceResult<List<GetRegisterEmployeeListResponse>>> GetRegisterEmployeeList();
        Task<ServiceResult<GetRegisterEmployeeDetailByIdResponse>> GetRegisterEmployeeDetailById(int id);
        Task<ServiceResult<bool>> RegisterEmployee(RegisterEmployeeRequest reqEmp);
        Task<ServiceResult<bool>> UpdateRegisterEmployee(UpdateRegisterEmployeeRequest reqEmp);
        Task<ServiceResult<bool>> DeleteRegisterEmployee(int id);
        Task<ServiceResult<GetRegisterEmployeeDetailAloongWithOtherDetailsByIdResponse>> GetRegisterEmployeeDetailAloongWithOtherDetailsById(int id);
        Task<ServiceResult<List<GetRegisterEmployeeListWithDetailsResponse>>> GetRegisterEmployeeListWithDetails();
        Task<ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>> AllLeaveDetailsOfEmp(int id);
        Task<ServiceResult<List<GetLeaveBalanceOfEmpResponse>>> AllLeaveBalanceDetailsOfEmp(int id);
        Task<ServiceResult<List<EnumDataResponseDto>>> GetAllRoles();
    }
}
