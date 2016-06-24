using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ComandaEletronica.Models;

namespace ComandaEletronica.Entity
{
    public class Produto
    {
        // campo:
        private int id;

        // propriedades:
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public string Descricao { get; set; }

        public Categoria Categoria { get; set; }

        public string Imagem { get; set; }

        public int TempoPreparo { get; set; }

        public int Estoque { get; set; }
    }
}
