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
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult Index()
        {
            using (PedidoModel model = new PedidoModel())
            {
                List<Pedido> lista = model.Read();
                return View(lista);
            }
        }

        // FILTRO POR STATUS
        [HttpPost]
        public ActionResult Filter(FormCollection form)
        {
            // Extamente o name do input search <input name="status"...

            // Pega o texto converte em int
            StatusPedido status = (StatusPedido)int.Parse(form["status"]);
            using (PedidoModel model = new PedidoModel())
            {
                List<Pedido> lista = model.Read(status);
                return View("Index", lista);
            }
        }

        //GET: /pedido/create
        public ActionResult Create()
        {
            return View();
        }


        //GET: /pedido/create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ComandaEletronica.Entity.Pedido e = new Pedido();

            //e.Id = int.Parse(form["id"]);
            e.Conta_Id = int.Parse(form["conta_id"]);
            e.Status = StatusPedido.Produzindo;
            e.DataEntrega = DateTime.Parse(form["dataEntrega"]);


            using (PedidoModel model = new PedidoModel())
            {
                model.Create(e);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            using (PedidoModel model = new PedidoModel())
            {
                return View(model.Read(id));
            }
        }



        //GET: /pedido/update
        [HttpPost]
        public ActionResult Update(FormCollection form)
        {
            Pedido e = new Pedido();
            //e.Id = int.Parse(form["id"]);
            e.Conta_Id = int.Parse(form["conta_id"]);
            e.Status = StatusPedido.Produzindo;
            e.DataEntrega = DateTime.Parse(form["dataEntrega"]);

            using (PedidoModel model = new PedidoModel())
            {
                model.Update(e);
            }

            return RedirectToAction("Index");
        }






        public ActionResult Delete(int id)
        {
            using (PedidoModel model = new PedidoModel())
                model.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
