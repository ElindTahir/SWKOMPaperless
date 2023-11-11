using System.Text.Json.Serialization;

namespace NPaperless.WebUI.Models
{
    public class Login
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}