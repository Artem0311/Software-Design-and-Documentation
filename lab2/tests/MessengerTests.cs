using System.Net.Http.Json;
using lab2.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace tests;

public class MessengerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public MessengerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task SystemFlow_CreateUser_SendMessage_GetHistory()
    {
        var userResponse = await _client.PostAsJsonAsync("/users", new { name = "TestUser" });
        userResponse.EnsureSuccessStatusCode();
        var user = await userResponse.Content.ReadFromJsonAsync<User>();
        Assert.NotNull(user);

        var msgResponse = await _client.PostAsJsonAsync("/messages", new 
        { 
            conversationId = "test-chat", 
            senderId = user.Id, 
            text = "Integration test message" 
        });
        msgResponse.EnsureSuccessStatusCode();

        var historyResponse = await _client.GetAsync("/conversations/test-chat/messages");
        historyResponse.EnsureSuccessStatusCode();
        var history = await historyResponse.Content.ReadFromJsonAsync<List<Message>>();

        Assert.NotNull(history);
        Assert.Single(history);
        Assert.Equal("Integration test message", history[0].Text);
        Assert.Equal("sent", history[0].Status); 
    }
}
