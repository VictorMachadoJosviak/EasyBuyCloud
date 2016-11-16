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
    public class CategoriaApiController : ApiController
    {
        private Context db = new Context();

        [Route("categorias")]
        [HttpGet]
        public HttpResponseMessage GetCategorias()
        {
            try
            {
                var lista = db.Categorias.ToList();

                var response = Request.CreateResponse(HttpStatusCode.OK, lista);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nada encontrado");
            }
        }

        [HttpGet]
        [Route("categorias/{tipo}")]
        public HttpResponseMessage BuscaCategoriaPorTipo(string tipo)
        {
            try
            {
                var busca = db.Categorias.Where(x => x.Tipo.ToLower().Contains(tipo.ToLower())).ToList();

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