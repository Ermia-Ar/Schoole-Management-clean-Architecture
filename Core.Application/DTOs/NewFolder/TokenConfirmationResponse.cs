using System.Text.Json.Serialization;

namespace Core.Application.DTOs.NewFolder
{
    public class TokenConfirmationResponse
    {
        [JsonPropertyName("آیدی")]
        public string UserId { get; set; }

        [JsonPropertyName("توکن")]
        public string Token { get; set; }

    }
}