using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Domain.Services.Interface
{
    public interface ICityService
    {
        IUserServiceFactory _factoryIn { get; }
        Task<List<City>> GetCityList();
        Task<City> GetCityDetailById(int id);
        Task<List<City>> GetCityByCountryId(int countryId);
    }
}
