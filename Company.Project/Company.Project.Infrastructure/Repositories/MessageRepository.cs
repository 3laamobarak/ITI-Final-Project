using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.theDbcontext;
using Microsoft.EntityFrameworkCore;

namespace Company.Project.Infrastructure.Repositories
{
    public class MessageRepository : BaseRepository<ChatMessage> , IMessageRepository 
    {
//        private readonly Context _context;

        public MessageRepository(Context context) : base(context)
        {
//            _context = context;
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesForUserAsync(string userId, string adminId = "admin")
        {
            return await _dbContext.ChatMessages
                .Where(m => (m.SenderId == userId && m.ReceiverId == adminId) || (m.SenderId == adminId && m.ReceiverId == userId))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            var message = await _dbContext.ChatMessages.FindAsync(messageId);
            if (message != null)
            {
                message.IsRead = true;
                await _dbContext.SaveChangesAsync();
            }
        }      
    }
}
