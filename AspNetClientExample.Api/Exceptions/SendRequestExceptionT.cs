using System.Net;

namespace AspNetClientExample.Api.Exceptions;

public class SendRequestException<TResponseData> : SendRequestException
{
    public TResponseData? ResponseData { get; }

    public SendRequestException(
        Uri? uri,
        string? content,
        HttpStatusCode statusCode,
        ILookup<string?, string?> headers,
        TResponseData? responseData)
        : base(uri, content, statusCode, headers)
    {
        ResponseData = responseData;
    }
}