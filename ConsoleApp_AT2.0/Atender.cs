using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_AT2._0
{
    class Atender
    {
        string conexao = "server=localhost;uid=root;pwd=root;database=hospital;port=3306";

        public void AtenderPaciente()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string select = "select id, nome from pacientes order by preferencial desc, numerofila asc limit 1";
                    MySqlCommand cmd = new MySqlCommand(select, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        Console.WriteLine("\nNenhum paciente na fila\n");
                        reader.Close();
                        return;
                    }

                    int id = reader.GetInt32("id");
                    string nome = reader.GetString("nome");
                    reader.Close();

                    MySqlCommand del = new MySqlCommand("delete from pacientes where id=@id", conn);
                    del.Parameters.AddWithValue("@id", id);
                    del.ExecuteNonQuery();

                    MySqlCommand cnt = new MySqlCommand("select count(*) from pacientes", conn);
                    int total = Convert.ToInt32(cnt.ExecuteScalar());

                    if (total == 0)
                    {
                        MySqlCommand reset = new MySqlCommand("alter table pacientes auto_increment = 1", conn);
                        reset.ExecuteNonQuery();
                        Console.WriteLine("\nPaciente atendido: " + nome);
                    }
                    else
                    {
                        AtualizarFila(conn);
                        Console.WriteLine("\nPaciente atendido: " + nome + "\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nErro: " + ex.Message + "\n");
            }
        }

        private void AtualizarFila(MySqlConnection conn)
        {
            string select = "select id from pacientes order by preferencial desc, id asc";
            MySqlCommand cmd = new MySqlCommand(select, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            int tamanho = 15;
            int[] ids = new int[tamanho];
            int total = 0;

            while (reader.Read())
            {
                ids[total] = reader.GetInt32("id");
                total++;
            }
            reader.Close();

            int pos = 1;
            for (int i = 0; i < total; i++)
            {
                MySqlCommand update = new MySqlCommand("update pacientes set numerofila=@n where id=@id", conn);
                update.Parameters.AddWithValue("@n", pos++);
                update.Parameters.AddWithValue("@id", ids[i]);
                update.ExecuteNonQuery();
            }
        }
    }
}
