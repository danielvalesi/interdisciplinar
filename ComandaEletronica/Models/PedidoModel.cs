using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data.SqlClient;
using ComandaEletronica.Entity;

namespace ComandaEletronica.Models
{
    public class PedidoModel : Model
    {

        // CADASTRAR PEDIDO
        public void Create(Pedido e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    INSERT INTO Pedidos
                    (conta_id, status, dataEntrega)
                    VALUES
                    (@conta_id, @status, @dataEntrega)
            ";

            cmd.Parameters.AddWithValue("@conta_id", e.Conta_Id);
            cmd.Parameters.AddWithValue("@status", e.Status);
            cmd.Parameters.AddWithValue("@dataEntrega", e.DataEntrega);


            cmd.ExecuteNonQuery();

        }

        // LISTANDO PEDIDOS (SELECT)

        public List<Pedido> Read()
        {
            List<Pedido> lista = new List<Pedido>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Pedidos";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Pedido pedido = new Pedido();

                pedido.Id = (int)reader["Conta_Id"];
                pedido.Status = (StatusPedido)reader["Status"];
                pedido.DataEntrega = (DateTime)reader["DataEntrega"];


                lista.Add(pedido);
            }


            return lista;
        }

        // BUSCA DE PEDIDO POR STATUS

        internal List<Pedido> Read(StatusPedido status)
        {
            List<Pedido> lista = new List<Pedido>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Pedidos where nome like @status";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@nome", "%" + status + "%");

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Pedido pedido = new Pedido();
                //cliente.ClienteId = reader.GetInt32(0);
                pedido.Id = (int)reader["Conta_Id"];
                pedido.Status = (StatusPedido)reader["Status"];
                pedido.DataEntrega = (DateTime)reader["DataEntrega"];


                lista.Add(pedido);
            }

            return lista;

        }

        // BUSCA POR ID

        internal Pedido Read(int id)
        {
            Pedido pedido = new Pedido();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Pedidos where id = @id";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                pedido.Id = (int)reader["Conta_Id"];
                pedido.Status = (StatusPedido)reader["Status"];
                pedido.DataEntrega = (DateTime)reader["DataEntrega"];
            }

            return pedido;

        }

        // ATUALIZAR PRODUTO
        public void Update(Pedido e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"
                    INSERT INTO Pedidos
                    (conta_id, status, dataEntrega)
                    VALUES
                    (@conta_id, @status, @dataEntrega)
            ";

            cmd.Parameters.AddWithValue("@conta_id", e.Conta_Id);
            cmd.Parameters.AddWithValue("@status", e.Status);
            cmd.Parameters.AddWithValue("@dataEntrega", e.DataEntrega);

            cmd.ExecuteNonQuery();

        }

        // APAGAR PEDIDO
        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    DELETE from Pedidos
                    WHERE
                    id = @id;
            ";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

        }

    }
}
