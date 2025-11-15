using System.Text.Json.Serialization;

namespace NS.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoConteudo
{
    CURSO,
    ARTIGO,
    PODCAST,
    VIDEO,
    DESAFIO
}
