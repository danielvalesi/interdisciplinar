using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using ComandaEletronica.Entity;

namespace ComandaEletronica.Models
{
    public class LocalizacaoModel : Model
    {

        // CADASTRAR LOCALIZAÇÃO
        public void Create(Localizacao e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    INSERT INTO Localizacoes
                    (qtdLugares, descricao, status )
                    VALUES
                    (@qtdLugares, @descricao, @status)
            ";

            cmd.Parameters.AddWithValue("@qtdLugares", e.QtdLugares);
            cmd.Parameters.AddWithValue("@descricao", e.Descricao);
            cmd.Parameters.AddWithValue("@status", e.Status);

            cmd.ExecuteNonQuery();

        }

        // LISTANDO LOCALIZAÇÃOS (SELECT)

        public List<Localizacao> Read()
        {
            List<Localizacao> lista = new List<Localizacao>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Localizacoes";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Localizacao localizacao = new Localizacao();
                //cliente.ClienteId = reader.GetInt32(0);
                localizacao.Numero = (int)reader["Numero"];
                localizacao.QtdLugares = (int)reader["QtdLugares"];
                localizacao.Descricao = (string)reader["Descricao"];
                localizacao.Status = (Status)reader["Status"];


                lista.Add(localizacao);
            }


            return lista;
        }

        // BUSCA DE LOCALIZAÇÃO POR STATUS

        internal List<Localizacao> Read(string status)
        {
            List<Localizacao> lista = new List<Localizacao>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Localizacoes where status like @status";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@status", "%" + status + "%");

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Localizacao localizacao = new Localizacao();
                //cliente.ClienteId = reader.GetInt32(0);
                localizacao.Numero = (int)reader["Numero"];
                localizacao.QtdLugares = (int)reader["QtdLugares"];
                localizacao.Descricao = (string)reader["Descricao"];
                localizacao.Status = (Status)reader["Status"];


                lista.Add(localizacao);
            }

            return lista;

        }

        // BUSCA POR ID

        internal Localizacao Read(int numero)
        {
            Localizacao localizacao = new Localizacao();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Localizacoes where numero = @numero";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@numero", numero);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                localizacao.Numero = (int)reader["Numero"];
                localizacao.QtdLugares = (int)reader["QtdLugares"];
                localizacao.Descricao = (string)reader["Descricao"];
                localizacao.Status = (Status)reader["Status"];
            }

            return localizacao;

        }

        // ATUALIZAR LOCALIZAÇÃO
        public void Update(Localizacao e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    UPDATE Localizacoes set
                    qtdLugares = @qtdLugares, descricao = @descricao, status = @status
                    WHERE
                    numero = @numero;
            ";

            cmd.Parameters.AddWithValue("@qtdLugares", e.QtdLugares);
            cmd.Parameters.AddWithValue("@descricao", e.Descricao);
            cmd.Parameters.AddWithValue("@status", e.Status);

            cmd.ExecuteNonQuery();

        }

        // APAGAR LOCALIZAÇÃO
        public void Delete(int numero)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    DELETE from Localizacoes
                    WHERE
                    numero = @numero;
            ";

            cmd.Parameters.AddWithValue("@numero", numero);

            cmd.ExecuteNonQuery();

        }

    }
}
