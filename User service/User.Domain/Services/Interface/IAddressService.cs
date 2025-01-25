using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Domain.Services.Interface
{
    public interface IAddressService
    {
        IUserServiceFactory _factoryIn { get; }
        Task<List<Address>> GetAddressList();
        Task<Address> GetAddressDetailById(int id);
        Task<int> AddAddress(Address reqAddress);
        Task<bool> UpdateAddress(Address reqAddress);
        Task<bool> DeleteAddress(Address Address);
    }
}
