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
            Console.Write("\nDigite o ID do paciente para alterar: ");
            string idTexto = Console.ReadLine();
            int id;

            if (!int.TryParse(idTexto, out id))
            {
                Console.WriteLine("\nID inválido.");
                return;
            }

            try
            {
                MySqlConnection conn = new MySqlConnection(conexao);
                conn.Open();

                string select = "SELECT Nome, Preferencial FROM pacientes WHERE Id = @id";
                MySqlCommand cmdSel = new MySqlCommand(select, conn);
                cmdSel.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmdSel.ExecuteReader();

                if (!reader.Read())
                {
                    Console.WriteLine("\nPaciente não encontrado.");
                    reader.Close();
                    conn.Close();
                    return;
                }

                string nomeAtual = reader.GetString("Nome");
                bool prefAtual = reader.GetBoolean("Preferencial");
                reader.Close();

                Console.Write("\nNovo nome (atual: " + nomeAtual + "): ");
                string novoNome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(novoNome)) novoNome = nomeAtual;

                Console.Write("Paciente é preferencial? (atual: " + (prefAtual ? "Sim" : "Não") + ") (s/n): ");
                string resp = Console.ReadLine().ToLower();
                bool novoPref = (resp == "s") ? true : (resp == "n") ? false : prefAtual;

                string update = "UPDATE pacientes SET Nome=@nome, Preferencial=@pref WHERE Id=@id";
                MySqlCommand cmdUpd = new MySqlCommand(update, conn);
                cmdUpd.Parameters.AddWithValue("@nome", novoNome);
                cmdUpd.Parameters.AddWithValue("@pref", novoPref);
                cmdUpd.Parameters.AddWithValue("@id", id);

                cmdUpd.ExecuteNonQuery();

                Console.WriteLine("\nDados atualizados com sucesso");

                conn.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"\nErro ao atualizar:{ex.Message}\n");
            }
        }
    }
}
