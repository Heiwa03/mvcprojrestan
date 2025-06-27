using System.ComponentModel.DataAnnotations;

namespace BigHauling.Domain.Entities
{
    public class Truck
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;
        
        [Range(1900, 2030)]
        public int Year { get; set; }
        
        [StringLength(20)]
        public string? EngineType { get; set; }
        
        [StringLength(20)]
        public string? Transmission { get; set; }
        
        [Range(0, double.MaxValue)]
        public double? Mileage { get; set; }
        
        [StringLength(20)]
        public string? FuelType { get; set; }
        
        [StringLength(20)]
        public string? Condition { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 