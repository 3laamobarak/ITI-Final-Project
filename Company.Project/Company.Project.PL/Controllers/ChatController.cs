using AutoMapper;
using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.ChatMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public ChatController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages()
        {
            var userId = User.Identity.Name;
            var messages = await _messageService.GetMessagesForUserAsync(userId);
            return Ok(_mapper.Map<IEnumerable<MessageDto>>(messages));
        }
    }
}
