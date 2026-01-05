using System.Text.Json.Serialization;

namespace PicpayChal.App;

public record AuthData(
    [property: JsonPropertyName("Authorization")] bool IsAuthorized
);
