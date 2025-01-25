using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Common;
using User.Domain.Enum;

namespace User.Domain.Entities
{
    public class Documents : BaseEntity
    {
        public int EmployeeId { get; set; }
        public DocumentType DocumentType { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
    }
}
