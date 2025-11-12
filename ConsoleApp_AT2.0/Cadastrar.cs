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
            Console.Write("\nDigite o nome do paciente:");
            string nome = Console.ReadLine();

            Console.Write("\nO paciente é preferencial? (s/n):");
            string resp = Console.ReadLine().ToLower();
            bool preferencial = (resp == "s");

            int numeroFila = 1;

            try
            {
                MySqlConnection conn = new MySqlConnection(conexao);
                conn.Open();

                string sqlUltimo = "SELECT IFNULL(MAX(NumeroFila), 0) FROM pacientes";
                MySqlCommand cmdUltimo = new MySqlCommand(sqlUltimo, conn);
                numeroFila = Convert.ToInt32(cmdUltimo.ExecuteScalar()) + 1;

                string sqlInsert = "INSERT INTO pacientes (Nome, Preferencial, NumeroFila) VALUES (@nome, @preferencial, @numero)";
                MySqlCommand cmd = new MySqlCommand(sqlInsert, conn);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@preferencial", preferencial);
                cmd.Parameters.AddWithValue("@numero", numeroFila);

                cmd.ExecuteNonQuery();
                Console.WriteLine("\nPaciente cadastrado com sucesso");

                conn.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"\nErro ao cadastrar:{ex.Message}\n");
            }
        }
    }
}
