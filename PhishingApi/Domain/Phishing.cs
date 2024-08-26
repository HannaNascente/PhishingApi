using CsvHelper;
using Newtonsoft.Json;
using System.Globalization;

namespace PhishingApi.Domain;

public class Phishing
{
    public string Data { get; set; }
    public string Origem { get; set; }
    public string Marca { get; set; }
    public string Site { get; set; }
    public string Host { get; set; }

    [JsonProperty("meios de pagamento")]
    public string MeiosPagamento { get; set; }

    [JsonProperty("responsável/pix")]
    public string ResponsvelPix { get; set; }

    public string CodigoPix { get; set; }

    [JsonProperty("Responsável site")]
    public string ResponsvelSite { get; set; }

    [JsonProperty("Notificação/Data")]
    public string NotificacaoData { get; set; }

    public string Status { get; set; }

    public static IEnumerable<Phishing> GetSiteNotificadoComStatusVazio(IList<Phishing> phishings)
    {
        return phishings.Where(phishing =>
            phishing.Site is not null &&
            phishing.NotificacaoData is not (null or "") &&
            phishing.Status is (null or ""));
    }

    public static IEnumerable<Phishing> GetSiteComStatusRemovido(IList<Phishing> phishings)
    {
        return phishings.Where(phishing => phishing.Status == "removido");
    }

    public static IEnumerable<Phishing> GetSiteComStatusInativo(IList<Phishing> phishings)
    {
        return phishings.Where(phishing => phishing.Status == "inativo");
    }

    public static IEnumerable<object> PhishingsDuplicados(IList<Phishing> phishings)
    {
        return phishings
                    .GroupBy(x => x.Site)
                    .Where(g => g.Count() > 1)
                    .Select(g => new { Item = g.Key, Count = g.Count() });
    }

    public static IList<Phishing> GetPhishings(string caminhoPlanilha)
    {
        var reader = new StreamReader(caminhoPlanilha);
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<dynamic>();

        var json = JsonConvert.SerializeObject(records, Formatting.Indented);

        var phishings = JsonConvert.DeserializeObject<IList<Phishing>>(json);

        return phishings;
    }
}