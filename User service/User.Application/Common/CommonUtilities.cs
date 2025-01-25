using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Enum;

namespace User.Application.Common
{
    public static class CommonUtilities
    {
        public class ServiceResult<t>
        {
            public ResultStatus Result { get; set; }
            public string Message { get; set; }
            public t Data { get; set; }

            public ServiceResult()
            {
                Result = Result;
                Message = Message;
                Data = Data;
            }

        }
        public static class ResponseMessage
        {
            public static string AddedSuccessfully { get; set; } = "Added Successfully";
            public static string DeletedSuccessfully { get; set; } = "Deleted Successfully";
            public static string FetchedSuccessfully { get; set; } = "Fetched Successfully";
            public static string UpdatedSuccessfully { get; set; } = "Updated Successfully";
            public static string AddFailed { get; set; } = "Failed to add";
            public static string DeleteFailed { get; set; } = "Failed to delete";
            public static string FetchedFailed { get; set; } = "Failed to fetch";
            public static string UpdateFailed { get; set; } = "Failed to update";
            public static string IdNotFound { get; set; } = "Id Not Found";
        }
        public static string HashPassword(string password)
        {
            // Generate a salt
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Combine the password and salt, then hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combine the salt and hash
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert to a base64 string
            return Convert.ToBase64String(hashBytes);
        }
        public static bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Extract the bytes
            byte[] hashBytes = Convert.FromBase64String(storedPasswordHash);

            // Get the salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Hash the entered password with the extracted salt
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Compare the results
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }
    }
}
