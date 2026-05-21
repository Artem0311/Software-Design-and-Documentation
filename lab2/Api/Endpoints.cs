using lab2.Models;
using lab2.Storage;

namespace lab2.Api;

public static class Endpoints
{
    public static void MapMessageEndpoints(this WebApplication app)
    {
        app.MapPost("/users", (User newUser, JsonStorage storage) =>
        {
            if (string.IsNullOrWhiteSpace(newUser.Name))
                return Results.BadRequest("User name cannot be empty.");

            storage.Users.Add(newUser);
            storage.SaveData(); 
            return Results.Created($"/users/{newUser.Id}", newUser);
        });

        app.MapPost("/messages", (Message newMessage, JsonStorage storage) =>
        {
            if (string.IsNullOrWhiteSpace(newMessage.SenderId) || string.IsNullOrWhiteSpace(newMessage.Text))
                return Results.BadRequest("SenderId and Text are required.");

            if (!storage.Users.Any(u => u.Id == newMessage.SenderId))
                return Results.NotFound("Sender not found.");

            storage.Messages.Add(newMessage);
            storage.SaveData(); 
            return Results.Created($"/messages/{newMessage.MessageId}", newMessage);
        });

        app.MapGet("/conversations/{conversationId}/messages", (string conversationId, JsonStorage storage) =>
        {
            var history = storage.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderBy(m => m.CreatedAt)
                .ToList();
                
            return Results.Ok(history);
        });

        app.MapPatch("/messages/{id}/status", (string id, string newStatus, JsonStorage storage) =>
        {
            if (string.IsNullOrWhiteSpace(newStatus))
            {
                return Results.BadRequest(new { error = "Status cannot be empty" });
            }

            var success = storage.UpdateMessageStatus(id, newStatus);
            if (!success) return Results.NotFound(new { error = "Message not found" });
            
            return Results.Ok(new { message = "Status successfully updated", newStatus });
        });

        app.MapDelete("/messages/{id}", (string id, JsonStorage storage) =>
        {
            var success = storage.DeleteMessage(id);
            if (!success) return Results.NotFound(new { error = "Message not found" });
            
            return Results.Ok(new { message = "Message successfully deleted" });
        });
    }
}
