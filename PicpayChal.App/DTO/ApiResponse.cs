using System.Text.Json.Serialization;

namespace PicpayChal.App.DTO;

public record ApiResponse<T>(
    [property: JsonPropertyName("status")] string Status,
    T Data
);
