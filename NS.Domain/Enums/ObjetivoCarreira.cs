using System.Text.Json.Serialization;

namespace NS.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ObjetivoCarreira
{
    PRIMEIRO_EMPREGO,
    MUDAR_CARREIRA,
    CRESCER_AREA,
    APRENDER
}
