namespace ChatbotApi.Models;

public class Message
{
    public int Id { get; set; }
    public string? Sender { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
