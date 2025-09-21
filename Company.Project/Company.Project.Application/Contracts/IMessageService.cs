using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.ChatMessage;

namespace Company.Project.Application.Contracts
{
    public interface IMessageService
    {
        Task SendMessageAsync(ChatMessage message);
        Task<IEnumerable<ChatMessage>> GetMessagesForUserAsync(string userId);
        Task MarkAsReadAsync(int messageId);        
    }
}
