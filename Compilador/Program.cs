using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader codigo = new StreamReader(@"C:\Users\33236\Desktop\teste.txt");
            AnalisadorLexico analisadorLexico = new AnalisadorLexico();
            Console.WriteLine(analisadorLexico.geraToken(codigo) ? "Passou no Léxico" : "Não passou no Léxico");

            codigo.Close();
            Console.ReadKey();
        }
    }
}
