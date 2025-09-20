using AutoMapper;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.ChatMessage;

namespace Company.Project.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task SendMessageAsync(ChatMessage message)
        {
            await _unitOfWork.ChatMessageRepository.AddAsync(message);
            await _unitOfWork.Completeasync();
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesForUserAsync(string userId)
        {
            return await _unitOfWork.ChatMessageRepository.GetMessagesForUserAsync(userId);
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            await _unitOfWork.ChatMessageRepository.MarkAsReadAsync(messageId);
        }

        #region old

        // public async Task SendMessageAsync(ChatMessageDto chatMessageDto)
        // {
        //     var chatMessage = new ChatMessage
        //     {
        //         SenderId = chatMessageDto.SenderId,
        //         ReceiverId = chatMessageDto.ReceiverId,
        //         Content = chatMessageDto.Content,
        //         IsFromAdmin = chatMessageDto.IsFromAdmin,
        //         Timestamp = DateTime.UtcNow
        //     };
        //
        //     await _messageRepository.AddAsync(chatMessage);
        // }
        // public async Task SaveMessageAsync(ChatMessageDto chatMessageDto)
        // {
        //     var chatMessage = new ChatMessage
        //     {
        //         SenderId = chatMessageDto.SenderId,
        //         ReceiverId = chatMessageDto.ReceiverId,
        //         Content = chatMessageDto.Content,
        //         IsFromAdmin = chatMessageDto.IsFromAdmin,
        //         Timestamp = DateTime.UtcNow
        //     };
        //
        //     await _messageRepository.AddAsync(chatMessage);
        // }
        // public async Task<List<ChatMessageDto>> GetConversationHistoryAsync(string userId)
        // {
        //     var messages = await _messageRepository.GetMessagesByUserIdAsync(userId);
        //     return messages.Select(m => new ChatMessageDto
        //     {
        //         SenderId = m.SenderId,
        //         ReceiverId = m.ReceiverId,
        //         Content = m.Content,
        //         IsFromAdmin = m.IsFromAdmin,
        //         
        //     }).ToList();
        //     
        //     
        //     
        // }

        #endregion
        
        
    }
}
