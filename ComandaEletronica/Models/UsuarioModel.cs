using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ComandaEletronica.Entity;
using System.Data.SqlClient;

namespace ComandaEletronica.Models
{
    public class UsuarioModel : Model
    {
        internal Pessoa Read(string email, string senha)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select * from Usuarios where email = @Email and senha = @Senha";


            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Senha", senha);


            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string tipo = (string)reader["Tipo"];

                switch (tipo)
                {
                    case "Funcionario":
                        Funcionario f = new Funcionario();
                        f.Id = (int)reader["Id"];
                        f.Nome = (string)reader["Nome"];
                        f.Email = (string)reader["Email"];
                        return f;
                    case "Cliente":
                        Cliente c = new Cliente();
                        c.Id = (int)reader["Id"];
                        c.Nome = (string)reader["Nome"];
                        c.Email = (string)reader["Email"];
                        return c;
                }

            }

            return null;

        }
    }
}
