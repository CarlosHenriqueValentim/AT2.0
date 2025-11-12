using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_AT2._0
{
    internal class Atender
    {
        string conexao = "server=localhost;uid=root;pwd=root;database=hospital;port=3306";

        public void AtenderPaciente()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(conexao);
                conn.Open();

                string sqlSelect = "SELECT Id, Nome FROM pacientes ORDER BY Preferencial DESC, NumeroFila ASC LIMIT 1";
                MySqlCommand cmdSelect = new MySqlCommand(sqlSelect, conn);
                MySqlDataReader reader = cmdSelect.ExecuteReader();

                if (!reader.Read())
                {
                    Console.WriteLine("\nNenhum paciente na fila.");
                    reader.Close();
                    conn.Close();
                    return;
                }

                int id = reader.GetInt32("Id");
                string nome = reader.GetString("Nome");

                reader.Close();

                string sqlDelete = "DELETE FROM pacientes WHERE Id = @id";
                MySqlCommand cmdDelete = new MySqlCommand(sqlDelete, conn);
                cmdDelete.Parameters.AddWithValue("@id", id);
                cmdDelete.ExecuteNonQuery();

                Console.WriteLine("\nAtendendo paciente: " + nome);

                conn.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("\nErro ao atender: " + ex.Message);
            }
        }
    }
}
