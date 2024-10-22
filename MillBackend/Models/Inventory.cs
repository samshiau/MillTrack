using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillBackend.Models
{
    public class InventoryItem
    {
        [Key]
        public int ProductId { get; set; } // Primary Key

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public required string SKU { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public required string Category { get; set; }

        [MaxLength(100)]
        public required string Location { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        public required int AmountInStock { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? ExpirationDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Weight { get; set; }

 
    }
}
