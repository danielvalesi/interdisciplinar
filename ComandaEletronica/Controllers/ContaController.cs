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
    public class ContaController : Controller
    {
        // GET: Conta
        public ActionResult Index()
        {
            using (ContaModel model = new ContaModel())
            {
                List<Conta> lista = model.Read();
                return View(lista);
            }
        }

        // FILTRO POR NOMES
        [HttpPost]
        public ActionResult Filter(FormCollection form)
        {
            // Extamente o name do input search <input name="status"..
            string statusConta = form["statusConta"];
            using (ContaModel model = new ContaModel())
            {
                List<Conta> lista = model.Read(statusConta);
                return View("Index", lista);
            }
        }

        //GET: /conta/create
        public ActionResult Create()
        {
            return View();
        }


        //GET: /conta/create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ComandaEletronica.Entity.Conta e = new Conta();
            e.Localizacao_id = int.Parse(form["localizacao_id"]);
            e.Cliente_id = int.Parse(form["cliente_id"]);
            e.DataAbertura = DateTime.Parse(form["dataAbertura"]);
            e.DataFechamento = DateTime.Parse(form["dataFechamento"]);
            e.Valor = decimal.Parse(form["valor"]);
            e.Status = StatusConta.Fechada;
            e.FormaPagamento = FormaPagamento.Dinheiro;



            using (ContaModel model = new ContaModel())
            {
                model.Create(e);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            using (ContaModel model = new ContaModel())
            {
                return View(model.Read(id));
            }
        }



        //GET: /conta/update
        [HttpPost]
        public ActionResult Update(FormCollection form)
        {
            Conta e = new Conta();
            e.Localizacao_id = int.Parse(form["localizacao_id"]);
            e.Cliente_id = int.Parse(form["cliente_id"]);
            e.DataAbertura = DateTime.Parse(form["dataAbertura"]);
            e.Valor = decimal.Parse(form["valor"]);
            e.Status = StatusConta.Fechada;
            e.FormaPagamento = FormaPagamento.Dinheiro;
            e.Id = int.Parse(form["id"]);


            using (ContaModel model = new ContaModel())
            {
                model.Update(e);
            }

            return RedirectToAction("Index");
        }






        public ActionResult Delete(int id)
        {
            using (ContaModel model = new ContaModel())
                model.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
