using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class AS
    {
        private AnalisadorLexico analisadorLexico;
        private Token token;
        private string escopo = "global";
        private Simbolo tabelaSimbolo;
        private Parametro tabelaParametro;

        private void escreva(string erro)
        {
            Console.WriteLine(erro);
        }

        private void lerProximoToken()
        {
            token = analisadorLexico.retornaToken();
        }

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
                if (token.tipo == "Identificador")
                {
                    if (corpo())
                    {
                        lerProximoToken();
                        if (token.id == ".")
                        {
                            return true;
                        }
                        escreva("Falta identificadoo '.'");
                        return false;
                    }
                    return false;
                }
                escreva("Falta identificador de nome do programa");
                return false;
            }
            escreva("Falta identificador 'program'");
            return false;
        }

        private bool corpo()
        {
            if (dc())
            {
                lerProximoToken();
                if (token.id == "begin")
                {
                    if (comandos())
                    {
                        lerProximoToken();
                        if (token.id == "end")
                        {
                            return true;
                        }
                        escreva("Falta identificador 'end'");
                        return false;
                    }
                    return false;
                }
                escreva("Falta identificador 'begin'");
                return false;
            }
            return false;
        }

        private bool dc()
        {
            if (dc_v())
            {
                if (mais_dc())
                {
                    //return true;
                }
                return false;
            }
            if (dc_p())
            {
                if (mais_dc())
                {
                    //return true;
                }
                return false;
            }
            return true;
        }

        private bool mais_dc()
        {
            lerProximoToken();
            if (token.id == ";")
            {
                if (dc())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool dc_v()
        {
            lerProximoToken();
            if (token.id == "var")
            {
                if (variaveis())
                {
                    lerProximoToken();
                    if (token.id == ":")
                    {
                        if (tipo_var())
                        {
                            return true;
                        }
                        return false;
                    }
                    escreva("Falta identificador ':'");
                    return false;
                }
                return false;
            }
            escreva("Falta identiicador 'var'");
            return false;
        }

        private bool tipo_var()
        {
            lerProximoToken();
            if (token.tipo == "integer")
            {
                return true;
            }
            if (token.tipo == "real")
            {
                return true;
            }
            escreva("Tipo da variável faltando ou incorreta");
            return false;
        }

        private bool variaveis()
        {
            lerProximoToken();
            if (token.tipo == "Identificador")
            {
                if (mais_var())
                {
                    return true;
                }
                return false;
            }
            escreva("Falta idendificador de variável");
            return false;
        }

        private bool mais_var()
        {
            lerProximoToken();
            if (token.id == ",")
            {
                if (variaveis())
                {
                    return true;
                }
                //escreva("");
                return false;
            }
            return true;
        }

        private bool dc_p()
        {
            lerProximoToken();
            if (token.id == "procedure")
            {
                lerProximoToken();
                if (token.tipo == "Identificador")
                {
                    if (parametros())
                    {
                        if (corpo_p())
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

        private bool parametros()
        {
            lerProximoToken();
            if (token.id == "(")
            {
                if (lista_par())
                {
                    lerProximoToken();
                    if (token.id == ")")
                    {
                        return true;
                    }
                    escreva("Falta identificador ')'");
                    return false;
                }
                return false;
            }
            return true;
        }

        private bool lista_par()
        {
            if (variaveis())
            {
                lerProximoToken();
                if (token.id == ":")
                {
                    if (tipo_var())
                    {
                        if (mais_par())
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

        private bool mais_par()
        {
            lerProximoToken();
            if (token.id == ";")
            {
                if (lista_par())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool corpo_p()
        {
            if (dc_loc())
            {
                lerProximoToken();
                if (token.id == "begin")
                {
                    if (comandos())
                    {
                        lerProximoToken();
                        if (token.id == "end")
                        {
                            return true;
                        }
                        escreva("Falta identificador 'end'");
                        return false;
                    }
                    return false;
                }
                escreva("Falta identificador 'begin'");
                return false;
            }
            return false;
        }

        private bool dc_loc()
        {
            return true;
        }

        private bool mais_dcloc()
        {
            return true;
        }

        private bool lista_arg()
        {
            return true;
        }

        private bool argumentos()
        {
            return true;
        }

        private bool mais_ident()
        {
            return true;
        }

        private bool pfalsa()
        {
            return true;
        }

        private bool comandos()
        {
            return true;
        }

        private bool mais_comandos()
        {
            return true;
        }

        private bool comando()
        {
            return true;
        }

        private bool restoIdent()
        {
            return true;
        }

        private bool condicao()
        {
            return true;
        }

        private bool relacao()
        {
            return true;
        }

        private bool expressao()
        {
            return true;
        }

        private bool op_un()
        {
            return true;
        }

        private bool outros_termos()
        {
            return true;
        }

        private bool op_ad()
        {
            return true;
        }

        private bool termo()
        {
            return true;
        }

        private bool mais_fatores()
        {
            return true;
        }

        private bool op_mul()
        {
            return true;
        }

        private bool fator()
        {
            return true;
        }
    }
}
