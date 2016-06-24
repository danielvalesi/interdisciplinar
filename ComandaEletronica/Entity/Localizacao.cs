using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ComandaEletronica.Models;

namespace ComandaEletronica.Entity
{
    public class Localizacao
    {
        // campo:
        private int numero;

        // propriedades:
        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public int QtdLugares { get; set; }

        public string Descricao { get; set; }

        public Status Status { get; set; }

    }
}
