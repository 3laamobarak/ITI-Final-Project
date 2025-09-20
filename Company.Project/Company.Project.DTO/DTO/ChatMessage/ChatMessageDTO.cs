namespace Company.Project.DTO.DTO.ChatMessage
{
    public class ChatMessageDto
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public bool IsFromAdmin { get; set; }
    }
}