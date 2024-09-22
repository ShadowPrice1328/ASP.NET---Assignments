using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders = new();
        private readonly List<SellOrder> _sellOrders = new();
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? request)
        {
            ArgumentNullException.ThrowIfNull(request);

            ValidationHelper.ModelValidation(request);

            BuyOrder buyOrder = request.ToBuyOrder();

            return buyOrder.ToBuyOrderResponse();
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? request)
        {
            ArgumentNullException.ThrowIfNull(request);

            ValidationHelper.ModelValidation(request);

            SellOrder sellOrder = request.ToSellOrder();

            return sellOrder.ToSellOrderResponse();
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            return _buyOrders.Select(o => o.ToBuyOrderResponse()).ToList();
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            return _sellOrders.Select(o => o.ToSellOrderResponse()).ToList();
        }
    }
}
