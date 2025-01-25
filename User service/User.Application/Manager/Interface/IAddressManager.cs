using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using static User.Application.Common.CommonUtilities;

namespace User.Application.Manager.Interface
{
    public interface IAddressManager
    {
        Task<ServiceResult<List<GetAddressListResponseDTO>>> GetAddressList();
        Task<ServiceResult<GetAddressDetailByIdResponseDTO>> GetAddressDetailById(int id);
        Task<ServiceResult<bool>> AddAddress(AddAddressRequest reqEmp);
        Task<ServiceResult<bool>> UpdateAddress(UpdateAddressRequest reqEmp);
        Task<ServiceResult<bool>> DeleteAddress(int id);
    }
}
