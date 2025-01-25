using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Domain.Services.Interface
{
    public interface IEmployeeService
    {
        IUserServiceFactory _factoryIn { get; }
        Task<List<Employee>> GetRegisterEmployeeList();
        Task<Employee> GetRegisterEmployeeDetailById(int id);
        Task<int> RegisterEmployee(Employee registerEmployee);
        Task<bool> UpdateRegisterEmployee(Employee registerEmployee);
        Task<bool> DeleteEmployee(Employee employee);
        Task<Employee> GetEmployeeByEmail(string email);
    }
}
