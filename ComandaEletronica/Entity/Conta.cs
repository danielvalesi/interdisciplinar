using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ComandaEletronica.Models;

namespace ComandaEletronica.Entity
{
    public class Conta
    {
        //

        public Conta()
        {
            Pedidos = new List<Pedido>();
        }

        // campo:
        private int id;

        // propriedades:
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Localizacao_id { get; set; }

        public int Cliente_id { get; set; }

        public DateTime DataAbertura { get; set; }

        public DateTime? DataFechamento { get; set; }

        public decimal Valor { get; set; }

        public StatusConta Status { get; set; }

        public FormaPagamento FormaPagamento { get; set; }

        public List<Pedido> Pedidos { get; set; }





    }
}
