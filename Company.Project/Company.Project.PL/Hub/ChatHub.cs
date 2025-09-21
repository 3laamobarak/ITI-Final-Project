using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Models;

namespace Company.Project.PL.Hub
{
    [Authorize]
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessage(string user,string message)
        {
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
            // send to all admins roles users
            await Clients.Group("admins").SendAsync("ReceiveMessage", user, message);
            // store in data bse
            var msg = new ChatMessage
            {
                SenderId = user,
                ReceiverId = "admin",
                Content = message,
                Timestamp = DateTime.UtcNow
            };
            await _messageService.SendMessageAsync(msg);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.Identity.Name;
            var isAdmin = Context.User.IsInRole("admin");

            if (isAdmin)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "admins");
            }

            await base.OnConnectedAsync();
        }

        public async Task SendMessageToAdmin(string userId, string message)
        {
            var msg = new ChatMessage
            {
                SenderId = userId,
                ReceiverId = "admin",
                Content = message,
                Timestamp = DateTime.UtcNow
            };
            await _messageService.SendMessageAsync(msg);
            await Clients.Group("admins").SendAsync("ReceiveMessage", userId, message, msg.Timestamp);
        }

        public async Task SendMessageToUser(string userId, string message)
        {
            if (!Context.User.IsInRole("admin")) return;
            var msg = new ChatMessage
            {
                SenderId = "admin",
                ReceiverId = userId,
                Content = message,
                Timestamp = DateTime.UtcNow
            };
            await _messageService.SendMessageAsync(msg);
            await Clients.User(userId).SendAsync("ReceiveMessage", "admin", message, msg.Timestamp);
        }

        public async Task MarkMessageAsRead(int messageId)
        {
            await _messageService.MarkAsReadAsync(messageId);
        }
    }
}