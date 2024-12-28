using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.Common.Models
{
    // PlayBox.Application/Common/Models/ServiceResponse.cs
    // PlayBox.Application/Common/Models/ServiceResponse.cs
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ServiceResponse()
        {
            Success = true;
        }

        public ServiceResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }

        public ServiceResponse() : base()
        {
        }

        public ServiceResponse(bool success, string message, T data = default) : base(success, message)
        {
            Data = data;
        }
    }
}

