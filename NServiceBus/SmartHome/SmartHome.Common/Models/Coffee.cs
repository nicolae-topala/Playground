using System.ComponentModel.DataAnnotations;

namespace SmartHome.Common.Models
{
    public class Coffee
    {
        [Key]
        public Guid CoffeeId { get; set; }
        public bool IsDone { get; set; }
        public Guid CoffeeMachineId { get; set; }
        public virtual CoffeeMachine CoffeeMachine { get; set; }
    }
}
