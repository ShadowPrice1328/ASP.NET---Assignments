namespace Models
{
    public class CompanyProfile
    {
        /// <summary>
        /// Country of the company's headquarters.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Currency used in the company's filings.
        /// </summary>
        public string? Currency { get; set; }

        public string? EstimateCurrency { get; set; }

        /// <summary>
        /// Exchange on which the company is listed.
        /// </summary>
        public string? Exchange { get; set; }

        /// <summary>
        /// Date of the company's Initial Public Offering (IPO).
        /// </summary>
        public DateTime IPO { get; set; }

        /// <summary>
        /// Market capitalization of the company.
        /// </summary>
        public decimal MarketCapitalization { get; set; }

        /// <summary>
        /// Name of the company.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Company's phone number.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Number of outstanding shares.
        /// </summary>
        public double ShareOutstanding { get; set; }

        /// <summary>
        /// Company symbol or ticker as used on the listed exchange.
        /// </summary>
        public string? Ticker { get; set; }

        /// <summary>
        /// Company's website URL.
        /// </summary>
        public string? WebUrl { get; set; }

        /// <summary>
        /// URL of the company's logo image.
        /// </summary>
        public string? Logo { get; set; }

        /// <summary>
        /// Industry classification according to Finnhub.
        /// </summary>
        public string? FinnhubIndustry { get; set; }
    }
}
