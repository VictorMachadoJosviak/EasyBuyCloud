using EasyBuySDK.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EasyBuySDK.Controllers
{
    public class EstabelecimentoesController : Controller
    {
        private Context db = new Context();

        public async Task<ActionResult> Index()
        {
            return View(await db.Estabelecimentos.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estabelecimento estabelecimento = await db.Estabelecimentos.FindAsync(id);
            if (estabelecimento == null)
            {
                return HttpNotFound();
            }

            ViewBag.mercado = estabelecimento;

            return View(estabelecimento);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Endereco,CaminhoImagem")] Estabelecimento estabelecimento, HttpPostedFileBase fileImagem)
        {
            if (ModelState.IsValid)
            {
                if (fileImagem != null)
                {
                    string imagemNome = fileImagem.FileName;
                    string caminho = System.IO.Path.Combine(Server.MapPath("~/Images/"), imagemNome);

                    fileImagem.SaveAs(caminho);
                    estabelecimento.CaminhoImagem = imagemNome;
                }
                else
                {
                    estabelecimento.CaminhoImagem = "SemImagem.jpg";
                }

                db.Estabelecimentos.Add(estabelecimento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(estabelecimento);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estabelecimento estabelecimento = await db.Estabelecimentos.FindAsync(id);
            if (estabelecimento == null)
            {
                return HttpNotFound();
            }
            return View(estabelecimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Endereco")] Estabelecimento estabelecimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estabelecimento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(estabelecimento);
        }

        // GET: Estabelecimentoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estabelecimento estabelecimento = await db.Estabelecimentos.FindAsync(id);
            if (estabelecimento == null)
            {
                return HttpNotFound();
            }
            return View(estabelecimento);
        }

        // POST: Estabelecimentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Estabelecimento estabelecimento = await db.Estabelecimentos.FindAsync(id);
            db.Estabelecimentos.Remove(estabelecimento);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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