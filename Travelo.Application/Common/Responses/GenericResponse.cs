using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelo.Application.Common.Responses
{
    public class GenericResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static GenericResponse<T> SuccessResponse(T data, string message = "Success")
            => new GenericResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };

        public static GenericResponse<T> FailureResponse(string message)
            => new GenericResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
    }
}
