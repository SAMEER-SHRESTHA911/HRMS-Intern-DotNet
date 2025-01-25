using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;
using User.Domain.Services.Interface;

namespace User.Infrastructure.Service.Implementation
{
    public class CityService : ICityService
    {
        public readonly IUserServiceFactory _factory = null;
        public CityService(IUserServiceFactory factory)
        {
            _factory = factory;
        }

        public IUserServiceFactory _factoryIn   // property
        {
            get { return _factory; }
            set { }
        }

        public async Task<List<City>> GetCityList()
        {
            var city = _factory.GetInstance<City>();
            var getCity = await city.ListAsync();
            return getCity;
        }
        public async Task<City> GetCityDetailById(int id)
        {
            var city = _factory.GetInstance<City>();
            var getCityDetail = await city.FindAsync(id);
            return getCityDetail;
        }

        public async Task<List<City>> GetCityByCountryId(int countryId)
        {
            var city = _factory.GetInstance<City>();
            var getCities = (await city.ListAsync()).Where(x=>x.CountryId == countryId).ToList();
            return getCities;
        }
    }
}
