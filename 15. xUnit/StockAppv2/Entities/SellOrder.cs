using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Domain model for SellOrder
    /// </summary>
    public class SellOrder
    {
        public Guid? SellOrderId { get; set; }
        [Required(ErrorMessage = "StockSymbol cannot be blank")]
        public string? StockSymbol {  get; set; }
        [Required(ErrorMessage = "StockName cannot be blank")]
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000)]
        public uint Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }
    }
}
