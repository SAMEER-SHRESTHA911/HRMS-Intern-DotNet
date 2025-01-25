using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Application.DTO.Response
{
    public class GetRegisterEmployeeListResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string MobileNo { get; set; }
        public int AddressId { get; set; }
        public string Email { get; set; }
        public string CitizenshipNo { get; set; }
        public string DOB { get; set; }
        public int DepartmentId { get; set; }
        public string Role { get; set; }
        public int Gender { get; set; }
        public string Nationality { get; set; }
        public string StartDate { get; set; }
    }
    public class GetRegisterEmployeeListWithDetailsResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string MobileNo { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string Email { get; set; }
        public string CitizenshipNo { get; set; }
        public string DOB { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public int GenderId { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string StartDate { get; set; }

    }
    public class GetRegisterEmployeeDetailByIdResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string MobileNo { get; set; }
        public int AddressId { get; set; }
        public string Email { get; set; }
        public string CitizenshipNo { get; set; }
        public string DOB { get; set; }
        public int DepartmentId { get; set; }
        public string Role { get; set; }
        public int Gender { get; set; }
        public string Nationality { get; set; }
        public string StartDate { get; set; }

    }
    public class GetRegisterEmployeeDetailAloongWithOtherDetailsByIdResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string MobileNo { get; set; }
        public int AddressId { get; set; }
        public Address Address {  get; set; }
        public string AddressName {  get; set; }
        public string Email { get; set; }
        public string CitizenshipNo { get; set; }
        public string DOB { get; set; }
        public int DepartmentId { get; set; }
        public string Role { get; set; }
        public int Gender { get; set; }
        public string Nationality { get; set; }
        public string StartDate { get; set; }

    }

}
