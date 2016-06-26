using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using ComandaEletronica.Entity;

namespace ComandaEletronica.Models
{
    public class FuncionarioModel : Model
    {

        // CADASTRAR Funcionario
        public void Create(Funcionario f)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    INSERT INTO Pessoas
                    (nome, email, senha, cpf)
                    VALUES
                    (@nome, @email, @senha, @cpf)
                    INSERT INTO Funcionarios
                    (pessoa_id, cargo, salario, horarioEntrada, horarioSaida)
                    VALUES
                    (@@IDENTITY, @cargo, @salario, @horarioEntrada, @horarioSaida)
            ";

            cmd.Parameters.AddWithValue("@nome", f.Nome);
            cmd.Parameters.AddWithValue("@email", f.Email);
            cmd.Parameters.AddWithValue("@senha", f.Senha);
            cmd.Parameters.AddWithValue("@cpf", f.Cpf);
            cmd.Parameters.AddWithValue("@cargo", f.Cargo);
            cmd.Parameters.AddWithValue("@salario", f.Salario);
            cmd.Parameters.AddWithValue("@horarioEntrada", f.HorarioEntrada);
            cmd.Parameters.AddWithValue("@horarioSaida", f.HorarioSaida);

            cmd.ExecuteNonQuery();

        }

        // LISTANDO FUNCIONARIOS (SELECT)

        public List<Funcionario> Read()
        {

            List<Funcionario> lista = new List<Funcionario>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @" SELECT p.id, p.nome, p.email, p.senha, p.cpf, f.cargo, f.salario, f.horarioEntrada, f.horarioSaida
                                FROM Pessoas p, Funcionarios f
                                WHERE p.id = f.pessoa_id";


            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                Funcionario f = new Funcionario();
                //funcionario.FuncionarioId = reader.GetInt32(0);
                f.Id = (int)reader["Id"];
                f.Nome = (string)reader["Nome"];
                f.Email = (string)reader["Email"];
                f.Senha = (string)reader["Senha"];
                f.Cpf = (string)reader["Cpf"];
                f.Cargo = (string)reader["Cargo"];
                f.Salario = (Decimal)reader["Salario"];
                f.HorarioEntrada = (DateTime)reader["HorarioEntrada"];
                f.HorarioSaida = (DateTime)reader["HorarioSaida"];

                lista.Add(f);
            }


            return lista;
        }

        // BUSCA DE FUNCIONARIOS POR NOME

        internal List<Funcionario> Read(string nome)
        {
            List<Funcionario> lista = new List<Funcionario>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Pessoas where nome like @nome";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Funcionario f = new Funcionario();
                //funcionario.funcionarioId = reader.GetInt32(0);
                f.Id = (int)reader["Id"];
                f.Nome = (string)reader["Nome"];
                f.Email = (string)reader["Email"];
                f.Senha = (string)reader["Senha"];
                f.Cpf = (string)reader["Cpf"];
                f.Cargo = (string)reader["Cargo"];
                f.Salario = (decimal)reader["Salario"];
                f.HorarioEntrada = (DateTime)reader["HorarioEntrada"];
                f.HorarioSaida = (DateTime)reader["HorarioSaida"];



                lista.Add(f);
            }

            return lista;

        }

        // BUSCA POR ID

        internal Funcionario Read(int pessoa_id)
        {
            Funcionario funcionario = new Funcionario();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from Funcionarios, Pessoas  where pessoa_id = @pessoa_id";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@pessoa_id", pessoa_id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                funcionario.Id = (int)reader["Id"];
                funcionario.Nome = (string)reader["Nome"];
                funcionario.Email = (string)reader["Email"];
                funcionario.Senha = (string)reader["Senha"];
            }

            return funcionario;

        }

        // ATUALIZAR FUNCIONARIO
        public void Update(Funcionario e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    UPDATE Funcionarios set
                    Nome = @nome, Email = @email, Senha = @senha, Cpf = @cpf, Cargo = @cargo, Salario = @salario, HorarioEntrada = @horarioEntrada, HorarioSaida = @horarioSaida
                    WHERE
                    id = @id;
            ";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@senha", e.Senha);
            cmd.Parameters.AddWithValue("@cpf", e.Cpf);
            cmd.Parameters.AddWithValue("@cargo", e.Cargo);
            cmd.Parameters.AddWithValue("@salario", e.Salario);
            cmd.Parameters.AddWithValue("@horarioEntrada", e.HorarioEntrada);
            cmd.Parameters.AddWithValue("@horarioSaida", e.HorarioSaida);

            cmd.ExecuteNonQuery();
        }

        // APAGAR FUNCIONARIO
        public void Delete(int idFuncionario)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                    DELETE from Funcionarios
                    WHERE
                    id = @id;
            ";

            cmd.Parameters.AddWithValue("@id", idFuncionario);

            cmd.ExecuteNonQuery();

        }

        // metodo login

        internal Funcionario Read(string email, string senha)
        {
            Funcionario f = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select * from Pessoas where email = @Email and senha = @Senha";


            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Senha", senha);


            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                f = new Funcionario();
                //funcionario.funcionarioId = reader.GetInt32(0);
                f.Id = (int)reader["Id"];
                f.Nome = (string)reader["Nome"];
                f.Email = (string)reader["Email"];
            }

            return f;

        }

    }
}
