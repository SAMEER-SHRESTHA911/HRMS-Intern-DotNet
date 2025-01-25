using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;
using User.Domain.Services.Interface;

namespace User.Infrastructure.Service.Implementation
{
    public class EmployeeService: IEmployeeService
    {
        public readonly IUserServiceFactory _factory = null;
        public EmployeeService(IUserServiceFactory factory)
        {
            _factory = factory;
        }

        public IUserServiceFactory _factoryIn   // property
        {
            get { return _factory; }
            set { }
        }

        public async Task <List<Employee>> GetRegisterEmployeeList()
        {
            var emp = _factory.GetInstance<Employee>();
            var getEmployee = await emp.ListAsync();
            return getEmployee;
        }
        public async Task<Employee> GetRegisterEmployeeDetailById(int id)
        {
            var emp = _factory.GetInstance<Employee>();
            var getEmpDetail = await emp.FindAsync(id);
            return getEmpDetail;
        }
        public async Task<int> RegisterEmployee(Employee registerEmployee)
        {
            var emp =  _factory.GetInstance<Employee>();
            var employee = await emp.AddAsync(registerEmployee);
            return employee.Id;
        }
        public async Task<bool> UpdateRegisterEmployee(Employee registerEmployee)
        {
            var emp = _factory.GetInstance<Employee>();
            await emp.UpdateAsync(registerEmployee);
            return true;
        }
        public async Task<bool> DeleteEmployee(Employee employee)
        {
            var emp = _factory.GetInstance<Employee>();
            employee.IsDeleted = true;
            await emp.UpdateAsync(employee);
            return true;
        }
        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            var emp = _factory.GetInstance<Employee>();
            var getEmpDetail = (await emp.ListAsync()).Where(x => x.Email == email).FirstOrDefault();
            return getEmpDetail;
        }
    }
}
