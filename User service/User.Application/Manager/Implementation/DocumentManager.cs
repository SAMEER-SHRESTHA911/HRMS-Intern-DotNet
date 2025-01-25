using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using User.Application.Manager.Interface;
using User.Domain.Entities;
using User.Domain.Enum;
using User.Domain.Services.Interface;
using static User.Application.Common.CommonUtilities;

namespace User.Application.Manager.Implementation
{
    public class DocumentManager : IDocumentManager
    {
        private readonly IFileService _fileService;
        private readonly IDocumentService _documentService;
        private readonly IEmployeeService _employeeService;
        public DocumentManager(IFileService fileService, IDocumentService documentService, IEmployeeService employeeService)
        {
            _fileService = fileService;
            _documentService = documentService;
            _employeeService = employeeService;
        }
        public async Task<ServiceResult<ProfilePictureResponse>> GetProfilePictureOfEmp(int empId)
        {
            var getProfile = await _documentService.GetProfilePictureOfEmp(empId);
            if (getProfile == null || getProfile.ImageData == null)
            {
                return new ServiceResult<ProfilePictureResponse>
                {
                    Result = ResultStatus.unHandeledError,
                    Message = "Employee profile pic doesnt exists",
                    Data = new ProfilePictureResponse(),
                };
            }
            string imageBase64 = Convert.ToBase64String(getProfile.ImageData);
            var result =  new ProfilePictureResponse
            {
                EmployeeId = getProfile.EmployeeId,
                ImageName = getProfile.ImageName,
                ImageDataBase64 = imageBase64
            };
            return new ServiceResult<ProfilePictureResponse>
            {
                Result = ResultStatus.Ok,
                Message = $"Profile picture {ResponseMessage.FetchedSuccessfully}",
                Data = result,
            };
        }


        public async Task<ServiceResult<bool>> AddDocumentOfEmp(AddDocumentDTO request)
        {
            try
            {
                var getEmployee = await _employeeService.GetRegisterEmployeeDetailById(request.EmployeeId);
                if (getEmployee == null)
                {
                    return new ServiceResult<bool>
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = "Employee doesnt exists",
                        Data = false,
                    };
                }
                if (request.ImageFile?.Length > 1 * 1024 * 1024)
                {
                    return new ServiceResult<bool>
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = "File size shouldnt exceed 1MB",
                        Data = false,
                    };
                }
                string[] allowedFileExtensions = { ".jpg", ".jpeg", ".png" };
                string createdImageName = await _fileService.SaveFileAsync(request.ImageFile, allowedFileExtensions);

                byte[] imageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    request.ImageFile.CopyTo(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }


                var document = new Documents
                {
                    EmployeeId = request.EmployeeId,
                    DocumentType = (DocumentType)request.DocumentType,
                    ImageName = createdImageName,
                    ImageData = imageBytes
                };
                var addedImage = await _documentService.AddDocumentOfEmp(document);
                if (addedImage > 0)
                {
                    return new ServiceResult<bool>
                    {
                        Result = ResultStatus.Ok,
                        Message = "File added successfully",
                        Data = true,
                    };
                }
                return new ServiceResult<bool>
                {
                    Result = ResultStatus.unHandeledError,
                    Message = "File add failed",
                    Data = false,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public async Task<ServiceResult<bool>> UpdateDocumentOfEmp(UpdateDocumentDTO request)
        {
            var getExisting = await _documentService.GetDocumentOfEmpById(request.Id);
            if (getExisting == null)
            {
                return new ServiceResult<bool>
                {
                    Result = ResultStatus.unHandeledError,
                    Message = "File update failed",
                    Data = false,
                };
            }
            string oldImage = getExisting.ImageName;
            if (request.ImageFile != null)
            {
                if (request.ImageFile.Length > 1 * 1024 * 1024)
                {
                    return new ServiceResult<bool>
                    {
                        Result = ResultStatus.unHandeledError,
                        Message = "File size shouldn't exceed 1MB",
                        Data = false,
                    };
                }
                string[] allowedFileExtentions = { ".jpg", ".jpeg", ".png" };
                string createdImageName = await _fileService.SaveFileAsync(request.ImageFile, allowedFileExtentions);
                //request.ImageName = createdImageName;

                getExisting.EmployeeId = request.EmployeeId;
                getExisting.DocumentType = (DocumentType)request.DocumentType;
                getExisting.ImageName = createdImageName;
            }

            var updatedDcoument = await _documentService.UpdateDocumentOfEmp(getExisting);

            if (request.ImageFile != null)
            {
                _fileService.DeleteFile(oldImage);
            }
            return new ServiceResult<bool>
            {
                Result = ResultStatus.Ok,
                Message = "File updated successfully",
                Data = updatedDcoument,
            };
        }

    }
}

