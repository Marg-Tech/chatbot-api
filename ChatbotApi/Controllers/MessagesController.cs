using ChatbotApi.Models;
using ChatbotApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatbotApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(IMessageService messageService, ILogger<MessagesController> logger)
    {
        _messageService = messageService;
        _logger = logger;
    }

    /// <summary>
    /// Send a new message to the chatbot
    /// </summary>
    /// <param name="message">The message to send</param>
    /// <returns>The created message with ID and timestamp</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Message>> SendMessage([FromBody] Message message)
    {
        if (string.IsNullOrWhiteSpace(message.Content))
        {
            return BadRequest("Message content cannot be empty");
        }

        _logger.LogInformation("Sending message from {Sender}: {Content}", 
            message.Sender ?? "Anonymous", message.Content);

        var createdMessage = await _messageService.SendMessageAsync(message);
        return CreatedAtAction(nameof(GetMessageById), new { id = createdMessage.Id }, createdMessage);
    }

    /// <summary>
    /// Get all messages (most recent first)
    /// </summary>
    /// <param name="limit">Maximum number of messages to retrieve (default: 100)</param>
    /// <returns>List of messages</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Message>>> GetMessages([FromQuery] int limit = 100)
    {
        if (limit <= 0 || limit > 1000)
        {
            return BadRequest("Limit must be between 1 and 1000");
        }

        _logger.LogInformation("Getting messages with limit {Limit}", limit);
        var messages = await _messageService.GetMessagesAsync(limit);
        return Ok(messages);
    }

    /// <summary>
    /// Get a specific message by ID
    /// </summary>
    /// <param name="id">The message ID</param>
    /// <returns>The message if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Message>> GetMessageById(int id)
    {
        _logger.LogInformation("Getting message with ID {Id}", id);
        var message = await _messageService.GetMessageByIdAsync(id);
        
        if (message == null)
        {
            return NotFound($"Message with ID {id} not found");
        }

        return Ok(message);
    }
}
