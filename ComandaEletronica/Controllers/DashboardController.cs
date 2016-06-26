using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComandaEletronica.Entity;
using ComandaEletronica.Models;

namespace ComandaEletronica.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard do Funcionario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dashboard do Cliente
        public ActionResult Cliente()
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                List<Produto> lista = model.Read();
                return View(lista);
            }
        }

        public ActionResult Adicionar(int id)
        {
            // acessando o banco e pegar os dados do produto

            using (ProdutoModel model = new ProdutoModel())
            {
                Produto p = model.Read(id);

                //Conta conta = (Conta)Session["carrinho"];
                if (Session["Pedido"] == null)
                    Session["Pedido"] = new Pedido();

                ((Pedido)Session["Pedido"]).Itens.Add(new Item() { Produto_Id = id, Produto = p, Qtd = 1 });

                return RedirectToAction("Cliente");
            }

        }

        public ActionResult Finalizar()
        {
            // acessando o banco e pegar os dados do produto

            Conta conta = (Conta)Session["carrinho"];

            conta.Pedidos.Add((Pedido)Session["Pedido"]);

            return RedirectToAction("Cliente");

        }

        public ActionResult FinalizarConta()
        {
            // acessando o banco e pegar os dados do produto

            Conta conta = (Conta)Session["carrinho"];

            // create


            using (ContaModel model = new ContaModel())
            {
                model.Create(conta);
            }
               

                foreach (var p in conta.Pedidos) 
                {
                    using (PedidoModel model = new PedidoModel())
                    model.Create(p);

                    foreach (var i in p.Itens)
                    {
                        using (ItemModel model = new ItemModel())
                        model.Create(i);
                    }
                }

            return RedirectToAction("Cliente");

        }



    }
}