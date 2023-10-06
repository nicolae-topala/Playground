namespace SmartHome.Common.Messages
{
    public class CoffeeMade : IEvent
    {
        public Guid CoffeeId { get; set; }
    }
}
