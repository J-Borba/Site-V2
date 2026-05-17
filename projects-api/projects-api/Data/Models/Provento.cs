using projects_api.Data.Abstractions;
using projects_api.Data.Models.Enums;

namespace projects_api.Data.Models;

public class Provento : ICurrentUserFinancialEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public DateOnly Data { get; set; }
    public string Ticker { get; set; } = null!;
    public ProventoTipo Tipo { get; set; }
    public decimal ValorLiquido { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
