using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using ComandaEletronica.Entity;

namespace ComandaEletronica.Models
{
    public class ClienteModel : Model
    {

        // CADASTRAR Cliente
        public void Create(Cliente e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    INSERT INTO ClientesVIP
                    (Nome, Email, Senha, Cpf, PorcentagemDesconto)
                    VALUES
                    (@nome, @email, @senha, @cpf, @porcentagemDesconto)
            ";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@senha", e.Senha);
            cmd.Parameters.AddWithValue("@cpf", e.Cpf);
            cmd.Parameters.AddWithValue("@porcentagemDesconto", e.PorcentagemDesconto);
            


            cmd.ExecuteNonQuery();

        }

        // LISTANDO ClienteS (SELECT)

        public List<Cliente> Read()
        {
            List<Cliente> lista = new List<Cliente>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from ClientesVIP, Pessoas WHERE Pessoas.id = ClientesVIP.pessoa_id";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Cliente cliente = new Cliente();
                //cliente.ClienteId = reader.GetInt32(0);
                cliente.Pessoa_id = (int)reader["Pessoa_id"];
                cliente.Nome = (string)reader["Nome"];
                cliente.Email = (string)reader["Email"];
                cliente.Senha = (string)reader["Senha"];
                cliente.Cpf = (string)reader["Cpf"];
                cliente.PorcentagemDesconto = (decimal)reader["PorcentagemDesconto"];



                lista.Add(cliente);
            }


            return lista;
        }

        // BUSCA DE CLIENTE POR NOME

        internal List<Cliente> Read(string nome)
        {
            List<Cliente> lista = new List<Cliente>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from ClientesVIP where Nome like @nome";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Cliente cliente = new Cliente();
                //cliente.ClienteId = reader.GetInt32(0);
                cliente.Pessoa_id = (int)reader["Pessoa_id"];
                cliente.Nome = (string)reader["Nome"];
                cliente.Email = (string)reader["Email"];
                cliente.Senha = (string)reader["Senha"];
                cliente.Cpf = (string)reader["Cpf"];
                cliente.PorcentagemDesconto = (decimal)reader["PorcentagemDesconto"];



                lista.Add(cliente);
            }

            return lista;

        }

        // BUSCA POR ID

        internal Cliente Read(int id)
        {
            Cliente cliente = new Cliente();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Cliente, Pessoas where cliente.id = @id AND pessoa.id = cliente.id";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
               cliente.Pessoa_id = (int)reader["Pessoa_id"];
                cliente.Nome = (string)reader["Nome"];
                cliente.Email = (string)reader["Email"];
                cliente.Senha = (string)reader["Senha"];
                cliente.Cpf = (string)reader["Cpf"];
                cliente.PorcentagemDesconto = (decimal)reader["PorcentagemDesconto"];
            }

            return cliente;

        }

        // ATUALIZAR CLIENTE
        public void Update(Cliente e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    UPDATE ClientesVIP set
                    Nome = @nome, Email = @email, Senha = @senha, Cpf = @cpf, PorcentagemDesconto = @porcentagemDesconto
                    WHERE
                    pessoa_id = @pessoa_id;
            ";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@senha", e.Senha);
            cmd.Parameters.AddWithValue("@cpf", e.Cpf);
            cmd.Parameters.AddWithValue("@porcentagemDesconto", e.PorcentagemDesconto);
            cmd.Parameters.AddWithValue("@id", e.Id);

            cmd.ExecuteNonQuery();

        }

        // APAGAR CLIENTE
        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    DELETE from ClientesVIP
                    WHERE
                    id = @id;
            ";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

        }

        // metodo login

        internal Cliente Read(string email, string senha)
        {
            Cliente c = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select * from Pessoas where email = @Email and senha = @Senha";


            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Senha", senha);


            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                c = new Cliente();
                //funcionario.funcionarioId = reader.GetInt32(0);
                c.Id = (int)reader["Id"];
                c.Nome = (string)reader["Nome"];
                c.Email = (string)reader["Email"];
            }

            return c;

        }

    }
}
