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
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            using (ItemModel model = new ItemModel())
            {
                List<Item> lista = model.Read();
                return View(lista);
            }
        }

        // FILTRO POR STATUS
        [HttpPost]
        public ActionResult Filter(FormCollection form)
        {
            // Extamente o name do input search <input name="status"...

            // Pega o texto converte em int
            StatusItem status = (StatusItem)int.Parse(form["status"]);
            using (ItemModel model = new ItemModel())
            {
                List<Item> lista = model.Read(status);
                return View("Index", lista);
            }
        }

        //GET: /item/create
        public ActionResult Create()
        {
            return View();
        }


        //GET: /item/create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ComandaEletronica.Entity.Item e = new Item();

            //e.Id = int.Parse(form["id"]);
            e.Produto_Id = int.Parse(form["produto_id"]);
            e.Funcionario_Id = int.Parse(form["funcionario_id"]);
            e.Pedido_Id = int.Parse(form["pedido_id"]);
            e.Qtd = int.Parse(form["qtd"]);
            e.Preco = decimal.Parse(form["preco"]);
            e.DataEntrega = DateTime.Parse(form["dataEntrega"]);
            e.Status = StatusItem.Produzindo;



            using (ItemModel model = new ItemModel())
            {
                model.Create(e);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            using (ItemModel model = new ItemModel())
            {
                return View(model.Read(id));
            }
        }



        //GET: /item/update
        [HttpPost]
        public ActionResult Update(FormCollection form)
        {
            ComandaEletronica.Entity.Item e = new Item();

            //e.Id = int.Parse(form["id"]);
            e.Produto_Id = int.Parse(form["produto_id"]);
            e.Funcionario_Id = int.Parse(form["funcionario_id"]);
            e.Pedido_Id = int.Parse(form["pedido_id"]);
            e.Qtd = int.Parse(form["qtd"]);
            e.Preco = decimal.Parse(form["preco"]);
            e.DataEntrega = DateTime.Parse(form["dataEntrega"]);
            e.Status = StatusItem.Produzindo;

            using (ItemModel model = new ItemModel())
            {
                model.Update(e);
            }

            return RedirectToAction("Index");
        }






        public ActionResult Delete(int produto_id, int pedido_id)
        {
            using (ItemModel model = new ItemModel())
                model.Delete(produto_id, pedido_id);

            return RedirectToAction("Index");
        }

    }
}
