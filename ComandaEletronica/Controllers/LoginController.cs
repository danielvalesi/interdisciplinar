using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComandaEletronica.Models;
using ComandaEletronica.Entity;


namespace ComandaEletronica.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            using (UsuarioModel Model = new UsuarioModel())
            {
                Pessoa p = Model.Read(form["email"], form["senha"]);

                if (p == null)
                {
                    return RedirectToAction("index");
                }
                else
                {

                    Session["pessoa"] = p;

                    if (p is Funcionario)
                    {

                        return RedirectToAction("index", "dashboard");
                    }
                    else
                    {
                        Session["carrinho"] = new Conta();

                        return RedirectToAction("cliente", "Dashboard");
                    }


                }
            }


        }
    }
}
