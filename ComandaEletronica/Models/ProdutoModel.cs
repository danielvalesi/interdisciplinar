using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using ComandaEletronica.Entity;

namespace ComandaEletronica.Models
{
    public class ProdutoModel : Model
    {

        // CADASTRAR PRODUTO
        public void Create(Produto e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    INSERT INTO Produtos
                    (nome, preco, descricao, categoria, imagem, tempoPreparo, estoque )
                    VALUES
                    (@nome, @preco, @descricao, @categoria, @imagem, @tempoPreparo, @estoque)
            ";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@preco", e.Preco);
            cmd.Parameters.AddWithValue("@descricao", e.Descricao);
            cmd.Parameters.AddWithValue("@categoria", e.Categoria);
            cmd.Parameters.AddWithValue("@imagem", e.Imagem);
            cmd.Parameters.AddWithValue("@tempoPreparo", e.TempoPreparo);
            cmd.Parameters.AddWithValue("@estoque", e.Estoque);

            cmd.ExecuteNonQuery();

        }

        // LISTANDO PRODUTOS (SELECT)

        public List<Produto> Read()
        {
            List<Produto> lista = new List<Produto>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Produtos";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Produto produto = new Produto();
                //cliente.ClienteId = reader.GetInt32(0);
                produto.Id = (int)reader["Id"];
                produto.Nome = (string)reader["Nome"];
                produto.Preco = (decimal)reader["Preco"];
                produto.Descricao = (string)reader["Descricao"];
                produto.Categoria = (Categoria)reader["Categoria"];
                produto.Imagem = (string)(reader["Imagem"] != DBNull.Value ? reader["Imagem"] : null);
                produto.TempoPreparo = (int)reader["TempoPreparo"];
                produto.Estoque = (int)reader["Estoque"];


                lista.Add(produto);
            }


            return lista;
        }

        // BUSCA DE PRODUTO POR NOME

        internal List<Produto> Read(string nome)
        {
            List<Produto> lista = new List<Produto>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Produtos where nome like @nome";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Produto produto = new Produto();
                //cliente.ClienteId = reader.GetInt32(0);
                produto.Id = (int)reader["Id"];
                produto.Nome = (string)reader["Nome"];
                produto.Preco = (decimal)reader["Preco"];
                produto.Descricao = (string)reader["Descricao"];
                produto.Categoria = (Categoria)reader["Categoria"];
                produto.Imagem = (string)reader["Imagem"];
                produto.TempoPreparo = (int)reader["TempoPreparo"];
                produto.Estoque = (int)reader["Estoque"];


                lista.Add(produto);
            }

            return lista;

        }

        // BUSCA POR ID

        internal Produto Read(int id)
        {
            Produto produto = new Produto();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Produtos where id = @id";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                produto.Id = (int)reader["Id"];
                produto.Nome = (string)reader["Nome"];
                produto.Preco = (decimal)reader["Preco"];
                produto.Descricao = (string)reader["Descricao"];
                produto.Categoria = (Categoria)reader["Categoria"];
                //produto.Imagem = (string)reader["Imagem"];
                produto.Imagem = (string)(reader["Imagem"] != DBNull.Value ? reader["Imagem"] : null);
                produto.TempoPreparo = (int)reader["TempoPreparo"];
                produto.Estoque = (int)reader["Estoque"];
            }

            return produto;

        }

        // ATUALIZAR PRODUTO
        public void Update(Produto e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    UPDATE Produtos set
                    nome = @nome, preco = @preco, descricao = @descricao, categoria = @categoria, imagem = @imagem, tempoPreparo = @tempoPreparo, estoque = @estoque
                    WHERE
                    id = @id;
            ";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@preco", e.Preco);
            cmd.Parameters.AddWithValue("@descricao", e.Descricao);
            cmd.Parameters.AddWithValue("@categoria", e.Categoria);
            cmd.Parameters.AddWithValue("@imagem", e.Imagem);
            cmd.Parameters.AddWithValue("@tempoPreparo", e.TempoPreparo);
            cmd.Parameters.AddWithValue("@estoque", e.Estoque);
            cmd.Parameters.AddWithValue("@id", e.Id);

            cmd.ExecuteNonQuery();

        }

        // APAGAR PRODUTO
        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    DELETE from Produtos
                    WHERE
                    id = @id;
            ";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

        }

    }
}
