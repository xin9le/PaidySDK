using System.Net;
using System.Runtime.Serialization;
using Paidy.Internals;
using Utf8Json;



namespace Paidy
{
    /// <summary>
    /// Represents error object.
    /// </summary>
    public sealed class ErrorResponse
    {
#pragma warning disable CS8618
        #region Properties
        /// <summary>
        /// Internal error code.
        /// </summary>
        [DataMember(Name = "reference")]
        public string Reference { get; private init; }


        /// <summary>
        /// HTTP response code.
        /// </summary>
        [DataMember(Name = "status")]
        [JsonFormatter(typeof(HttpStatusCodeFormatter))]
        public HttpStatusCode Status { get; private init; }


        /// <summary>
        /// Code indicating the kind of error that occurred.
        /// </summary>
        [DataMember(Name = "code")]
        public string Code { get; private init; }


        /// <summary>
        /// Text version of the error code.
        /// This field can sometimes provide the name of the field with which there is an issue.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; private init; }


        /// <summary>
        /// Description providing more details about the error.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; private init; }
        #endregion


        #region Constructors
        /// <summary>
        /// Creates instance.
        /// </summary>
        private ErrorResponse()
        { }
        #endregion
#pragma warning restore CS8618
    }
}
