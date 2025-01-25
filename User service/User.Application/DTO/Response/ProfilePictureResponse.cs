using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO.Response
{
    public class ProfilePictureResponse
    {
        public int EmployeeId { get; set; }
        public string ImageName { get; set; }
        public string ImageDataBase64 { get; set; }
    }
}
