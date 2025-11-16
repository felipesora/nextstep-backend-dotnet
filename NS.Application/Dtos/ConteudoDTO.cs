using NS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NS.Application.Dtos;

public class ConteudoDTO
{
    [Required]
    public string Titulo { get; set; }

    [Required]
    public string Descricao { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoConteudo Tipo { get; set; }

    public string? Link { get; set; }

    [Required]
    public long IdTrilha { get; set; }
}
