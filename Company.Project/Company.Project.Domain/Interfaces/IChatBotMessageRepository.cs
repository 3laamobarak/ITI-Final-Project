using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IChatBotMessageRepository : IBaseRepository<ChatBotMessages>
    {
        Task<IEnumerable<ChatBotMessages>> GetUserMessagesAsync(string userId);
    }

}
