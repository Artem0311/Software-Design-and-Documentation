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
        // 1. Создаем пользователя
        var userResponse = await _client.PostAsJsonAsync("/users", new { name = "TestUser" });
        userResponse.EnsureSuccessStatusCode();
        var user = await userResponse.Content.ReadFromJsonAsync<User>();
        Assert.NotNull(user);

        // 2. Отправляем сообщение
        var msgResponse = await _client.PostAsJsonAsync("/messages", new 
        { 
            conversationId = "test-chat", 
            senderId = user.Id, 
            text = "Integration test message" 
        });
        msgResponse.EnsureSuccessStatusCode();

        // 3. Получаем историю
        var historyResponse = await _client.GetAsync("/conversations/test-chat/messages");
        historyResponse.EnsureSuccessStatusCode();
        var history = await historyResponse.Content.ReadFromJsonAsync<List<Message>>();

        // 4. Проверяем статус (Твой вариант!)
        Assert.NotNull(history);
        Assert.Single(history);
        Assert.Equal("Integration test message", history[0].Text);
        Assert.Equal("sent", history[0].Status); 
    }
}