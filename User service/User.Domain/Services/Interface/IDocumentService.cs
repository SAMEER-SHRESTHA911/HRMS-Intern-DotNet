using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Domain.Services.Interface
{
    public interface IDocumentService
    {
        Task<int> AddDocumentOfEmp(Documents request);
        Task<Documents> GetDocumentOfEmpById(int id);
        Task<bool> UpdateDocumentOfEmp(Documents request);
        Task<Documents> GetProfilePictureOfEmp(int empId);
    }
}
