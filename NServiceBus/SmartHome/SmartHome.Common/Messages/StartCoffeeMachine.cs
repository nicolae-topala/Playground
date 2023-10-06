namespace SmartHome.Common.Messages
{
    public class StartCoffeeMachine : ICommand
    {
        public Guid CoffeeId { get; set; }
    }
}
