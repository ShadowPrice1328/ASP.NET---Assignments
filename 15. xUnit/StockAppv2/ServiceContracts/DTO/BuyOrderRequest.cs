using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.CustomValidators;

namespace ServiceContracts.DTO
{
    public class BuyOrderRequest
    {
        [Required(ErrorMessage = "StockSymbol cannot be blank")]
        public string? StockSymbol { get; set; }

        [Required(ErrorMessage = "StockName cannot be blank")]
        public string? StockName { get; set; }

        [MaximumDateValidator("2000-01-01")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1, 10000)]
        public double Price { get; set; }
    }
}
