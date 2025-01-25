using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Domain.Services.Interface
{
    public interface ICountryService
    {
        IUserServiceFactory _factoryIn { get; }
        Task<List<Country>> GetCountryList();
        Task<Country> GetCountryDetailById(int id);
    }
}
