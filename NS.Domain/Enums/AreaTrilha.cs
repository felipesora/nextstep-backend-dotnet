using System.Text.Json.Serialization;

namespace NS.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AreaTrilha
{
    BACKEND,
    WEB,
    DATA_SCIENCE,
    MOBILE,
    DESIGN,
    DEVOPS,
    IA
}
