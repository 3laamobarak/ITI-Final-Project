using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.theDbcontext;

namespace Company.Project.Infrastructure.Repositories
{
    public class MessageRepository :IMessageRepository
    {
        private readonly Context _context;

        public MessageRepository(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(ChatMessage message)
        {
            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
        public async Task<List<ChatMessage>> GetMessagesByUserIdAsync(string userId)
        {
            return await Task.FromResult(_context.ChatMessages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .OrderBy(m => m.Timestamp)
                .ToList());
        }

        // Other methods for retrieving messages can be implemented here
        
    }
}
