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
    public class ProdutoApiController : ApiController
    {
        private Context db = new Context();

        [HttpGet]
        [Route("produtos")]
        public HttpResponseMessage ListarTodosProdutos()
        {
            try
            {
                var lista = db.Produtos.Include("Categoria").Include("Estabelecimento").ToList();

                var response = Request.CreateResponse(HttpStatusCode.OK, lista);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nada encontrado");
            }
        }

        [HttpGet]
        [Route("apenasprodutos")]
        public HttpResponseMessage ListarApenasProdutos()
        {
            try
            {
                var lista = db.Produtos.ToList();

                var response = Request.CreateResponse(HttpStatusCode.OK, lista);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nada encontrado");
            }
        }

        [HttpGet]
        [Route("categoriaproduto/{categoria}")]
        public HttpResponseMessage BuscarProdutosPelaCategoria(string categoria)
        {
            try
            {
                var busca = db.Produtos.Include("Categoria").Include("Estabelecimento")
                    .Where(x => x.Categoria.Tipo.ToLower().Contains(categoria.ToLower()));

                var response = Request.CreateResponse(HttpStatusCode.OK, busca);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nada encontrado");
            }
        }

        [HttpGet]
        [Route("produtos/{marca}")]
        public HttpResponseMessage BuscarProdutosPelaMarca(string marca)
        {
            try
            {
                var busca = db.Produtos.Include("Categoria").Include("Estabelecimento")
                    .Where(x => x.Marca.ToLower().Contains(marca.ToLower()));

                var response = Request.CreateResponse(HttpStatusCode.OK, busca);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nada encontrado");
            }
        }

        [HttpGet]
        [Route("produtodisponivel/{disponivel}")]
        public HttpResponseMessage BuscarProdutoDisponivel(bool disponivel)
        {
            try
            {
                var busca = db.Produtos.Include("Categoria").Include("Estabelecimento")
                    .Where(x => x.Disponivel == disponivel).ToList();

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