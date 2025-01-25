using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;
using User.Domain.Services.Interface;

namespace User.Infrastructure.Service.Implementation
{
    public class DocumentService : IDocumentService
    {
        public readonly IUserServiceFactory _factory = null;
        public DocumentService(IUserServiceFactory factory)
        {
            _factory = factory;
        }
        public async Task<Documents> GetDocumentOfEmpById(int id)
        {
            var document = _factory.GetInstance<Documents>();
            var result = await document.FindAsync(id);
            return result;
        }
        public async Task<int> AddDocumentOfEmp(Documents request)
        {
            var document = _factory.GetInstance<Documents>();
            var result = await document.AddAsync(request);
            return result.Id;
        }

        public async Task<bool> UpdateDocumentOfEmp(Documents request)
        {
            var document = _factory.GetInstance<Documents>();
            await document.UpdateAsync(request);
            return true;
        }
        public async Task<Documents> GetProfilePictureOfEmp(int empId)
        {
            var document = _factory.GetInstance<Documents>();
            var result = (await document.ListAsync()).FirstOrDefault(x=>x.EmployeeId == empId && x.DocumentType == Domain.Enum.DocumentType.ProfileImage);
            return result;
        }
    }
}
