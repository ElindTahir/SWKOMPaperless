using System.Text.Json.Serialization;

namespace NPaperless.WebUI.Models;

public class AckTasksRequest
{
    [JsonPropertyName("tasks")]
    public IEnumerable<int> Tasks { get; set; }
}