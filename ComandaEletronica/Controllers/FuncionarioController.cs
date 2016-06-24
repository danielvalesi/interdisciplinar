using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComandaEletronica.Entity;
using ComandaEletronica.Models;

namespace ComandaEletronica.Controllers
{
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public ActionResult Index()
        {
            using (FuncionarioModel model = new FuncionarioModel())
            {
                List<Funcionario> lista = model.Read();
                return View(lista);
            }
        }

        // FILTRO POR NOMES
        [HttpPost]
        public ActionResult Filter(FormCollection form)
        {
            // Extamente o name do input search <input name="nome"...
            string nome = form["nome"];
            using (FuncionarioModel model = new FuncionarioModel())
            {
                List<Funcionario> lista = model.Read(nome);
                return View("Index", lista);
            }
        }

        //GET: /Funcionario/create
        public ActionResult Create()
        {
            return View();
        }

        //GET: /Funcionario/create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Funcionario e = new Funcionario();
            e.Nome = form["nome"];
            e.Email = form["email"];
            e.Senha = form["senha"];



            using (FuncionarioModel model = new FuncionarioModel())
            {
                model.Create(e);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            using (FuncionarioModel model = new FuncionarioModel())
            {
                return View(model.Read(id));
            }
        }



        //GET: /Funcionario/update
        [HttpPost]
        public ActionResult Update(FormCollection form)
        {
            Funcionario e = new Funcionario();
            e.Nome = form["nome"];
            e.Email = form["email"];
            e.Senha = form["senha"];


            e.Id = int.Parse(form["pessoa_id"]);


            using (FuncionarioModel model = new FuncionarioModel())
            {
                model.Update(e);
            }

            return RedirectToAction("Index");
        }






        public ActionResult Delete(int id)
        {
            using (FuncionarioModel model = new FuncionarioModel())
                model.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
