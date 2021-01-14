namespace Paidy
{
    /// <summary>
    /// Represents configuration options for Paidy API.
    /// </summary>
    public sealed class PaidyOptions
    {
        #region Properties
        /// <summary>
        /// Gets the payment API endpoint.
        /// </summary>
        /// <remarks>https://api.paidy.com</remarks>
        public string ApiEndpoint { get; private init; }


        /// <summary>
        /// Gets the API secret key.
        /// </summary>
        /// <remarks>sk_test_xxxxxxxxxxxxxxxxxxxxxxxxxx</remarks>
        public string SecretKey { get; private init; }


        /// <summary>
        /// Gets the API version.
        /// </summary>
        /// <remarks>2018-04-10</remarks>
        public string ApiVersion { get; private init; }
        #endregion


        #region Constructors
#pragma warning disable CS8618
        /// <summary>
        /// Creates instance.
        /// </summary>
        public PaidyOptions()
        { }
#pragma warning restore CS8618


        /// <summary>
        /// Creates instance.
        /// </summary>
        public PaidyOptions(string apiEndpoint, string secretKey, string apiVersion)
        {
            this.ApiEndpoint = apiEndpoint;
            this.SecretKey = secretKey;
            this.ApiVersion = apiVersion;
        }
        #endregion
    }
}
