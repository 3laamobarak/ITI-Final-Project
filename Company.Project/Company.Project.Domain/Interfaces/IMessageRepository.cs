using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task AddAsync(ChatMessage message);
        // chat history 
        Task<List<ChatMessage>> GetMessagesByUserIdAsync(string userId);
        // Other methods for retrieving messages can be added here
    }
}
