namespace Company.Project.Domain.Enums;

public class Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5
    }

    public enum OrderType
    {
        Online = 1,
        InStore = 2,
        Phone = 3
    }
    public enum MessageType
    {
        Text = 1,
        Image = 2,
        File = 3,
        Video = 4,
        Audio = 5
    }
}