using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO.Response
{
    public class GetDepartmentListResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetDepartmentDetailByIdResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
