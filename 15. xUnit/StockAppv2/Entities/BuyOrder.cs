using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Domain model for BuyOrder
    /// </summary>
    public class BuyOrder
    {
        public Guid? BuyOrderId { get; set; }
        [Required(ErrorMessage = "StockSymbol cannot be blank")]
        public string? StockSymbol { get; set; }
        [Required(ErrorMessage = "StockName cannot be blank")]
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000)]
        public uint Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }
    }
}
