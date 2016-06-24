using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComandaEletronica.Entity;
using ComandaEletronica.Models;
using System.Web.Hosting;


namespace ComandaEletronica.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                List<Produto> lista = model.Read();
                return View(lista);
            }
        }

        // FILTRO POR NOMES
        [HttpPost]
        public ActionResult Filter(FormCollection form)
        {
            // Extamente o name do input search <input name="nome"...
            string nome = form["nome"];
            using (ProdutoModel model = new ProdutoModel())
            {
                List<Produto> lista = model.Read(nome);
                return View("Index", lista);
            }
        }

        //GET: /produto/create
        public ActionResult Create()
        {
            return View();
        }


        //GET: /produto/create
        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase imagem)
        {
            ComandaEletronica.Entity.Produto e = new Produto();
            e.Nome = form["nome"];
            e.Preco = decimal.Parse(form["preco"]);
            e.Descricao = form["descricao"];
            e.Categoria = Categoria.Pronto;

            e.TempoPreparo = int.Parse(form["tempoPreparo"]);
            e.Estoque = int.Parse(form["estoque"]);


            if (imagem != null)
            {
                string[] name = imagem.FileName.Split('\\');
                e.Imagem = "/Arquivos/" + name[name.Length - 1];

                imagem.SaveAs(HostingEnvironment.ApplicationPhysicalPath + "\\Arquivos\\" + name[name.Length - 1]);
            }



            using (ProdutoModel model = new ProdutoModel())
            {
                model.Create(e);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                return View(model.Read(id));
            }
        }



        //GET: /produto/update
        [HttpPost]
        public ActionResult Update(FormCollection form, HttpPostedFileBase imagem)
        {
            Produto e = new Produto();
            e.Nome = form["nome"];
            e.Preco = decimal.Parse(form["preco"]);
            e.Descricao = form["descricao"];
            e.Categoria = Categoria.Pronto;
            // e.Imagem = form["imagem"];
            e.TempoPreparo = int.Parse(form["tempoPreparo"]);
            e.Estoque = int.Parse(form["estoque"]);
            e.Id = int.Parse(form["id"]);

            if (imagem != null)
            {
                string[] name = imagem.FileName.Split('\\');
                e.Imagem = "/Arquivos/" + name[name.Length - 1];

                imagem.SaveAs(HostingEnvironment.ApplicationPhysicalPath + "\\Arquivos\\" + name[name.Length - 1]);
            }


            using (ProdutoModel model = new ProdutoModel())
            {
                model.Update(e);
            }

            return RedirectToAction("Index");
        }






        public ActionResult Delete(int id)
        {
            using (ProdutoModel model = new ProdutoModel())
                model.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
