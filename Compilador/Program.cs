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


            StreamReader codigo2 = new StreamReader(@"C:\Codigos\erro1.txt");
            Console.Write("Arquivo erro1.txt : ");
            AnalisadorLexico analisadorLexico2 = new AnalisadorLexico();
            Console.Write(analisadorLexico2.realizaAnaliseLexica(codigo2) ? "Passou no Léxico | " : "Não passou no Léxico | ");
            AnalisadorSintatico analisadorSintatico2 = new AnalisadorSintatico();
            Console.WriteLine(analisadorSintatico2.realizaAnaliseSintatica(analisadorLexico2) ? "Passou no Sintático" : "Não passou no Sintático");
            codigo2.Close();

            StreamReader codigo3 = new StreamReader(@"C:\Codigos\erro2.txt");
            Console.Write("Arquivo erro2.txt : ");
            AnalisadorLexico analisadorLexico3 = new AnalisadorLexico();
            Console.Write(analisadorLexico3.realizaAnaliseLexica(codigo3) ? "Passou no Léxico | " : "Não passou no Léxico | ");
            AnalisadorSintatico analisadorSintatico3 = new AnalisadorSintatico();
            Console.WriteLine(analisadorSintatico3.realizaAnaliseSintatica(analisadorLexico3) ? "Passou no Sintático" : "Não passou no Sintático");
            codigo3.Close();

            StreamReader codigo4 = new StreamReader(@"C:\Codigos\erro3.txt");
            Console.Write("Arquivo erro3.txt : ");
            AnalisadorLexico analisadorLexico4 = new AnalisadorLexico();
            Console.Write(analisadorLexico4.realizaAnaliseLexica(codigo4) ? "Passou no Léxico | " : "Não passou no Léxico | ");
            AnalisadorSintatico analisadorSintatico4 = new AnalisadorSintatico();
            Console.WriteLine(analisadorSintatico4.realizaAnaliseSintatica(analisadorLexico4) ? "Passou no Sintático" : "Não passou no Sintático");
            codigo4.Close();

            StreamReader codigo5 = new StreamReader(@"C:\Codigos\erro4.txt");
            Console.Write("Arquivo erro4.txt : ");
            AnalisadorLexico analisadorLexico5 = new AnalisadorLexico();
            Console.Write(analisadorLexico5.realizaAnaliseLexica(codigo5) ? "Passou no Léxico | " : "Não passou no Léxico | ");
            AnalisadorSintatico analisadorSintatico5 = new AnalisadorSintatico();
            Console.WriteLine(analisadorSintatico5.realizaAnaliseSintatica(analisadorLexico5) ? "Passou no Sintático" : "Não passou no Sintático");
            codigo5.Close();

            StreamReader codigo6 = new StreamReader(@"C:\Codigos\erro5.txt");
            Console.Write("Arquivo erro5.txt : ");
            AnalisadorLexico analisadorLexico6 = new AnalisadorLexico();
            Console.Write(analisadorLexico6.realizaAnaliseLexica(codigo6) ? "Passou no Léxico | " : "Não passou no Léxico | ");
            AnalisadorSintatico analisadorSintatico6 = new AnalisadorSintatico();
            Console.WriteLine(analisadorSintatico6.realizaAnaliseSintatica(analisadorLexico6) ? "Passou no Sintático" : "Não passou no Sintático");
            codigo5.Close();

            StreamReader codigo7 = new StreamReader(@"C:\Codigos\erro6.txt");
            Console.Write("Arquivo erro6.txt : ");
            AnalisadorLexico analisadorLexico7 = new AnalisadorLexico();
            Console.Write(analisadorLexico7.realizaAnaliseLexica(codigo7) ? "Passou no Léxico" : "Não passou no Léxico");

            Console.ReadKey();
        }
    }
}
