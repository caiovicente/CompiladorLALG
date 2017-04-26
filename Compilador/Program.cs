using System;
using System.IO;

namespace Compilador
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader codigo1 = new StreamReader(@"C:\Codigos\certo.txt");
            Console.Write("Arquivo certo.txt : ");
            AnalisadorLexico analisadorLexico1 = new AnalisadorLexico();
            Console.Write(analisadorLexico1.realizaAnaliseLexica(codigo1) ? "Passou no Léxico | " : "Não passou no Léxico | ");
            AnalisadorSintatico analisadorSintatico1 = new AnalisadorSintatico();
            Console.WriteLine(analisadorSintatico1.realizaAnaliseSintatica(analisadorLexico1) ? "Passou no Sintático" : "Não passou no Sintático");
            codigo1.Close();

            Console.ReadKey();
        }
    }
}
