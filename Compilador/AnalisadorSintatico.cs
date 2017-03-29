using System;

namespace Compilador
{
    public class AnalisadorSintatico
    {
        private AnalisadorLexico analisadorLexico;
        private Token token;
        private string escopo = "global";
        private Simbolo tabelaSimbolo;
        private Parametro tabelaParametro;
        

        public bool realizaAnaliseSintatica(AnalisadorLexico analisadorLexico)
        {
            token = new Token();
            this.analisadorLexico = analisadorLexico;
            if (programa())
                return true;
            return false;
        }
        
        private bool programa()
        {
            token = analisadorLexico.retornaToken();
            if (token.id == "program")
                token = analisadorLexico.retornaToken();
            else
            {
                Console.WriteLine("Falta identificador de inicio 'program'");
                return false;
            }


            if (token.tipo == "Identificador")
                token = analisadorLexico.retornaToken();
            else
            {
                Console.WriteLine("Nome do programa faltando.");
                return false;
            }
            if (corpo())
            {
                Console.WriteLine("Parte 1 passou");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool corpo()
        {
            return true;
        }
    }
}