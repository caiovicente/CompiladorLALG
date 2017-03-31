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
            {
                token = analisadorLexico.retornaToken();
                if (token.tipo == "Identificador")
                {
                    token = analisadorLexico.retornaToken();
                    if (corpo())
                    {
                        token = analisadorLexico.retornaToken();
                        if (token.id == ".")
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private bool corpo()
        {
            token = analisadorLexico.retornaToken();
            if (declaracao())
            {
                if (token.id == "begin")
                {
                    if (comandos())
                    {
                        token = analisadorLexico.retornaToken();
                        if (token.id == "end")
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private bool declaracao()
        {
            if (token.id != "begin")
            {
                if (declaraVariavel())
                {
                    if (declaraProcedimento())
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return true;
        }

        private bool declaraVariavel()
        {
            if (token.id == "var")
            {
                token = analisadorLexico.retornaToken();
                if (variaveis())
                {
                    token = analisadorLexico.retornaToken();
                    if (token.id == ":")
                    {
                        token = analisadorLexico.retornaToken();
                        if (tipoDeVariavel())
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return true;
        }

        private bool variaveis()
        {
            return true;
        }

        private bool tipoDeVariavel()
        {
            return true;
        }

        private bool declaraProcedimento()
        {
            if (token.id == "procedure")
            {
                //todo
            }
            return true;
        }

        private bool comandos()
        {
            return true;
        }
    }
}