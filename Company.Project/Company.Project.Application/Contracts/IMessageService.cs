using Company.Project.DTO.DTO.ChatMessage;

namespace Company.Project.Application.Contracts
{
    public interface IMessageService
    {
        Task SendMessageAsync(ChatMessageDto chatMessageDto);
        Task SaveMessageAsync(ChatMessageDto chatMessageDto);
        Task<List<ChatMessageDto>> GetConversationHistoryAsync(string userId);
        
    }
}
