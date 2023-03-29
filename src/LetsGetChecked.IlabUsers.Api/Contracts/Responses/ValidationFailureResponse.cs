namespace LetsGetChecked.IlabUsers.Api.Contracts.Responses;

public class ValidationFailureResponse
{
    public IEnumerable<ValidationResponse> Errors { get; init; } 
}

public class ValidationResponse
{
    public string PropertyName { get; set; }
    public string Message { get; set; }
}
