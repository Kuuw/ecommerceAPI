using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T> { Data = data, Success = true, StatusCode = 200 };
        }

        public static ServiceResult<T> BadRequest(string errorMessage)
        {
            return new ServiceResult<T> { Success = false, ErrorMessage = errorMessage, StatusCode = 400 };
        }

        public static ServiceResult<T> NotFound(string errorMessage)
        {
            return new ServiceResult<T> { Success = false, ErrorMessage = errorMessage, StatusCode = 404 };
        }
    }
}
