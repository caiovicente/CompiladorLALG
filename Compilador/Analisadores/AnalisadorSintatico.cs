﻿using System;

namespace Compilador
{
    public class AnalisadorSintatico
    {
        private AnalisadorLexico analisadorLexico;
        private Token token;
        private string escopo = "global";
        private Simbolo tabelaSimbolo = new Simbolo();
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
            lerProximoToken();
            if (token.id == "program")
            {
                lerProximoToken();
                if (token.tipo == "ident")
                {
                    insereSimbolo(token.id, "ident_programa", "", "");
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
            lerProximoToken();
            if (dc())
            {
                lerProximoToken();
                if (token.id == "begin")
                {
                    if (comandos())
                    {
                        //lerProximoToken();
                        if (token.id == "end")
                        {
                            return true;
                        }
                        escreva("4");
                        return false;
                    }
                    escreva("3");
                    return false;
                }
                escreva("2");
                return false;
            }
            escreva("1");
            return false;
        }

        private bool dc()
        {
            if (dc_v() && dc_p())
            {
                if (mais_dc())
                {
                    return true;
                }
                escreva("6");
                return false;
            }
            escreva("5");
            return false;
        }

        private bool mais_dc()
        {
            if (token.id == ";")
            {
                lerProximoToken();
                if (dc())
                {
                    return true;
                }
                escreva("7");
                return false;
            }
            return true;
        }

        private bool dc_v()
        {
            if (token.id == "var")
            {
                if (variaveis())
                {
                    if (token.id == ":")
                    {
                        if (tipo_var())
                        {
                            lerProximoToken();
                            return true;
                        }
                        escreva("10");
                        return false;
                    }
                    escreva("9");
                    return false;
                }
                escreva("8");
                return false;
            }
            return true;
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
            escreva("11");
            return false;
        }

        private bool variaveis()
        {
            lerProximoToken();
            if (token.tipo == "ident")
            {
                lerProximoToken();
                if (mais_var())
                {
                    return true;
                }
                escreva("13");
                return false;
            }
            escreva("12");
            return false;
        }

        private bool mais_var()
        {
            if (token.id == ",")
            {
                if (variaveis())
                {
                    return true;
                }
                escreva("14");
                return false;
            }
            return true;
        }

        private bool dc_p()
        {
            if (token.id == "procedure")
            {
                if (parametros())
                {
                    if (corpo_p())
                    {
                        return true;
                    }
                    escreva("16");
                    return false;
                }
                escreva("15");
                return false;
            }
            return true;
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
                    escreva("18");
                    return false;
                }
                escreva("17");
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
                        escreva("22");
                        return false;
                    }
                    escreva("21");
                    return false;
                }
                escreva("20");
                return false;
            }
            escreva("19");
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
                escreva("23");
                return false;
            }
            return true;
        }

        private bool corpo_p()
        {
            lerProximoToken();
            if (dc_loc())
            {
                if (token.id == "begin")
                {
                    if (comandos())
                    {
                        //lerProximoToken();
                        if (token.id == "end")
                        {
                            return true;
                        }
                        escreva("27");
                        return false;
                    }
                    escreva("26");
                    return false;
                }
                escreva("25");
                return false;
            }
            escreva("24");
            return false;
        }

        private bool dc_loc()
        {
            if (dc_v())
            {
                //lerProximoToken();
                if (mais_dcloc())
                {
                    return true;
                }
                escreva("28");
                return false;
            }
            return true;
        }

        private bool mais_dcloc()
        {
            //lerProximoToken();
            if (token.id == ";")
            {
                lerProximoToken();
                if (dc_loc())
                {
                    return true;
                }
                escreva("29");
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
                    escreva("31'");
                    return false;
                }
                escreva("30");
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
                escreva("33");
                return false;
            }
            escreva("32");
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
                //lerProximoToken(); trocado
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
            if (token.id == ";")
            {
                //lerProximoToken(); -------------------------------
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
                    //lerProximoToken();
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
            while (simbolo != null)
            {
                if (simbolo.nome == nome && simbolo.escopo == escopo)
                {
                    return true;
                }
                simbolo = simbolo.proximoSimbolo;
            }
            return false;
        }
    }
}