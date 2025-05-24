using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string? Email { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
