using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static User.Application.Common.CommonUtilities;
using User.Application.DTO.Response;

namespace User.Application.Manager.Interface
{
    public interface IDepartmentManager
    {
        Task<ServiceResult<List<GetDepartmentListResponseDTO>>> GetDepartmentList();
        Task<ServiceResult<GetDepartmentDetailByIdResponseDTO>> GetDepartmentDetailById(int id);
    }
}
