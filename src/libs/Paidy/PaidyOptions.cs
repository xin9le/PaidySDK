namespace Paidy;



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
    public required string ApiEndpoint { get; init; }


    /// <summary>
    /// Gets the API secret key.
    /// </summary>
    /// <remarks>sk_test_xxxxxxxxxxxxxxxxxxxxxxxxxx</remarks>
    public required string SecretKey { get; init; }


    /// <summary>
    /// Gets the API version.
    /// If the API version is not set, the system will use the API version set in the Merchant Dashboard.
    /// If nothing is set in the Merchant Dashboard, the system will default to the current version of the API.
    /// </summary>
    /// <remarks>2018-04-10</remarks>
    public string? ApiVersion { get; init; }
    #endregion
}
