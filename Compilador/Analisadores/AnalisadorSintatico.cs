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
                if (token.tipo == "ident")
                {
                    if (corpo())
                    {
                        lerProximoToken();
                        if (token.id == ".")
                        {
                            return true;
                        }
                        escreva("Falta identificador '.'");
                        return false;
                    }
                    return false;
                }
                escreva("Falta ident de nome do programa");
                return false;
            }
            escreva("Falta ident 'program'");
            return false;
        }

        private bool corpo()
        {
            if (dc())
            {
                //lerProximoToken();
                if (token.id == "begin")
                {
                    if (comandos())
                    {
                        //lerProximoToken();
                        if (token.id == "end")
                        {
                            return true;
                        }
                        escreva("Falta ident 'end'");
                        return false;
                    }
                    return false;
                }
                escreva("Falta ident 'begin'");
                return false;
            }
            return false;
        }

        private bool dc()
        {
            lerProximoToken();
            if (token.id == "var")
            {
                if (dc_v())
                {
                    if (mais_dc())
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            if (token.id == "procedure")
            {
                if (dc_p())
                {
                    if (mais_dc())
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
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
            //lerProximoToken();
            if (token.id == "var")
            {
                if (variaveis())
                {
                    if (token.id == ":")
                    {
                        if (tipo_var())
                        {
                            return true;
                        }
                        return false;
                    }
                    escreva("Falta ident ':'");
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
            if (token.id == "integer")
            {
                return true;
            }
            if (token.id == "real")
            {
                return true;
            }
            escreva("Tipo da variável faltando ou incorreta");
            return false;
        }

        private bool variaveis()
        {
            lerProximoToken();
            if (token.tipo == "ident")
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
            //lerProximoToken();
            //if (token.id == "procedure")
            //{
                lerProximoToken();
                if (token.tipo == "ident")
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
            //}
            //return false;
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
                    escreva("Falta ident ')'");
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
                //lerProximoToken();
                if (token.id == "begin")
                {
                    if (comandos())
                    {
                        //lerProximoToken();
                        if (token.id == "end")
                        {
                            return true;
                        }
                        escreva("Falta ident 'end'");
                        return false;
                    }
                    return false;
                }
                escreva("Falta ident 'begin'");
                return false;
            }
            return false;
        }

        private bool dc_loc()
        {
            if (dc_v())
            {
                if (mais_dcloc())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool mais_dcloc()
        {
            lerProximoToken();
            if (token.id == ";")
            {
                if (dc_loc())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool lista_arg()
        {
            lerProximoToken();
            if (token.id == "(")
            {
                if (argumentos())
                {
                    lerProximoToken();
                    if (token.id == ")")
                    {
                        return true;
                    }
                    escreva("Falta ident ')'");
                    return false;
                }
                return false;
            }
            return true;
        }

        private bool argumentos()
        {
            lerProximoToken();
            if (token.tipo == "ident")
            {
                if (mais_ident())
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool mais_ident()
        {
            lerProximoToken();
            if (token.id == ";")
            {
                if (argumentos())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool pfalsa()
        {
            lerProximoToken();
            if (token.id == "else")
            {
                if (comandos())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool comandos()
        {
            if (comando())
            {
                if (mais_comandos())
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool mais_comandos()
        {
            //lerProximoToken();
            if (token.id == ";")
            {
                if (comandos())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool comando()
        {
            lerProximoToken();
            if (token.id == "read")
            {
                lerProximoToken();
                if (token.id == "(")
                {
                    if (variaveis())
                    {
                        lerProximoToken();
                        if (token.id == ")")
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }

            if (token.id == "write")
            {
                lerProximoToken();
                if (token.id == "(")
                {
                    if (variaveis())
                    {
                        lerProximoToken();
                        if (token.id == ")")
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }

            if (token.id == "while")
            {
                if (condicao())
                {
                    lerProximoToken();
                    if (token.id == "do")
                    {
                        if (comandos())
                        {
                            lerProximoToken();
                            if (token.id == "$")
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

            if (token.id == "if")
            {
                if (condicao())
                {
                    lerProximoToken();
                    if (token.id == "then")
                    {
                        if (comandos())
                        {
                            if (pfalsa())
                            {
                                lerProximoToken();
                                if (token.id == "$")
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
                return false;
            }

            if (token.tipo == "ident")
            {
                if (restoIdent())
                {
                    return true;
                }
                return false;
            }

            return false;
        }

        private bool restoIdent()
        {
            lerProximoToken();
            if (token.id == ":=")
            {
                if (expressao())
                {
                    return true;
                }
                return false;
            }
            if (lista_arg())
            {
                return true;
            }
            return false;
        }

        private bool condicao()
        {
            if (expressao())
            {
                if (relacao())
                {
                    if (expressao())
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private bool relacao()
        {
            lerProximoToken();
            if (token.id == "=")
            {
                return true;
            }

            if (token.id == "<>")
            {
                return true;
            }

            if (token.id == ">=")
            {
                return true;
            }

            if (token.id == "<=")
            {
                return true;
            }

            if (token.id == ">")
            {
                return true;
            }

            if (token.id == "<")
            {
                return true;
            }

            return false;
        }

        private bool expressao()
        {
            if (termo())
            {
                if (outros_termos())
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool op_un()
        {
            lerProximoToken();
            if (token.id == "+")
            {
                return true;
            }

            if (token.id == "-")
            {
                return true;
            }
            return true;
        }

        private bool outros_termos()
        {
            if (op_ad())
            {
                if (termo())
                {
                    if (outros_termos())
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return true;
        }

        private bool op_ad()
        {
            //lerProximoToken();
            if (token.id == "+")
            {
                return true;
            }

            if (token.id == "-")
            {
                return true;
            }
            return false;
        }

        private bool termo()
        {
            if (op_un())
            {
                if (fator())
                {
                    if (mais_fatores())
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private bool mais_fatores()
        {
            if (op_mul())
            {
                if (fator())
                {
                    if (mais_fatores())
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return true;
        }

        private bool op_mul()
        {
            lerProximoToken();
            if (token.id == "*")
            {
                lerProximoToken();
                return true;
            }

            if (token.id == "/")
            {
                lerProximoToken();
                return true;
            }
            return false;
        }

        private bool fator()
        {
            //lerProximoToken();
            if (token.tipo == "ident")
            {
                return true;
            }
            if (token.tipo == "numero_int")
            {
                return true;
            }
            if (token.tipo == "numero_real")
            {
                return true;
            }
            if (token.id == "(")
            {
                if (expressao())
                {
                    if (token.id == ")")
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
    }
}