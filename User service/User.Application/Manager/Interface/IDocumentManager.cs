using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static User.Application.Common.CommonUtilities;
using User.Application.DTO.Request;
using User.Application.DTO.Response;

namespace User.Application.Manager.Interface
{
    public interface IDocumentManager
    {
        Task<ServiceResult<bool>> AddDocumentOfEmp(AddDocumentDTO request);
        Task<ServiceResult<bool>> UpdateDocumentOfEmp(UpdateDocumentDTO request);
        Task<ServiceResult<ProfilePictureResponse>> GetProfilePictureOfEmp(int empId);
    }
}
