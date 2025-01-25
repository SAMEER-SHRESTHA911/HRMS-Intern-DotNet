using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Common;

namespace User.Domain.Entities
{
    public class City: BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
       
        public int CountryId { get; set; }
    }
}
