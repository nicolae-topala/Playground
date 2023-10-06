namespace SmartHome.Common.Messages
{
    public class MakeCoffee : ICommand
    {
        public Guid CoffeeId { get; set; }
        public Guid CoffeeMachineId { get; set; }
    }
}
