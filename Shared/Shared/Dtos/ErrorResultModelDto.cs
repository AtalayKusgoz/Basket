using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ErrorResultModelDto
    {
        public int StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        public ErrorResultModelDto(string message, int statusCode = 500)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
