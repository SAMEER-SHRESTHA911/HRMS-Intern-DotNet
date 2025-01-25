using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static User.Application.Common.CommonUtilities;
using User.Application.DTO.Response;

namespace User.Application.Manager.Interface
{
    public interface ICityManager
    {
        Task<ServiceResult<List<GetCityListResponseDTO>>> GetCityList();
        Task<ServiceResult<GetCityDetailByIdResponseDTO>> GetCityDetailById(int id);
        Task<ServiceResult<List<GetCityByCountryIdResponseDTO>>> GetCityByCountryId(int countryId);
    }
}
