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
            StreamReader codigo = new StreamReader(@"C:\Programa\teste.txt");
            AnalisadorLexico anLex = new AnalisadorLexico();
            Console.WriteLine(anLex.geraToken(codigo) ? "Passou no Léxico" : "Não passou no Léxico");
        }
    }
}
