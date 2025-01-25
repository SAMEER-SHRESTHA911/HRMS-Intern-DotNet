using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Application.DTO.Request
{
    public class AddAddressRequest
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }

    }
    public class UpdateAddressRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }

    }
}
