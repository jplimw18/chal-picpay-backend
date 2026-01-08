using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PicpayChal.App.DTO;

public record class TransactionRequest
{
    [Required]
    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    [Required]
    [JsonPropertyName("payer")]
    public long PayerId { get; set; }

    [Required]
    [JsonPropertyName("payee")]
    public long PayeeId { get; set; }
}
