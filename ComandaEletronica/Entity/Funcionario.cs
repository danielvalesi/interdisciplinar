using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ComandaEletronica.Models;

namespace ComandaEletronica.Entity
{
    public class Funcionario : Pessoa
    {


        public string Cargo { get; set; }

        public DateTime HorarioEntrada { get; set; }

        public DateTime HorarioSaida { get; set; }

    }
}
