using System.ComponentModel.DataAnnotations;

namespace Inventory_PushService.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }
        [MaxLength(32)] [Required] public string Name { get; set; }

    }
}
