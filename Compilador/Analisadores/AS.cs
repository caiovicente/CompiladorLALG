using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Analisadores
{
    class AS
    {
        private AnalisadorLexico analisadorLexico;
        private Token token;
        private Simbolo tabelaSimbolo = new Simbolo();
        private string escopo = "global";


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
            lerProximoToken();
            if (token.id == "program")
            {
                lerProximoToken();
                if (token.tipo == "ident")
                {
                    insereSimbolo(token.id, "ident_programa", "", "");
                    lerProximoToken();
                    if (corpo())
                    {
                        escreva("Passou coupo()");
                        lerProximoToken();
                        if (token.id == ".")
                        {
                            escreva("Código correto!");
                            return true;
                        }
                        escreva("Erro 4");
                        return false;
                    }
                    escreva("Erro 3");
                    return false;
                }
                escreva("Erro 2");
                return false;
            }
            escreva("Erro 1");
            return false;
        }

        private bool corpo()
        {
            if (dc())
            {

            }
        }

        private bool insereSimbolo(string nome, string categoria, string escopo, string tipo)
        {
            Simbolo simbolo = new Simbolo(nome, categoria, escopo, tipo);
            Simbolo aux;

            if (tabelaSimbolo.proximoSimbolo == null)
            {
                tabelaSimbolo.proximoSimbolo = simbolo;
                return true;
            }
            if (buscaSimbolo(nome, escopo))
            {
                escreva("A variável '{0}' já foi declarada nesse escopo", nome);
                return false;
            }
            aux = tabelaSimbolo.proximoSimbolo;
            while (aux.proximoSimbolo != null)
            {
                aux = aux.proximoSimbolo;
            }
            aux.proximoSimbolo = simbolo;
            return true;
        }

        private bool buscaSimbolo(string nome, string escopo)
        {
            Simbolo simbolo = tabelaSimbolo.proximoSimbolo;
            while(simbolo != null)
            {
                if (simbolo.nome == nome && simbolo.escopo == escopo)
                {
                    return true;
                }
                simbolo = simbolo.proximoSimbolo;
            }
            return false;
        }

        private void escreva(string erro)
        {
            Console.WriteLine(erro);
        }

        private void escreva(string erro, string par)
        {
            Console.WriteLine(erro, par);
        }

        private void lerProximoToken()
        {
            token = analisadorLexico.retornaToken();
        }
    }
}
