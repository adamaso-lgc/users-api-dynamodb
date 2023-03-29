using System.Net;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using LetsGetChecked.IlabUsers.Api.Domain.Entities;
using LetsGetChecked.IlabUsers.Api.Settings;
using Microsoft.Extensions.Options;

namespace LetsGetChecked.IlabUsers.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IAmazonDynamoDB _dynamoDb;
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public UserRepository(IAmazonDynamoDB dynamoDb, IOptions<DatabaseSettings> databaseSettings)
    {
        _dynamoDb = dynamoDb;
        _databaseSettings = databaseSettings;
    }

    public async Task<bool> CreateAsync(User user)
    {
        var userAsJson = JsonSerializer.Serialize(user);
        var itemAsDocument = Document.FromJson(userAsJson);
        var itemAsAttributes = itemAsDocument.ToAttributeMap();
        var createItemRequest = new PutItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Item = itemAsAttributes
        };

        var response = await _dynamoDb.PutItemAsync(createItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<User?> GetAsync(Guid id)
    {
        var getItemRequest = new GetItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            },
            ConsistentRead = true
        };

        var response = await _dynamoDb.GetItemAsync(getItemRequest);
        if (response.Item.Count == 0)
        {
            return null;
        }

        var itemAsDocument = Document.FromAttributeMap(response.Item);
        return JsonSerializer.Deserialize<User>(itemAsDocument.ToJson());
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var userAsJson = JsonSerializer.Serialize(user);
        var itemAsDocument = Document.FromJson(userAsJson);
        var itemAsAttributes = itemAsDocument.ToAttributeMap();
        var updateItemRequest = new PutItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Item = itemAsAttributes
        };

        var response = await _dynamoDb.PutItemAsync(updateItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deleteItemRequest = new DeleteItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
        };
        var response = await _dynamoDb.DeleteItemAsync(deleteItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}