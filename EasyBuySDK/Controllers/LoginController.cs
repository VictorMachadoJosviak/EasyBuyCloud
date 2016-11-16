using EasyBuySDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EasyBuySDK.Controllers
{
    public class LoginController : Controller
    {
        private Context db = new Context();

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Produtoes");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Usuario user, bool permanecerLogado)
        {
            try
            {
                Usuario u = db.Usuarios.FirstOrDefault(o => o.Email.Equals(user.Email) &&
                o.Senha.Equals(user.Senha));

                if (u != null)
                {
                    FormsAuthentication.SetAuthCookie(u.Email, permanecerLogado);

                    return RedirectToAction("Index", "Produtoes");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario ou senha inváidos");
                }

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult CriarConta()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CriarConta([Bind(Include = "Id,Nome,Email,Senha,ConfirmaSenha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                await db.SaveChangesAsync();

                Usuario u = db.Usuarios.FirstOrDefault(o => o.Nome.Equals(usuario.Nome) &&
                o.Senha.Equals(usuario.Senha));

                if (u != null)
                {
                    FormsAuthentication.SetAuthCookie(u.Nome, true);
                    return RedirectToAction("Index", "Produtoes");
                }
                else
                {
                    ModelState.AddModelError("", "Erro");
                }

                return RedirectToAction("Index", "Produtoes");
            }

            return View(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}