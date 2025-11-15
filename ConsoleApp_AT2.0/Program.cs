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
        public static void Main(string[] args)
        {
            for (; ; )
            {
                try
                {
                    Console.Write("[Menu de Atendimento Hospitalar]\n\n1 - Cadastrar paciente\n2 - Listar fila de pacientes\n3 - Atender paciente\n4 - Alterar dados do paciente\nQ - Sair\n\nEscolha uma opção:");
                    string OpçãoDoUsuario = Console.ReadLine();

                    switch (OpçãoDoUsuario)
                    {
                        case "1":
                            new Cadastrar().CadastrarPaciente();
                            break;
                        case "2":
                            new Listar().ListarPacientes();
                            break;
                        case "3":
                            new Atender().AtenderPaciente();
                            break;
                        case "4":
                            new Atualizar().AlterarPaciente();
                            break;
                        case "Q":
                        case "q":
                            Console.WriteLine("\nSoftware Finalizado :)");
                            return;
                        default:
                            Console.WriteLine("\n(Opção inválida, tente novamente)\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nErro: " + ex.Message + "\n");
                }
            }
        }
    }
}