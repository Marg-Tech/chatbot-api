using ChatbotApi.Models;

namespace ChatbotApi.Services;

public interface IMessageService
{
    Task<Message> SendMessageAsync(Message message);
    Task<IEnumerable<Message>> GetMessagesAsync(int limit = 100);
    Task<Message?> GetMessageByIdAsync(int id);
}

public class MessageService : IMessageService
{
    private readonly List<Message> _messages = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public Task<Message> SendMessageAsync(Message message)
    {
        lock (_lock)
        {
            message.Id = _nextId++;
            message.Timestamp = DateTime.UtcNow;
            _messages.Add(message);
            return Task.FromResult(message);
        }
    }

    public Task<IEnumerable<Message>> GetMessagesAsync(int limit = 100)
    {
        lock (_lock)
        {
            var messages = _messages
                .OrderBy(m => m.Timestamp)
                .TakeLast(limit)
                .ToList();
            return Task.FromResult<IEnumerable<Message>>(messages);
        }
    }

    public Task<Message?> GetMessageByIdAsync(int id)
    {
        lock (_lock)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            return Task.FromResult(message);
        }
    }
}
