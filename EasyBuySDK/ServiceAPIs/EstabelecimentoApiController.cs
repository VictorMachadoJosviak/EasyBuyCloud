using EasyBuySDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EasyBuySDK.ServiceAPIs
{
    [RoutePrefix("api/public")]
    public class EstabelecimentoApiController : ApiController
    {
        private Context db = new Context();

        [HttpGet]
        [Route("mercados")]
        public HttpResponseMessage PegaTodosOsEstabbelecimentos()
        {
            try
            {
                var lista = db.Estabelecimentos.ToList();

                var response = Request.CreateResponse(HttpStatusCode.OK, lista);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nada encontrado");
            }
        }

        [HttpGet]
        [Route("estabelecimentos/{nome}")]
        public HttpResponseMessage BuscarEstabelecimentoPeloNome(string nome)
        {
            try
            {
                var busca = db.Estabelecimentos.Where(x => x.Nome.ToLower().Contains(nome.ToLower()));

                var response = Request.CreateResponse(HttpStatusCode.OK, busca);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nada encontrado");
            }
        }
    }
}