using System.Text.Json.Serialization;

namespace Models
{
    public class StockPriceQuote
    {
        /// <summary>
        /// Current price.
        /// </summary>
        [JsonPropertyName("c")]
        public decimal CurrentPrice { get; set; }

        /// <summary>
        /// Change in price.
        /// </summary>
        [JsonPropertyName("d")]
        public decimal Change { get; set; }

        /// <summary>
        /// Percent change.
        /// </summary>
        [JsonPropertyName("dp")]
        public decimal PercentChange { get; set; }

        /// <summary>
        /// High price of the day.
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Low price of the day.
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Open price of the day.
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// Previous close price.
        /// </summary>
        [JsonPropertyName("pc")]
        public decimal PreviousClosePrice { get; set; }

        /// <summary>
        /// Unix timestamp of the quote.
        /// </summary>
        [JsonPropertyName("t")]
        public long Timestamp { get; set; }
    }

}
