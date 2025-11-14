using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_AT2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection conexao = new MySqlConnection("server=localhost;uid=root;pwd=;database=hosp;port=3307");

            for (; ; )
            {
                try
                {
                    int auxiliar = 0;
                    int tamanho = 15;
                    paciente[] espaço = new paciente[tamanho];

                    Console.Write("[Menu de Atendimento Hospitalar]\n\n1 - Cadastrar paciente\n2 - Listar fila de pacientes\n3 - Atender paciente\n4 - Alterar dados do paciente\nQ - Sair\n\nEscolha uma opção:");
                    string digitodoUsuário = Console.ReadLine();

                    if (digitodoUsuário == "1")
                    {
                        if (auxiliar >= tamanho)
                        {
                            Console.WriteLine("Fila Está lotada, espere os Pacientes serem atendidos");
                        }
                        else if (auxiliar < tamanho)
                        {
                            conexao.Open();

                            paciente NovoPaciente = new paciente();
                            NovoPaciente.CadastrarPaciente();
                            NovoPaciente.numerofila = auxiliar + 1;

                            string insert = "INSERT INTO paciente (nome, preferencial, numerofila) VALUES (@nome, @preferencial, @numerofila)";
                            MySqlCommand cmd = new MySqlCommand(insert, conexao);
                            cmd.Parameters.AddWithValue("@nome", NovoPaciente.nome);
                            cmd.Parameters.AddWithValue("@preferencial", NovoPaciente.preferencial);
                            cmd.Parameters.AddWithValue("@numerofila", NovoPaciente.numerofila);
                            cmd.ExecuteNonQuery();

                            Console.Write("\nPaciente Registrado\n\n");

                            conexao.Close();
                        }
                    }
                    else if (digitodoUsuário == "2")
                    {
                        conexao.Open();
                        string select = "SELECT * FROM paciente ORDER BY preferencial DESC, numerofila ASC";
                        MySqlCommand promptdecomando = new MySqlCommand(select, conexao);
                        MySqlDataReader leitor = promptdecomando.ExecuteReader();
                        Console.WriteLine("\n[Lista de Pacientes na Fila de Atendimento]\n");
                        while (leitor.Read())
                        {
                            Console.WriteLine("ID: " + leitor["id"] + " | Nome: " + leitor["nome"] + " | Preferencial: " + leitor["preferencial"] + " | Número da Fila: " + leitor["numerofila"] + "\n");
                        }
                    }
                    else if (digitodoUsuário == "3")
                    {
                        Console.Write("\nManutenção...\n\n");
                    }
                    else if (digitodoUsuário == "4")
                    {
                        Console.Write("\nManutenção...\n\n");
                    }
                    else if (digitodoUsuário == "Q" || digitodoUsuário == "q")
                    {
                        Console.Write("\nDepuração Finalizada :)\n"); return;
                    }
                    else
                    {
                        Console.WriteLine("\nTecla errada, Tente outras do MENU.\n");
                    }
                }
                catch (Exception erro)
                {
                    Console.WriteLine("\nERROR 404: " + erro.Message + "\n");
                }
            }
        }
    }
}
