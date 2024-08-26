using System.Net;

namespace PhishingApi.Domain;

public class WebsiteChecker
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<HttpResponseMessage> IsWebsiteActive(string url)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br, zstd");
                httpClient.DefaultRequestHeaders.Add("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                httpClient.DefaultRequestHeaders.Add("Priority", "u=0, i");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Google Chrome\";v=\"126\"");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "none");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");

                httpClient.Timeout = TimeSpan.FromSeconds(180);

                return await httpClient.GetAsync(url);
            }
        }
        catch (Exception)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}