using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO.Response
{
    public class GetAddressListResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
    }
    public class GetAddressDetailByIdResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }

        public int CityId { get; set; }
        public string City { get; set; }

    }
}
