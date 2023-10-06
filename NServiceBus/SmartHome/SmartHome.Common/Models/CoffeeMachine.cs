using System.ComponentModel.DataAnnotations;

namespace SmartHome.Common.Models
{
    public class CoffeeMachine
    {
        [Key]
        public Guid CoffeeMachineId { get; set; }
        [MaxLength(100)]
        public string CoffeeMachineSerialNr { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Coffee> Coffees { get; set; }
    }
}
