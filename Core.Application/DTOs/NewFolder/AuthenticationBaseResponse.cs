using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Application.DTOs.NewFolder
{
    public class AuthenticationBaseResponse
    {
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }
    }
}
