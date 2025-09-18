using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;

namespace Company.Project.Application.Contracts
{
    public interface IChatBotMessageService
    {

        Task<(ChatBotMessages userMsg, ChatBotMessages botMsg)> SendMessageAsync(string userId, string message);
        Task<IEnumerable<ChatBotMessages>> GetMessagesByUserAsync(string userId);
         Task<IEnumerable<ChatBotMessages>> GetAllMessagesAsync();


    }
}
