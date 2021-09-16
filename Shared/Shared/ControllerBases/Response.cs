using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ControllerBases
{
    public class Response<T>
    {
        public T Data { get; set; }

        public int StatusCode { get; set; }

        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

        public int Count { get; set; }

        public static Response<T> Success(T data, int statusCode, int count = 1)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true, Count = count };
        }

        public static Response<T> Fail(List<string> errors, int statusCode, int count = 0)

        {
            return new Response<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessful = false,
                Count = count
            };
        }

        public static Response<T> Fail(string error, int statusCode, int count = 0)
        {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessful = false, Count = count };
        }
    }
}
