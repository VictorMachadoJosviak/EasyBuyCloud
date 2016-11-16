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
    public class ProdutoesController : Controller
    {
        private Context db = new Context();

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var produtos = db.Produtos.Include(p => p.Categoria).Include(p => p.Estabelecimento);
            return View(await produtos.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Index(string txtPesquisar)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPesquisar))
                {
                    var busca = db.Produtos.Where(x => x.Marca.ToLower().Contains(txtPesquisar.ToLower()) ||
                                                 x.Estabelecimento.Nome.ToLower().Contains(txtPesquisar.ToLower()))
                                                 .Include(x => x.Categoria).Include(x => x.Estabelecimento);

                    Produtos = new List<Produto>(busca);

                    return View(Produtos);
                }
                else
                {
                    return RedirectToAction("Index", "Produtoes");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Produtoes");
            }
        }

        // GET: Produtoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produtoes/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Tipo");
            ViewBag.EstabelecimentoId = new SelectList(db.Estabelecimentos, "Id", "Nome");
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Marca,Preco,Disponivel,CategoriaId,EstabelecimentoId,CaminhoImagem")] Produto produto, HttpPostedFileBase fileImagem)
        {
            if (ModelState.IsValid)
            {
                if (fileImagem != null)
                {
                    string imagemNome = fileImagem.FileName;
                    string caminho = System.IO.Path.Combine(Server.MapPath("~/Images/"), imagemNome);

                    fileImagem.SaveAs(caminho);
                    produto.CaminhoImagem = imagemNome;
                }
                else
                {
                    produto.CaminhoImagem = "SemImagem.jpg";
                }

                db.Produtos.Add(produto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Tipo", produto.CategoriaId);
            ViewBag.EstabelecimentoId = new SelectList(db.Estabelecimentos, "Id", "Nome", produto.EstabelecimentoId);
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Tipo", produto.CategoriaId);
            ViewBag.EstabelecimentoId = new SelectList(db.Estabelecimentos, "Id", "Nome", produto.EstabelecimentoId);
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Marca,Preco,Disponivel,CategoriaId,EstabelecimentoId,CaminhoImagem")] Produto produto, HttpPostedFileBase fileImagem)
        {
            if (ModelState.IsValid)
            {
                if (fileImagem != null)
                {
                    var imgNome = fileImagem.FileName;
                    var caminho = System.IO.Path.Combine(Server.MapPath("~/Images/"), imgNome);
                    fileImagem.SaveAs(caminho);
                    produto.CaminhoImagem = imgNome;
                }
                else
                {
                    produto.CaminhoImagem = "SemImagem.png";
                }

                db.Entry(produto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Tipo", produto.CategoriaId);
            ViewBag.EstabelecimentoId = new SelectList(db.Estabelecimentos, "Id", "Nome", produto.EstabelecimentoId);
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Produto produto = await db.Produtos.FindAsync(id);
            db.Produtos.Remove(produto);
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

        private List<Produto> _produtos;

        public List<Produto> Produtos
        {
            get { return _produtos; }
            set { _produtos = value; }
        }
    }
}