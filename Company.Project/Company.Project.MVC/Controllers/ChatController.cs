using AutoMapper;
using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.ChatMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class ChatController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public ChatController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var messages = await _messageService.GetMessagesForUserAsync(userId);
                ViewBag.Messages = _mapper.Map<IEnumerable<MessageDto>>(messages);
                ViewBag.UserId = userId;
            }
            // Add logic to list users with active chats if needed
            return View();
        }
    }
}