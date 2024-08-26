using System.Net;

namespace PhishingApi.Domain;

public class PhishingResponse
{
    public string Data { get; set; }
    public string Site { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}