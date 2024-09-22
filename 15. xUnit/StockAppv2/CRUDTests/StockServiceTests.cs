using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class StockServiceTests()
    {
        private readonly IStocksService _stocksService = new StocksService();

        #region CreateBuyOrder

        [Theory]
        [InlineData(null, "Microsoft Corp.", "2001-01-01", 100, 9000, typeof(ArgumentNullException))] // Null BuyOrderRequest & null StockSymbol
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 0, 9000, typeof(ArgumentException))] // Quantity is too low
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 100001, 9000, typeof(ArgumentException))] // Quantity is too high
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 100, 0, typeof(ArgumentException))] // Price is too low
        [InlineData("MSFT", "Microsoft Corp.", "2001-01-01", 100, 10001, typeof(ArgumentException))] // Price is too high
        [InlineData("MSFT", "Microsoft Corp.", "1999-12-31", 100, 100, typeof(ArgumentException))] // Date is too old
        public async Task CreateBuyOrder_InvalidParameters_ThrowsException(
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
            await Assert.ThrowsAsync(expectedException, async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_ValidParameters()
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
            BuyOrderResponse buy_order_reponse_from_add = await _stocksService.CreateBuyOrder(buy_order_request);

            // Assert
            Assert.True(buy_order_reponse_from_add.BuyOrderId != Guid.Empty);
        }
        #endregion
    }
}