using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_AT2._0
{
    class paciente
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string preferencial { get; set; }
        public int numerofila { get; set; }

        public void CadastrarPaciente()
        {          
                Console.Write("\nDigite o nome do Paciente:");
                nome = Console.ReadLine();

                Console.Write("\nÉ Preferencial ? (s ou n):");
                preferencial = Console.ReadLine();         
        }
        public void ListarPacientes()
        {
            Console.WriteLine("\n" + numerofila + "-" + nome + "(" + preferencial + ")" + "[" + id + "]");
        }
    }
}
