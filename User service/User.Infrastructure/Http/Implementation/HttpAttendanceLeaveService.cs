using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Common;
using User.Application.DTO.Response;
using User.Application.Helpers;
using User.Application.Http.Interface;
using static User.Application.Common.CommonUtilities;
using User.Infrastructure.Http.Url;
using Newtonsoft.Json;
using User.Domain.Entities;
using User.Application.DTO;
using User.Application.DTO.Http;

namespace User.Infrastructure.Http.Implementation
{
    public class HttpAttendanceLeaveService : IHttpAttendanceLeaveService
    {
        public HttpAttendanceLeaveService()
        {

        }
        public async Task<ServiceResult<bool>> AddLeaveBalanceOfEmp(int Id)
        {
            var requestData = new ELeaveBalanceDto
            {
                EmployeeId = Id,
            };

            // Serialize to JSON
            string requestBody = JsonConvert.SerializeObject(requestData);
            var response = await ApiClient.Post(HttpAttendanceLeaveServiceUrl.LeaveBaseUrl, requestBody, HttpAttendanceLeaveServiceUrl.AddLeaveBalanceOfEmp(Id));
            if (response.StatusCode == 0)
            {
                return new ServiceResult<bool> { };

            }
            var result = response.Content;
            var deserizableData = JsonConvert.DeserializeObject<ServiceResult<bool>>(result);
            if (deserizableData != null)
            {
                return deserizableData;
            }
            return new ServiceResult<bool>()
            { };

        }

        public async Task<ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>> GetLeaveRequestByEmpId(int empId)
        {
            var response = await ApiClient.Get(HttpAttendanceLeaveServiceUrl.LeaveBaseUrl, HttpAttendanceLeaveServiceUrl.GetLeaveRequestByEmpId(empId));
            if (response.StatusCode == 0)
            {
                return new ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>> { };
            }
            var result = response.Content;
            var deserializableData = JsonConvert.DeserializeObject<ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>>(result);
            if (deserializableData != null)
            {
                return deserializableData;
            }
            return new ServiceResult<List<GetLeaveRequestListBasedOnEmployeeResponse>>() { };
        }
        public async Task<ServiceResult<List<GetLeaveBalanceOfEmpResponse>>> GetLeaveBalanceofEmp(int empId)
        {
            var response = await ApiClient.Get(HttpAttendanceLeaveServiceUrl.LeaveBaseUrl, HttpAttendanceLeaveServiceUrl.GetLeaveBalanceofEmp(empId));
            if (response.StatusCode == 0)
            {
                return new ServiceResult<List<GetLeaveBalanceOfEmpResponse>> { };
            }
            var result = response.Content;
            var deserializableData = JsonConvert.DeserializeObject<ServiceResult<List<GetLeaveBalanceOfEmpResponse>>>(result);
            if (deserializableData != null)
            {

                return deserializableData;
            }
            return new ServiceResult<List<GetLeaveBalanceOfEmpResponse>> { };
        }
    }
}
