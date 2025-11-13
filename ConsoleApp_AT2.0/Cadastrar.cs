using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_AT2._0
{
    internal class Cadastrar
    {
        string conexao = "server=localhost;uid=root;pwd=root;database=hospital;port=3306";

        public void CadastrarPaciente()
        {
            try
            {
                Console.Write("\nDigite o nome do paciente:");
                string nome = Console.ReadLine();

                Console.Write("\nO paciente é preferencial? (s ou n):");
                string resp = Console.ReadLine().ToLower();
                bool preferencial = resp == "s";

                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();
                    string insert = "insert into pacientes (nome, preferencial, numerofila) values (@n, @p, 0)";
                    MySqlCommand cmd = new MySqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@n", nome);
                    cmd.Parameters.AddWithValue("@p", preferencial);
                    cmd.ExecuteNonQuery();

                    AtualizarFila(conn);
                    Console.WriteLine("\nPaciente cadastrado com sucesso\n");
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

            int[] ids = new int[1000];
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
