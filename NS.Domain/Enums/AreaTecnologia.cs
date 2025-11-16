using System.Text.Json.Serialization;

namespace NS.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AreaTecnologia
{
    FRONTEND,
    BACKEND,
    MOBILE,
    CLOUD,
    DADOS,
    CIBER,
    DESIGN
}
