using System.Text.Json;
using lab2.Models;

namespace lab2.Storage;

public class JsonStorage
{
    private readonly string _usersFile = "users.json";
    private readonly string _messagesFile = "messages.json";

    public List<User> Users { get; set; } = new();
    public List<Message> Messages { get; set; } = new();

    public JsonStorage()
    {
        if (File.Exists(_usersFile))
        {
            var usersJson = File.ReadAllText(_usersFile);
            Users = JsonSerializer.Deserialize<List<User>>(usersJson) ?? new();
        }
        if (File.Exists(_messagesFile))
        {
            var messagesJson = File.ReadAllText(_messagesFile);
            Messages = JsonSerializer.Deserialize<List<Message>>(messagesJson) ?? new();
        }
    }

    public void SaveData()
    {
        File.WriteAllText(_usersFile, JsonSerializer.Serialize(Users));
        File.WriteAllText(_messagesFile, JsonSerializer.Serialize(Messages));
    }

    public bool UpdateMessageStatus(string messageId, string newStatus)
    {
        var msg = Messages.FirstOrDefault(m => m.MessageId == messageId);
        
        if (msg == null) return false; 
        msg.Status = newStatus; 
        SaveData();
        return true;
    }

    public bool DeleteMessage(string messageId)
    {
        var msg = Messages.FirstOrDefault(m => m.MessageId == messageId);
        
        if (msg == null) return false; 

        Messages.Remove(msg); 
        SaveData(); 
        return true;
    }
}