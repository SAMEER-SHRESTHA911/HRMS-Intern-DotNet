using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;
using User.Domain.Services.Interface;

namespace User.Infrastructure.Service.Implementation
{
    public class CountryService:ICountryService
    {
        public readonly IUserServiceFactory _factory = null;
        public CountryService(IUserServiceFactory factory)
        {
            _factory = factory;
        }

        public IUserServiceFactory _factoryIn   // property
        {
            get { return _factory; }
            set { }
        }

        public async Task<List<Country>> GetCountryList()
        {
            var country = _factory.GetInstance<Country>();
            var getCountry = await country.ListAsync();
            return getCountry;
        }
        public async Task<Country> GetCountryDetailById(int id)
        {
            var country = _factory.GetInstance<Country>();
            var getCountryDetail = await country.FindAsync(id);
            return getCountryDetail;
        }
    }
}
