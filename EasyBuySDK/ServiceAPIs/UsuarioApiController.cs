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
    public class UsuarioApiController : ApiController
    {
        private Context db = new Context();

        [HttpGet]
        [Route("usuarios")]
        public HttpResponseMessage ListarTodosOsUsuarios()
        {
            try
            {
                var lista = db.Usuarios.ToList();

                var response = Request.CreateResponse(HttpStatusCode.OK, lista);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nenhum usuario cadastrado");
            }
        }

        [HttpGet]
        [Route("usuario/{nome}")]
        public HttpResponseMessage BuscarUsuarioPorNome(string nome)
        {
            try
            {
                var busca = db.Usuarios.Where(x => x.Nome.ToLower().Contains(nome.ToLower()));

                var resp = Request.CreateResponse(HttpStatusCode.OK, busca);

                return resp;
            }
            catch (Exception err)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nenhum usuario cadastrado");
            }
        }

        [HttpPost]
        [Route("cadastro")]
        public HttpResponseMessage CadastrarUsuario(Usuario user)
        {
            try
            {
                if (user == null) return Request.CreateResponse(HttpStatusCode.BadRequest, "objeto não pode ser nulo");

                var inserir = db.Usuarios.Add(user);
                db.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.OK, inserir);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nenhum usuario cadastrado");
            }
        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage AutenticaUsuario(Usuario usuario)
        {
            try
            {
                Usuario u = db.Usuarios.FirstOrDefault(o => o.Email.Equals(usuario.Email) &&
                o.Senha.Equals(usuario.Senha));

                if (u == null) return Request.CreateResponse(HttpStatusCode.BadRequest, "Usuario nao encontrado");

                var response = Request.CreateResponse(HttpStatusCode.OK, u);

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}