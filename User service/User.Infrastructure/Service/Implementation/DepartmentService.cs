using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;
using User.Domain.Services.Interface;

namespace User.Infrastructure.Service.Implementation
{
    public class DepartmentService:IDepartmentService
    {
        public readonly IUserServiceFactory _factory = null;
        public DepartmentService(IUserServiceFactory factory)
        {
            _factory = factory;
        }

        public IUserServiceFactory _factoryIn   // property
        {
            get { return _factory; }
            set { }
        }

        public async Task<List<Department>> GetDepartmentList()
        {
            var dept = _factory.GetInstance<Department>();
            var getDepartment = await dept.ListAsync();
            return getDepartment;
        }
        public async Task<Department> GetDepartmentDetailById(int id)
        {
            var dept = _factory.GetInstance<Department>();
            var getDeptDetail = await dept.FindAsync(id);
            return getDeptDetail;
        }
    }
}
