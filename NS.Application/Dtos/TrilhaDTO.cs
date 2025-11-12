using NS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NS.Application.Dtos;

public class TrilhaDTO
{
    [Required]
    public string Nome { get; set; }

    [Required]
    public string Descricao { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AreaTrilha Area { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NivelTrilha Nivel { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusTrilha Status { get; set; }
}
