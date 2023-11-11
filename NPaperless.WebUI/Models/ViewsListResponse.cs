using System.Text.Json.Serialization;

namespace NPaperless.WebUI.Models;

public partial class ViewsListResponse : ListResponse<SavedView>
{
    [JsonPropertyName("all")]
    public int[] All { get; set; }
}