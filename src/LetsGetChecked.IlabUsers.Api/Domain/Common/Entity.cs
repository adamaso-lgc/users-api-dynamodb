using Amazon.DynamoDBv2.DataModel;

namespace LetsGetChecked.IlabUsers.Api.Domain.Common;

public abstract class Entity
{
    [DynamoDBHashKey]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [DynamoDBProperty]
    public DateTime CreatedDate { get; init; } = DateTime.Now;
    
    [DynamoDBIgnore]
    public DateTime? LastModifiedDate { get; set; }
}