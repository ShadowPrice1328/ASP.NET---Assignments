using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class StockServiceTests(ITestOutputHelper testOutputHelper)
    {
        private readonly IStocksService _stocksService = new StocksService();
        private readonly ITestOutputHelper _outputHelper = testOutputHelper;

        #region CreateBuyOrder

        [Theory]
        [InlineData(null, "Microsoft Corp.", "2001-01-01", 100, 9000, typeof(ArgumentNullException))] // Null BuyOrderRequest & null StockSymbol
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 0, 9000, typeof(ArgumentException))] // Quantity is too low
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 100001, 9000, typeof(ArgumentException))] // Quantity is too high
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 100, 0, typeof(ArgumentException))] // Price is too low
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 100, 10001, typeof(ArgumentException))] // Price is too high
        [InlineData("MSFT", "Microsoft Corp.", "1999-12-31", 100, 100, typeof(ArgumentException))] // Date is too old
        public void CreateBuyOrder_InvalidParameters_ThrowsException(
            string stockSymbol,
            string stockName,
            string date,
            uint quantity,
            double price,
            Type expectedException)
        {
            // Arrange
            var buyOrderRequest = stockSymbol == null ? null : new BuyOrderRequest
            {
                StockSymbol = stockSymbol,
                StockName = stockName,
                DateAndTimeOfOrder = DateTime.Parse(date),
                Quantity = quantity,
                Price = price
            };

            // Assert
            Assert.Throws(expectedException, () =>
            {
                // Act
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_ValidParameters()
        {
            // Arrange
            BuyOrderRequest? buy_order_request = new()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft Corp.",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Quantity = 100,
                Price = 100
            };

            // Act
            BuyOrderResponse buy_order_reponse_from_add = _stocksService.CreateBuyOrder(buy_order_request);

            // Assert
            Assert.True(buy_order_reponse_from_add.BuyOrderId != Guid.Empty);
        }
        #endregion

        #region CreateSellOrder

        [Theory]
        [InlineData(null, "Microsoft Corp.", "2001-01-01", 100, 9000, typeof(ArgumentNullException))] // Null BuyOrderRequest & null StockSymbol
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 0, 9000, typeof(ArgumentException))] // Quantity is too low
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 100001, 9000, typeof(ArgumentException))] // Quantity is too high
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 100, 0, typeof(ArgumentException))] // Price is too low
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 100, 10001, typeof(ArgumentException))] // Price is too high
        [InlineData("MSFT", "Microsoft Corp.", "1999-12-31", 100, 100, typeof(ArgumentException))] // Date is too old
        public void CreateSellOrder_InvalidParameters_ThrowsException(
            string stockSymbol,
            string stockName,
            string date,
            uint quantity,
            double price,
            Type expectedException)
        {
            // Arrange
            var sellOrderRequest = stockSymbol == null ? null : new SellOrderRequest
            {
                StockSymbol = stockSymbol,
                StockName = stockName,
                DateAndTimeOfOrder = DateTime.Parse(date),
                Quantity = quantity,
                Price = price
            };

            // Assert
            Assert.Throws(expectedException, () =>
            {
                // Act
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_ValidParameters()
        {
            // Arrange
            SellOrderRequest? sell_order_request = new()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft Corp.",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Quantity = 100,
                Price = 100
            };

            // Act
            SellOrderResponse sell_order_reponse_from_add = _stocksService.CreateSellOrder(sell_order_request);

            // Assert
            Assert.True(sell_order_reponse_from_add.SellOrderId != Guid.Empty);
        }

        #endregion

        #region GetBuyOrders

        [Fact]
        public void GetBuyOrders_EmptyList()
        {
            // Act
            List<BuyOrderResponse> buy_orders_responses_from_get = _stocksService.GetBuyOrders();

            // Assert
            Assert.Empty(buy_orders_responses_from_get);
        }

        [Fact]
        public void GetBuyOrders_AddFewBuyOrders()
        {
            // Arrange
            List<BuyOrderResponse> buy_order_responses_from_add = AddFewOrders<BuyOrderRequest, BuyOrderResponse>(_stocksService.CreateBuyOrder);

            // Act
            List<BuyOrderResponse> buy_order_responses_from_get = _stocksService.GetBuyOrders();

            // Write expected values to Console
            _outputHelper.WriteLine("Expected: ");
            foreach (var buy_order_response in buy_order_responses_from_add)
            {
                _outputHelper.WriteLine(buy_order_response.ToString());
            }

            // Assert 
            _outputHelper.WriteLine("Actual: ");
            foreach (var buy_order_response in buy_order_responses_from_get)
            {
                // Write actual values to Console
                _outputHelper.WriteLine(buy_order_response.ToString());

                Assert.Contains(buy_order_response, buy_order_responses_from_get);
            }
        }

        #endregion

        #region GetSellOrders

        [Fact]
        public void GetSellOrders_EmptyList()
        {
            // Act
            List<SellOrderResponse> sell_orders_responses_from_get = _stocksService.GetSellOrders();

            // Assert
            Assert.Empty(sell_orders_responses_from_get);
        }

        [Fact]
        public void GetSellOrders_AddFewSellOrders()
        {
            // Arrange
            List<SellOrderResponse> sell_order_responses_from_add = AddFewOrders<SellOrderRequest, SellOrderResponse>(_stocksService.CreateSellOrder);

            // Act
            List<SellOrderResponse> sell_order_responses_from_get = _stocksService.GetSellOrders();

            // Write expected values to Console
            _outputHelper.WriteLine("Expected: ");
            foreach (var sell_order_response in sell_order_responses_from_add)
            {
                _outputHelper.WriteLine(sell_order_response.ToString());
            }

            // Assert 
            _outputHelper.WriteLine("Actual: ");
            foreach (var sell_order_response in sell_order_responses_from_get)
            {
                // Write actual values to Console
                _outputHelper.WriteLine(sell_order_response.ToString());

                Assert.Contains(sell_order_response, sell_order_responses_from_get);
            }
        }

        #endregion

        private List<TResponse> AddFewOrders<TRequest, TResponse>(Func<TRequest, TResponse> createOrderMethod)
            where TRequest : class, new()
        {
            TRequest order_request_1 = new TRequest();
            SetOrderProperties(order_request_1, "MSFT", "Microsoft Corp.", "2004-08-15", 500, 7000);

            TRequest order_request_2 = new TRequest();
            SetOrderProperties(order_request_2, "MSFT", "Microsoft Corp.", "2001-01-01", 700, 5000);

            List<TResponse> orderResponses = new List<TResponse>
            {
                createOrderMethod(order_request_1),
                createOrderMethod(order_request_2)
            };

            return orderResponses;
        }
        private void SetOrderProperties<TRequest>(TRequest request, string stockSymbol, string stockName, string date, uint quantity, double price)
        {
            request.GetType().GetProperty("StockSymbol")?.SetValue(request, stockSymbol);
            request.GetType().GetProperty("StockName")?.SetValue(request, stockName);
            request.GetType().GetProperty("DateAndTimeOfOrder")?.SetValue(request, DateTime.Parse(date));
            request.GetType().GetProperty("Quantity")?.SetValue(request, quantity);
            request.GetType().GetProperty("Price")?.SetValue(request, price);
        }
    }
}