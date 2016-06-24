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
    public class LocalizacaoController : Controller
    {
        // GET: Localizacao
        public ActionResult Index()
        {
            using (LocalizacaoModel model = new LocalizacaoModel())
            {
                List<Localizacao> lista = model.Read();
                return View(lista);
            }
        }

        // FILTRO POR STATUS
        [HttpPost]
        public ActionResult Filter(FormCollection form)
        {
            // Extamente o name do input search <input name="status"...
            string status = form["status"];
            using (LocalizacaoModel model = new LocalizacaoModel())
            {
                List<Localizacao> lista = model.Read(status);
                return View("Index", lista);
            }
        }

        //GET: /localizacao/create
        public ActionResult Create()
        {
            return View();
        }


        //GET: /localizacao/create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ComandaEletronica.Entity.Localizacao e = new Localizacao();
            e.QtdLugares = int.Parse(form["qtdLugares"]);
            e.Descricao = form["descricao"];
            e.Status = Status.Livre;



            using (LocalizacaoModel model = new LocalizacaoModel())
            {
                model.Create(e);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Update(int numero)
        {
            using (LocalizacaoModel model = new LocalizacaoModel())
            {
                return View(model.Read(numero));
            }
        }



        //GET: /localizacao/update
        [HttpPost]
        public ActionResult Update(FormCollection form)
        {
            Localizacao e = new Localizacao();
            e.QtdLugares = int.Parse(form["qtdLugares"]);
            e.Descricao = form["descricao"];
            e.Status = Status.Livre;
            e.Numero = int.Parse(form["numero"]);


            using (LocalizacaoModel model = new LocalizacaoModel())
            {
                model.Update(e);
            }

            return RedirectToAction("Index");
        }






        public ActionResult Delete(int id)
        {
            using (LocalizacaoModel model = new LocalizacaoModel())
                model.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
