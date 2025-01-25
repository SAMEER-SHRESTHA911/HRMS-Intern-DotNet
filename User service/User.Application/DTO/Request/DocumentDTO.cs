using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Enum;

namespace User.Application.DTO.Request
{
    public class AddDocumentDTO
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int DocumentType { get; set; }
        [Required]
        public IFormFile? ImageFile { get; set; }
    }
    public class UpdateDocumentDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int DocumentType { get; set; }
      
        //public string? ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
