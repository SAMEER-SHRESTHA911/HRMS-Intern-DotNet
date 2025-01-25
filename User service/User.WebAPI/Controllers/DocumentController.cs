using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using User.Application.Manager.Interface;
using static User.Application.Common.CommonUtilities;

namespace User.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentManager _documentManager;
        public DocumentController(IDocumentManager documentManager)
        {
            _documentManager = documentManager;
        }
        [HttpPost("AddDocumentOfEmp")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<bool>> AddDocumentOfEmp(AddDocumentDTO request)
        {
            var result = await _documentManager.AddDocumentOfEmp(request);
            return result;
        } 
        [HttpPut("UpdateDocumentOfEmp")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<bool>> UpdateDocumentOfEmp(UpdateDocumentDTO request)
        {
            var result = await _documentManager.UpdateDocumentOfEmp(request);
            return result;
        }
        [HttpGet("GetProfilePictureOfEmp")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<ServiceResult<ProfilePictureResponse>> GetProfilePictureOfEmp(int empId)
        {
            var result = await _documentManager.GetProfilePictureOfEmp(empId);
            return result;
        }
    }
}
