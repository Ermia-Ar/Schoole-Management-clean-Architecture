﻿using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class TokenRequest
    {
        [JsonPropertyName("userId")]
        public string EmailOrName { get; set; }

        [JsonPropertyName("roles")]
        public IList<string> Roles { get; set; }
    }
}