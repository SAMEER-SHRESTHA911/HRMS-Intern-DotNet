using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Domain.Services.Interface
{
    public interface IDepartmentService
    {
        IUserServiceFactory _factoryIn { get; }
        Task<List<Department>> GetDepartmentList();
        Task<Department> GetDepartmentDetailById(int id);
    }
}
