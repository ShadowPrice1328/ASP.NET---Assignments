using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {
        public Guid? BuyOrderId { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public override string ToString()
        {
            return $"{nameof(BuyOrderId)}: {BuyOrderId}\n" +
                $"{nameof(StockSymbol)}: {StockSymbol}\n" +
                $"{nameof(StockName)}: {StockName}\n" +
                $"{nameof(DateAndTimeOfOrder)}: {DateAndTimeOfOrder}\n" +
                $"{nameof(Quantity)}: {Quantity}\n" +
                $"{nameof(Price)}: {Price}\n" +
                $"{nameof(TradeAmount)}: {TradeAmount}\n";
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(BuyOrderResponse)) return false;

            BuyOrderResponse buyOrderResponse = (BuyOrderResponse)obj;

            return buyOrderResponse.BuyOrderId == BuyOrderId &&
                buyOrderResponse.StockSymbol == StockSymbol &&
                buyOrderResponse.DateAndTimeOfOrder == DateAndTimeOfOrder &&
                buyOrderResponse.Quantity == Quantity &&
                buyOrderResponse.Price == Price &&
                buyOrderResponse.TradeAmount == TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    
    public static class BuyOrderExtentions
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse()
            {
                BuyOrderId = buyOrder.BuyOrderId,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price,
                TradeAmount = buyOrder.Price * buyOrder.Quantity
            };
        }
    }
}
