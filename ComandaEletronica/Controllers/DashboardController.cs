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

      

    }
}