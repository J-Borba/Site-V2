using projects_api.Data.Abstractions;
using projects_api.Data.Models.Enums;

namespace projects_api.Data.Models;

public class Ativo : ICurrentUserFinancialEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string Codigo { get; set; } = null!;
    public AtivoTipo Tipo { get; set; }
    public string Setor { get; set; } = null!;
    public string Corretora { get; set; } = null!;
    public decimal Quantidade { get; set; }
    public decimal PrecoMedio { get; set; }
    public decimal? UltimoPreco { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
