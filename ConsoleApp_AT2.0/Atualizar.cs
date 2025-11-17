using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_AT2._0
{
    internal class Atualizar
    {
        string conexao = "server=localhost;uid=root;pwd=root;database=hospital;port=3306";

        public void AlterarPaciente()
        {
            try
            {
                Console.Write("\nDigite o ID do paciente:");
                string texto = Console.ReadLine();
                int id;
                if (!int.TryParse(texto, out id))
                {
                    Console.WriteLine("\nID inválido\n");
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();

                    string select = "select nome, preferencial from pacientes where id=@id";
                    MySqlCommand cmdSel = new MySqlCommand(select, conn);
                    cmdSel.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = cmdSel.ExecuteReader();

                    if (!reader.Read())
                    {
                        Console.WriteLine("\nPaciente não encontrado\n");
                        return;
                    }

                    string nome = reader.GetString("nome");
                    bool pref = reader.GetBoolean("preferencial");
                    reader.Close();

                    Console.Write("\nNovo nome (Nome atual: " + nome + "):");
                    string novo = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(novo)) novo = nome;

                    Console.Write("\nÉ preferencial? (atual: " + (pref ? "Preferencial" : "Não Preferencial") + ") (s ou n):");
                    string resp = Console.ReadLine().ToLower();
                    bool novoPref = resp == "s" ? true : resp == "n" ? false : pref;

                    MySqlCommand upd = new MySqlCommand("update pacientes set nome=@n, preferencial=@p where id=@id", conn);
                    upd.Parameters.AddWithValue("@n", novo);
                    upd.Parameters.AddWithValue("@p", novoPref);
                    upd.Parameters.AddWithValue("@id", id);
                    upd.ExecuteNonQuery();

                    AtualizarFila(conn);
                    Console.WriteLine("\nPaciente atualizado com sucesso\n");
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
                if (total >= tamanho)
                {
                    Console.WriteLine("\nA fila está cheia! Máximo permitido: 15 pacientes.\n");
                    break;
                }

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
