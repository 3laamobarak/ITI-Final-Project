using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.ChatMessage;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatHistoryController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public ChatHistoryController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET api/chathistory/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<ChatMessageDto>>> GetChatHistory(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }
            var messages = await _messageService.GetConversationHistoryAsync(userId);
            if (messages == null || messages.Count == 0)
            {
                return NotFound("No chat history found for this user.");
            }
            return Ok(messages);
        }
    }
}
