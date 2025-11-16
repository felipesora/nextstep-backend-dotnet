using System.Text.Json.Serialization;

namespace NS.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HorasEstudo
{
    ATE_5H,
    DE_6_A_10H,
    DE_11_A_15H,
    MAIS_DE_15H
}
