using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_AT2._0
{
    internal class Listar
    {
        string conexao = "server=localhost;uid=root;pwd=root;database=hospital;port=3306";

        public void ListarPacientes()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(conexao);
                conn.Open();

                string sql = "SELECT Id, Nome, Preferencial, NumeroFila FROM pacientes ORDER BY Preferencial DESC, NumeroFila ASC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("\nNenhum paciente na fila.\n");
                    reader.Close();
                    conn.Close();
                    return;
                }

                Console.WriteLine("\n[Fila de Pacientes]");
                while (reader.Read())
                {
                    int id = reader.GetInt32("Id");
                    string nome = reader.GetString("Nome");
                    bool pref = reader.GetBoolean("Preferencial");
                    int fila = reader.GetInt32("NumeroFila");

                    string tipo = pref ? "Preferencial" : "Normal";
                    Console.WriteLine(fila + " - " + nome + " (" + tipo + ") [ID: " + id + "]");
                }

                reader.Close();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"\nErro ao listar:{ex.Message}\n");
            }
        }
    }
}
