namespace Company.Project.DTO.DTO.OrderItem
{
    public class UpdateOrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
