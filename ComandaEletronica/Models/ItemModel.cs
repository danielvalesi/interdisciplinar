using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ComandaEletronica.Entity;

namespace ComandaEletronica.Models
{
    public class ItemModel : Model
    {

        // CADASTRAR PEDIDO
        public void Create(Item e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    INSERT INTO Itens
                    (produto_id, funcionario_id, pedido_id, qtd, preco, dataEntrega, status)
                    VALUES
                    (@produto_id, @funcionario_id, @pedido_id, @qtd, @preco, @dataEntrega, @status)
            ";

            cmd.Parameters.AddWithValue("@produto_id", e.Produto_Id);
            cmd.Parameters.AddWithValue("@funcionario_id", e.Funcionario_Id);
            cmd.Parameters.AddWithValue("@pedido_id", e.Pedido_Id);
            cmd.Parameters.AddWithValue("@qtd", e.Qtd);
            cmd.Parameters.AddWithValue("@preco", e.Preco);
            cmd.Parameters.AddWithValue("@dataEntrega", e.DataEntrega);
            cmd.Parameters.AddWithValue("@status", e.Status);


            cmd.ExecuteNonQuery();

        }

        // LISTANDO ITENS (SELECT)

        public List<Item> Read()
        {
            List<Item> lista = new List<Item>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Itens";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Item item = new Item();

                item.Produto_Id = (int)reader["Produto_Id"];
                item.Funcionario_Id = (int)reader["Funcionario_Id"];
                item.Pedido_Id = (int)reader["Pedido_Id"];
                item.Qtd = (int)reader["Qtd"];
                item.Preco = (decimal)reader["Preco"];
                item.DataEntrega = (DateTime)reader["DataEntrega"];
                item.Status = (StatusItem)reader["Status"];



                lista.Add(item);
            }


            return lista;
        }

        // BUSCA DE PEDIDO POR STATUS

        internal List<Item> Read(StatusItem status)
        {
            List<Item> lista = new List<Item>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Itens where nome like @status";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@status", "%" + status + "%");

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Item item = new Item();
                //cliente.ClienteId = reader.GetInt32(0);
                item.Produto_Id = (int)reader["Produto_Id"];
                item.Funcionario_Id = (int)reader["Funcionario_Id"];
                item.Pedido_Id = (int)reader["Pedido_Id"];
                item.Qtd = (int)reader["Qtd"];
                item.Preco = (decimal)reader["Preco"];
                item.DataEntrega = (DateTime)reader["DataEntrega"];
                item.Status = (StatusItem)reader["Status"];


                lista.Add(item);
            }

            return lista;

        }

        // BUSCA POR ID

        internal Item Read(int id)
        {
            Item item = new Item();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Itens where id = @id";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                item.Produto_Id = (int)reader["Produto_Id"];
                item.Funcionario_Id = (int)reader["Funcionario_Id"];
                item.Pedido_Id = (int)reader["Pedido_Id"];
                item.Qtd = (int)reader["Qtd"];
                item.Preco = (decimal)reader["Preco"];
                item.DataEntrega = (DateTime)reader["DataEntrega"];
                item.Status = (StatusItem)reader["Status"];
            }

            return item;

        }

        // ATUALIZAR PRODUTO
        public void Update(Item e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"
                    INSERT INTO Itens
                    (produto_id, funcionario_id, pedido_id, qtd, preco, dataEntrega, status)
                    VALUES
                    (@produto_id, @funcionario_id, @pedido_id, @qtd, @preco, @dataEntrega, @status)
            ";

            cmd.Parameters.AddWithValue("@produto_id", e.Produto_Id);
            cmd.Parameters.AddWithValue("@funcionario_id", e.Funcionario_Id);
            cmd.Parameters.AddWithValue("@pedido_id", e.Pedido_Id);
            cmd.Parameters.AddWithValue("@qtd", e.Qtd);
            cmd.Parameters.AddWithValue("@preco", e.Preco);
            cmd.Parameters.AddWithValue("@dataEntrega", e.DataEntrega);
            cmd.Parameters.AddWithValue("@status", (int)e.Status);

            cmd.ExecuteNonQuery();

        }

        /* APAGAR ITEM*/
        public void Delete(int produto_id, int pedido_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    DELETE from Itens
                    WHERE
                    produto_id = @produto_id AND                    
                    pedido_id = @pedido_id
            ";

            cmd.Parameters.AddWithValue("@produto_id", produto_id);
            cmd.Parameters.AddWithValue("@pedido_id", pedido_id);

            cmd.ExecuteNonQuery();

        }

    }
}
