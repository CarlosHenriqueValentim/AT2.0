using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_AT2._0
{
    class Listar
    {
        string conexao = "server=localhost;uid=root;pwd=root;database=hospital;port=3307";

        public void ListarPacientes()
        {
            try
            {
                using (MySqlConnection conec = new MySqlConnection(conexao))
                {
                    conec.Open();
                    string sql = "select id, nome, preferencial, numerofila from pacientes order by preferencial desc, numerofila asc";
                    MySqlCommand cmd = new MySqlCommand(sql, conec);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("\nNenhum paciente na fila\n");
                        return;
                    }

                    Console.WriteLine("\n[Fila de Pacientes]\n");
                    while (reader.Read())
                    {
                        int fila = reader.GetInt32("numerofila");
                        string nome = reader.GetString("nome");
                        bool pref = reader.GetBoolean("preferencial");
                        int id = reader.GetInt32("id");
                        Console.WriteLine(fila + " - " + nome + " (" + (pref ? "Preferencial" : "Não Preferencial") + ") [ID: " + id + "]");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("\nErro: " + erro.Message + "\n");
            }
        }
    }
}