using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory_PushService.Data.Entities
{
    public class User
    {
        [Key] [MaxLength(32)] [Required] public string Username { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
