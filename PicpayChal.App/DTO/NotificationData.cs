using System.Text.Json.Serialization;

namespace PicpayChal.App.DTO;

public record NotificationData([property: JsonPropertyName("message")] bool Message);