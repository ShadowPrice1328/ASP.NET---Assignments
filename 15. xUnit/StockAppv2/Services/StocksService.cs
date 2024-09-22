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
        private readonly List<BuyOrder> _orders;
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? request)
        {
            ArgumentNullException.ThrowIfNull(request);

            ValidationHelper.ModelValidation(request);

            BuyOrder buyOrder = request.ToBuyOrder();

            return buyOrder.ToBuyOrderResponse();
        }

        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? request)
        {
            throw new NotImplementedException();
        }

        public Task<List<BuyOrderResponse>?> GetBuyOrders()
        {
            throw new NotImplementedException();
        }

        public Task<List<SellOrderResponse>?> GetSellOrders()
        {
            throw new NotImplementedException();
        }
    }
}
