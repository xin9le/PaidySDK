using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Paidy.Internals.Net.Http;
using Paidy.Internals.Text.Json;
using Paidy.Tokens.Entities;

namespace Paidy.Tokens;



/// <summary>
/// Provides the token API service.
/// </summary>
public sealed class TokenService
{
    #region Properties
    /// <summary>
    /// Gets the factory of <see cref="System.Net.Http.HttpClient"/>.
    /// </summary>
    private IHttpClientFactory HttpClientFactory { get; }


    /// <summary>
    /// Gets the HTTP client.
    /// </summary>
    private HttpClient HttpClient
        => this.HttpClientFactory.CreateClient(HttpClientNames.Paidy);
    #endregion


    #region Constructors
    /// <summary>
    /// Creates instance.
    /// </summary>
    internal TokenService(IHttpClientFactory factory)
        => this.HttpClientFactory = factory;
    #endregion


    /// <summary>
    /// Suspends a token.
    /// The token to be suspended must have a status of ACTIVE.
    /// If successful, the token status is updated to SUSPENDED and all authorization requests for a new payment using the token will be rejected.
    /// </summary>
    /// <param name="id">Paidy token ID</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#3-2-suspend-a-token"></a>
    /// </remarks>
    public async ValueTask<TokenResponse> SuspendAsync(string id, SuspendRequest request, CancellationToken cancellationToken = default)
    {
        var url = $"tokens/{id}/suspend";
        var options = JsonSerializerOptionsProvider.NoEscapeIgnoreNull;
        var response = await this.HttpClient.PostAsJsonAsync(url, request, options, cancellationToken).ConfigureAwait(false);
        return await ReadContentAsync<TokenResponse>(response, cancellationToken).ConfigureAwait(false);
    }


    /// <summary>
    /// Resumes a token.
    /// If successful, the token status is updated to ACTIVE and the merchant can use this token again to create payments.
    /// A token can only be resumed by the same authority that suspended it.
    /// If you try to resume a token that was suspended by a consumer, Paidy will return an error.
    /// </summary>
    /// <param name="id">Paidy token ID</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#3-3-resume-a-token"></a>
    /// </remarks>
    public async ValueTask<TokenResponse> ResumeAsync(string id, ResumeRequest request, CancellationToken cancellationToken = default)
    {
        var url = $"tokens/{id}/resume";
        var options = JsonSerializerOptionsProvider.NoEscapeIgnoreNull;
        var response = await this.HttpClient.PostAsJsonAsync(url, request, options, cancellationToken).ConfigureAwait(false);
        return await ReadContentAsync<TokenResponse>(response, cancellationToken).ConfigureAwait(false);
    }


    /// <summary>
    /// Permanently "disables" a token.
    /// The token to be deleted can have a status of ACTIVE or SUSPENDED.
    /// </summary>
    /// <param name="id">Paidy token ID</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#3-4-delete-a-token"></a>
    /// </remarks>
    public async ValueTask<TokenResponse> DeleteAsync(string id, DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var url = $"tokens/{id}/delete";
        var options = JsonSerializerOptionsProvider.NoEscapeIgnoreNull;
        var response = await this.HttpClient.PostAsJsonAsync(url, request, options, cancellationToken).ConfigureAwait(false);
        return await ReadContentAsync<TokenResponse>(response, cancellationToken).ConfigureAwait(false);
    }


    /// <summary>
    /// Retrieves the specified token object.
    /// You need a valid token ID, beginning with tok_.
    /// </summary>
    /// <param name="id">Paidy token ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#3-5-retrieve-a-token"></a>
    /// </remarks>
    public async ValueTask<TokenResponse> RetrieveAsync(string id, CancellationToken cancellationToken = default)
    {
        var url = $"tokens/{id}";
        var response = await this.HttpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
        return await ReadContentAsync<TokenResponse>(response, cancellationToken).ConfigureAwait(false);
    }


    /// <summary>
    /// Retrieve a list of all tokens, with a status of ACTIVE or SUSPENDED, for all your consumers.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>
    /// Reference : <a href="https://paidy.com/docs/api/en/index.html#3-6-retrieve-all-tokens"></a>
    /// </remarks>
    public async ValueTask<TokenResponse[]> RetrieveAllAsync(CancellationToken cancellationToken = default)
    {
        const string url = "tokens";
        var response = await this.HttpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
        return await ReadContentAsync<TokenResponse[]>(response, cancellationToken).ConfigureAwait(false);
    }


    #region Helpers
    /// <summary>
    /// Reads the response content of the token.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async ValueTask<T> ReadContentAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.StatusCode is HttpStatusCode.OK)
        {
            var result = await response.Content.ReadFromJsonAsync<T>(options: null, cancellationToken).ConfigureAwait(false);
            if (result is null)
                throw new NotSupportedException($"Null response was detected | StatusCode : {response.StatusCode}");
            return result;
        }
        else
        {
#if NETSTANDARD || NET462_OR_GREATER
            var error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#else
            var error = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#endif
            throw new PaidyException(response.StatusCode, error);
        }
    }
    #endregion
}
