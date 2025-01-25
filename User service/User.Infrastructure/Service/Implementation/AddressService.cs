using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;
using User.Domain.Services.Interface;

namespace User.Infrastructure.Service.Implementation
{
    public class AddressService : IAddressService
    {
        public readonly IUserServiceFactory _factory = null;
        public AddressService(IUserServiceFactory factory)
        {
            _factory = factory;
        }

        public IUserServiceFactory _factoryIn   // property
        {
            get { return _factory; }
            set { }
        }

        public async Task <List<Address>> GetAddressList()
        {
            var address = _factory.GetInstance<Address>();
            var getAddress = await address.ListAsync();
            return getAddress;
        }
        public async Task<Address> GetAddressDetailById(int id)
        {
            var address = _factory.GetInstance<Address>();
            var getAddressDetail = await address.FindAsync(id);
            return getAddressDetail;
        }
        public async Task<int> AddAddress(Address reqAddress)
        {
            var address =  _factory.GetInstance<Address>();
            var add = await address.AddAsync(reqAddress);
            return add.Id;
        }
        public async Task<bool> UpdateAddress(Address reqAddress)
        {
            var address = _factory.GetInstance<Address>();
            await address.UpdateAsync(reqAddress);
            return true;
        }
        public async Task<bool> DeleteAddress(Address add)
        {
            var address = _factory.GetInstance<Address>();
            add.IsDeleted = true;
            await address.UpdateAsync(add);
            return true;
        }
    }
}
