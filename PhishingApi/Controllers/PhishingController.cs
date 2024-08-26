using Microsoft.AspNetCore.Mvc;
using PhishingApi.Domain;

namespace PhishingApi.Controllers
{
    [ApiController]
    [Route("api/site-phishing")]
    public class PhishingController : ControllerBase
    {
        /// <summary>
        /// Retorna todos os sites notificados com o status vazio.
        /// </summary>
        [HttpGet("notificado")]
        public IActionResult GetSitePhishingNotificadoComStatusVazio(string caminhoPlanilha)
        {
            var phishings = Phishing.GetPhishings(caminhoPlanilha);

            var phishingsNotificados = Phishing.GetSiteNotificadoComStatusVazio(phishings);

            var phishingResponse = new List<PhishingResponse>();

            foreach (var phishing in phishingsNotificados)
            {
                var checker = new WebsiteChecker();
                var response = checker.IsWebsiteActive(phishing.Site).Result;

                phishingResponse.Add(new PhishingResponse
                {
                    Site = phishing.Site,
                    StatusCode = response.StatusCode,
                    Data = phishing.Data
                });
            }

            return Ok(phishingResponse.OrderBy(x => x.Data));
        }

        /// <summary>
        /// Retorna todos os sites notificados, removidos e com o status vazio.
        /// </summary>
        [HttpGet("notificado/removido")]
        public IActionResult GetSitePhishingNotificadoERemovidoComStatusVazio(string caminhoPlanilha)
        {
            var phishings = Phishing.GetPhishings(caminhoPlanilha);

            var phishingsNotificados = Phishing.GetSiteNotificadoComStatusVazio(phishings);

            var phishingResponse = new List<PhishingResponse>();

            foreach (var phishing in phishingsNotificados)
            {
                var checker = new WebsiteChecker();
                var response = checker.IsWebsiteActive(phishing.Site).Result;

                if (!response.IsSuccessStatusCode)
                {
                    phishingResponse.Add(new PhishingResponse
                    {
                        Site = phishing.Site,
                        StatusCode = response.StatusCode,
                        Data = phishing.Data
                    });
                }
            }

            return Ok(phishingResponse.OrderBy(x => x.Data));
        }

        /// <summary>
        /// Retorna todos os sites repetidos.
        /// </summary>
        [HttpGet("duplicado")]
        public IActionResult GetSitePhishingDuplicado(string caminhoPlanilha)
        {
            var phishings = Phishing.GetPhishings(caminhoPlanilha);

            var phishingsDuplicados = Phishing.PhishingsDuplicados(phishings);

            return Ok(phishingsDuplicados);
        }

        /// <summary>
        /// Retorna todos os sites ativos com o status removido.
        /// </summary>
        [HttpGet("ativo/status-removido")]
        public IActionResult GetSitePhishingAtivoComStatusRemovido(string caminhoPlanilha)
        {
            var phishings = Phishing.GetPhishings(caminhoPlanilha);

            var phishingsStatusRemovido = Phishing.GetSiteComStatusRemovido(phishings);

            var phishingResponse = new List<PhishingResponse>();

            foreach (var phishing in phishingsStatusRemovido)
            {
                var checker = new WebsiteChecker();
                var response = checker.IsWebsiteActive(phishing.Site).Result;

                if (response.IsSuccessStatusCode)
                {
                    phishingResponse.Add(new PhishingResponse
                    {
                        Site = phishing.Site,
                        StatusCode = response.StatusCode,
                        Data = phishing.Data
                    });
                }
            }

            return Ok(phishingResponse.OrderBy(x => x.Data));
        }

        /// <summary>
        /// Retorna todos os sites ativos com o status inativos.
        /// </summary>
        [HttpGet("ativo/status-inativo")]
        public IActionResult GetSitePhishingAtivoComStatusInativo(string caminhoPlanilha)
        {
            var phishings = Phishing.GetPhishings(caminhoPlanilha);

            var phishingsInativos = Phishing.GetSiteComStatusInativo(phishings);

            var phishingResponse = new List<PhishingResponse>();

            foreach (var phishing in phishingsInativos)
            {
                var checker = new WebsiteChecker();
                var response = checker.IsWebsiteActive(phishing.Site).Result;

                if (response.IsSuccessStatusCode)
                {
                    phishingResponse.Add(new PhishingResponse
                    {
                        Site = phishing.Site,
                        StatusCode = response.StatusCode,
                        Data = phishing.Data,
                    });
                }
            }

            return Ok(phishingResponse.OrderBy(x => x.Data));
        }
    }
}