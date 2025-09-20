using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.ChatMessage;
using Company.Project.theDbcontext;
using Microsoft.AspNetCore.SignalR;
namespace Company.Project.PL.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessageToAdmin(string userId, string messageContent)
        {
            var chatMessageDto = new ChatMessageDto
            {
                SenderId = userId,
                ReceiverId = "admin",
                Content = messageContent,
                IsFromAdmin = false
            };
            
            await _messageService.SendMessageAsync(chatMessageDto);

            // Send the message to all clients in the "Admins" group.
            await Clients.Group("admins").SendAsync("ReceiveMessage", userId, messageContent);
        }

        public async Task SendReplyToUser(string adminId, string messageContent, string targetUserId)
        {
            var chatMessageDto = new ChatMessageDto
            {
                SenderId = adminId,
                ReceiverId = targetUserId,
                Content = messageContent,
                IsFromAdmin = true
            };
            
            await _messageService.SendMessageAsync(chatMessageDto);

            // Send the reply to the specific user.
            await Clients.User(targetUserId).SendAsync("ReceiveMessage", adminId, messageContent);
        }

        public override async Task OnConnectedAsync()
        {
            // For a production app, you would use a proper authentication mechanism to identify the admin.
            // For this example, we assume a simple user identifier.
            if (Context.UserIdentifier == "adminUserId") // Replace with your logic for identifying an admin
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
            }
            await base.OnConnectedAsync();
        }
    }
}
