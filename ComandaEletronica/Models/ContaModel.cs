using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using ComandaEletronica.Entity;

namespace ComandaEletronica.Models
{
    public class ContaModel : Model
    {

        // CADASTRAR CONTA
        public void Create(Conta e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    INSERT INTO Contas
                    (localizacao_id, cliente_id, dataAbertura, dataFechamento, valor, status, formaPagamento )
                    VALUES
                    (@localizacao_id, @cliente_id, @dataAbertura, @dataFechamento, @valor, @status, @formaPagamento)
            ";

            cmd.Parameters.AddWithValue("@localizacao_id", e.Localizacao_id);
            cmd.Parameters.AddWithValue("@cliente_id", e.Cliente_id);
            cmd.Parameters.AddWithValue("@dataAbertura", e.DataAbertura);
            cmd.Parameters.AddWithValue("@dataFechamento", e.DataFechamento);
            cmd.Parameters.AddWithValue("@valor", e.Valor);
            cmd.Parameters.AddWithValue("@status", e.Status);
            cmd.Parameters.AddWithValue("@formaPagamento", e.FormaPagamento);

            cmd.ExecuteNonQuery();

        }

        // LISTANDO CONTAS (SELECT)

        public List<Conta> Read()
        {
            List<Conta> lista = new List<Conta>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Contas";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Conta conta = new Conta();
                //cliente.ClienteId = reader.GetInt32(0);
                conta.Id = (int)reader["Id"];
                conta.Localizacao_id = (int)reader["Localizacao_id"];
                conta.Cliente_id = (int)reader["Cliente_id"];
                conta.DataAbertura = (DateTime)reader["DataAbertura"];

                // Permite nullo
                conta.DataFechamento = (DateTime?)(reader["DataFechamento"] != DBNull.Value ? reader["DataFechamento"] : null);

                conta.Valor = (decimal)(reader["Valor"] == DBNull.Value ? 0.0m : reader["Valor"]);
                conta.Status = (StatusConta)reader["Status"];
                conta.FormaPagamento = (FormaPagamento)reader["FormaPagamento"];


                lista.Add(conta);
            }


            return lista;
        }

        // BUSCA DE CONTA POR NOME

        internal List<Conta> Read(string status)
        {
            List<Conta> lista = new List<Conta>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Contas where status like @status";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@status", "%" + status + "%");

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Conta conta = new Conta();
                //cliente.ClienteId = reader.GetInt32(0);
                conta.Id = (int)reader["Id"];
                conta.Localizacao_id = (int)reader["Localizacao_id"];
                conta.Cliente_id = (int)reader["Cliente_id"];
                conta.DataAbertura = (DateTime)reader["DataAbertura"];
                conta.DataFechamento = (DateTime)reader["DataFechamento"];
                conta.Valor = (decimal)reader["Valor"];
                conta.Status = (StatusConta)reader["StatusConta"];
                conta.FormaPagamento = (FormaPagamento)reader["FormaPagamento"];


                lista.Add(conta);
            }

            return lista;

        }

        // BUSCA POR ID

        internal Conta Read(int id)
        {
            Conta conta = new Conta();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Contas where id = @id";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                conta.Id = (int)reader["Id"];
                conta.Localizacao_id = (int)reader["Localizacao_id"];
                conta.Cliente_id = (int)reader["Cliente_id"];
                conta.DataAbertura = (DateTime)reader["DataAbertura"];
                conta.DataFechamento = (DateTime)reader["DataFechamento"];
                conta.Valor = (decimal)reader["Valor"];
                conta.Status = (StatusConta)reader["Status"];
                conta.FormaPagamento = (FormaPagamento)reader["FormaPagamento"];
            }

            return conta;

        }

        // ATUALIZAR CONTA
        public void Update(Conta e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    UPDATE Contas set
                    localizacao_id = @localizacao_id, cliente_id = @cliente_id, dataAbertura = @dataAbertura, dataFechamento = @dataFechamento, valor = @valor, status = @status, formaPagamento = @formaPagamento
                    WHERE
                    id = @id;
            ";

            cmd.Parameters.AddWithValue("@localizacao_id", e.Localizacao_id);
            cmd.Parameters.AddWithValue("@cliente_id", e.Cliente_id);
            cmd.Parameters.AddWithValue("@dataAbertura", e.DataAbertura);
            cmd.Parameters.AddWithValue("@dataFechamento", e.DataFechamento);
            cmd.Parameters.AddWithValue("@valor", e.Valor);
            cmd.Parameters.AddWithValue("@status", e.Status);
            cmd.Parameters.AddWithValue("@formaPagamento", e.FormaPagamento);
            cmd.Parameters.AddWithValue("@id", e.Id);

            cmd.ExecuteNonQuery();

        }

        // APAGAR CONTA
        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    DELETE from Contas
                    WHERE
                    id = @id;
            ";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

        }

    }
}
