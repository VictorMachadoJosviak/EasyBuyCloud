using EasyBuySDK.Helpers;
using EasyBuySDK.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyBuySDK.Controllers
{
    public class UploadExcelController : Controller
    {
        private Context db = new Context();

        // GET: UploadExcel
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase fileExcel)
        {
            try
            {
                if (fileExcel != null)
                {
                    string filename = fileExcel.FileName;
                    string caminho = System.IO.Path.Combine(Server.MapPath("~/Storage/"), filename);

                    fileExcel.SaveAs(caminho);

                    using (var data = new AccessData())
                    {
                        List<Estabelecimento> mercados = data.GetDataFromExcel(caminho);

                        if (mercados != null)
                        {
                            foreach (var item in mercados)
                            {
                                var mercado = new Estabelecimento
                                {
                                    Nome = item.Nome,
                                    Endereco = item.Endereco,
                                };

                                if (string.IsNullOrEmpty(mercado.CaminhoImagem))
                                {
                                    string path = "~/Images/";

                                    DirectoryInfo directory = new DirectoryInfo(Server.MapPath(path));
                                    FileInfo[] files = directory.GetFiles("SemImagem.jpg");

                                    foreach (var imagem in files)
                                    {
                                        mercado.CaminhoImagem = imagem.Name;
                                    }
                                }

                                db.Estabelecimentos.Add(mercado);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Erro = "Voce precesa ter o acces instalado para poder ler o excel";
                }
                return RedirectToAction("Index", "Estabelecimentoes");
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View();
            }
        }
    }
}