namespace projects_api.Data.Abstractions;

public interface ICurrentUserFinancialEntity
{
    Guid Id { get; set; }
    string UserId { get; set; }
}
