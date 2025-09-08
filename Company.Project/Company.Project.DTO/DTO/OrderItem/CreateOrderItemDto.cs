namespace Company.Project.DTO.DTO.OrderItem
{
    public class CreateOrderItemDto
    {
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
