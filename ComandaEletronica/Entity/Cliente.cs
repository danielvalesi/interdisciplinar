using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ComandaEletronica.Models;

namespace ComandaEletronica.Entity
{
    public class Cliente : Pessoa
    {
        public int Pessoa_id { get; set; }

        public decimal PorcentagemDesconto { get; set; }

    }
}
