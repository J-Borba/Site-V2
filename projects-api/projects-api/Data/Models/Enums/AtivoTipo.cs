using System.Text.Json.Serialization;

namespace projects_api.Data.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AtivoTipo
{
    Ação,
    FII,
    ETF,
    Tesouro,
    CDB,
    Cripto
}
