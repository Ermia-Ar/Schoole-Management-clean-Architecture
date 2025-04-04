﻿using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class TokenConfirmationResponse
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

    }
}