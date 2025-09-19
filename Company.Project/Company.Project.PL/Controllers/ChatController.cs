using System.Security.Claims;
using Company.Project.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatBotMessageService _chatBotMessageService;

        public ChatController(IChatBotMessageService chatBotMessageService)
        {
            _chatBotMessageService = chatBotMessageService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var (userMsg, botMsg) = await _chatBotMessageService.SendMessageAsync(userId, message);

            return Ok(new
            {
                user = userMsg.Message,
                bot = botMsg.Message
            });
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var messages = await _chatBotMessageService.GetMessagesByUserAsync(userId);
            return Ok(messages);
        }

        [HttpPost("product-query")]
        public async Task<IActionResult> ProductQuery([FromBody] string query)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            // This will use the product filtering logic in the service
            var (userMsg, botMsg) = await _chatBotMessageService.SendMessageAsync(userId, query);

            return Ok(new
            {
                user = userMsg.Message,
                bot = botMsg.Message
            });
        }
    }
}
