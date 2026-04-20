namespace projects_api.Common.Validation;

public class ValidationResult
{
    public bool IsValid => ErrorMessages.Count == 0;
    public List<string> ErrorMessages { get; } = [];

    public void AddError(string message) => ErrorMessages.Add(message);
    public void AddErrors(IEnumerable<string> messages) => ErrorMessages.AddRange(messages);
}
