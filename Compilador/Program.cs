using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader codigo = new StreamReader(@"C:\Users\Caio\Desktop\teste.txt");
            
            AnalisadorLexico analisadorLexico = new AnalisadorLexico();
            Console.WriteLine(analisadorLexico.realizaAnaliseLexica(codigo) ? "Passou no Léxico" : "Não passou no Léxico");

            AnalisadorSintatico analisadorSintatico = new AnalisadorSintatico();
            Console.WriteLine(analisadorSintatico.realizaAnaliseSintatica(analisadorLexico) ? "Passou no Sintático" : "Não passou no Sintático");
            //analisadorLexico.setaInicioToken();
            //Token tk = analisadorLexico.retornaToken();
            //while (tk != null)
            //{
            //    Console.WriteLine(tk.id + "\t|  " + tk.tipo);
            //    tk = tk.proximoToken;
            //}
            codigo.Close();
            Console.ReadKey();
        }
    }
}
