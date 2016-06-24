using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ComandaEletronica.Models;

namespace ComandaEletronica.Entity
{
    public class Item
    {
        // campo:
        private int produto_id;

        // propriedades:
        public int Produto_Id
        {
            get { return produto_id; }
            set { produto_id = value; }
        }


        public int Funcionario_Id { get; set; }

        public int Pedido_Id { get; set; }

        public int Qtd { get; set; }

        public decimal Preco { get; set; }

        public DateTime DataEntrega { get; set; }

        public StatusItem Status { get; set; }


        public Produto Produto { get; set; }



    }
}
