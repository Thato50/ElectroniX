using System.ComponentModel.DataAnnotations.Schema;
using ElectroniX.Models.Enums;

namespace ElectroniX.Models
{
    public class Products
    {
        public int Id { get; set; }

        // Basic Info
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DeviceType DeviceType { get; set; }

        // Pricing
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public bool IsUsed { get; set; }

        // Quality & Warranty
        public string Grade { get; set; } = "A"; // A / B / C
        public int WarrantyMonths { get; set; }

        // Stock
        public int Stock { get; set; }

        // Recommendation Logic
        public UsageType RecommendedUsage { get; set; }
        public StudyCourse? RecommendedCourse { get; set; } // Nullable (only for Study)

        // Media
        public string ImageUrl { get; set; } = null!;
    }
}
