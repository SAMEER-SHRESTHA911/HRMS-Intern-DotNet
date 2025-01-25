using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO.Response
{
    public class GetCountryListResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    } 
    public class GetCountryDetailByIdResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
