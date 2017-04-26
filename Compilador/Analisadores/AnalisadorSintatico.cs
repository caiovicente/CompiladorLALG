using System;
using System.Collections.Generic;

namespace Compilador
{
    public class AnalisadorSintatico
    {
        private AnalisadorLexico analisadorLexico;
        private Token token;
        private string escopo;
        private Simbolo tabelaSimbolo = new Simbolo();
        private Parametro tabelaParametro;
        int posicao = 0;
        public bool realizaAnaliseSintatica(AnalisadorLexico analisadorLexico)
        {
            token = new Token();
            this.analisadorLexico = analisadorLexico;
            lerProximoToken();
            if (programa())
                return true;
            return false;
        }

        private bool programa()
        {
            if (token.id == "program")
            {
                lerProximoToken();
                if (token.tipo == "ident")
                {
                    lerProximoToken();
                    //insereSimbolo(token.id, "ident_programa", "", "");
                    escopo = token.id;
                    if (corpo())
                    {
                        if (token.id == ".")
                        {
                            return true;
                        }
                        //escreva("Falta identificador '.'");
                        return false;
                    }
                    return false;
                }
                //escreva("Falta ident de nome do programa");
                return false;
            }
            //escreva("Falta ident 'program'");
            return false;
        }

        private bool corpo()
        {
            if (dc())
            {
                if (token.id == "begin")
                {
                    lerProximoToken();
                    if (comandos())
                    {
                        if (token.id == "end")
                        {
                            lerProximoToken();
                            return true;
                        }
                        //escreva("4");
                        return false;
                    }
                    //escreva("3");
                    return false;
                }
                //escreva("2");
                return false;
            }
            //escreva("1");
            return false;
        }

        private bool dc()
        {
            if (dc_v())
            {
                if (mais_dc())
                {
                    return true;
                }
                //escreva("6");
                return false;
            }
            if (dc_p())
            {
                if (mais_dc()) //Arrumar isso
                {
                    return true;
                }
                return false;
            }
            //escreva("5");
            return true;
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
                //escreva("7");
                return false;
            }
            return true;
        }

        private bool dc_v()
        {
            if (token.id == "var")
            {
                lerProximoToken();
                if (variaveis()) //var1
                {
                    if (token.id == ":")
                    {
                        lerProximoToken();
                        if (tipo_var())
                        {
                            //foreach (var nome in nomes)
                            //{
                            //    if (insereSimbolo(nome, categoria, escopo, tipo))
                            //    {
                            //        //Console.WriteLine("Inserido simbolo | nome: {0} | categoria: {1} | escopo: {2} | tipo: {3}", nome, categoria, escopo, tipo);
                            //    }
                            //    else
                            //    {
                            //        return false;
                            //    }
                            //}
                            return true;
                        }
                        //escreva("10");
                        return false;
                    }
                    //escreva("9");
                    return false;
                }
                //escreva("8");
                return false;
            }
            return true;
        }

        private bool tipo_var()
        {
            if (token.id == "integer")
            {
                lerProximoToken();
                return true;
            }
            if (token.id == "real")
            {
                lerProximoToken();
                return true;
            }
            //escreva("11");
            return false;
        }

        private bool variaveis()// var1
        {
            if (token.tipo == "ident")
            {
                lerProximoToken();
                if (mais_var())
                {
                    return true;
                }
                //escreva("13");
                return false;
            }
            //escreva("12");
            return false;
        }

        private bool mais_var()//var3
        {
            if (token.id == ",")
            {
                lerProximoToken();
                if (variaveis())
                {
                    return true;
                }
                return true;
            }
            return true;
        }

        private bool dc_p()
        {
            if (token.id == "procedure")
            {
                lerProximoToken();
                if (token.tipo == "ident")
                {
                    lerProximoToken();
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
            if (token.id == "(")
            {
                lerProximoToken();
                if (lista_par())
                {
                    if (token.id == ")")
                    {
                        lerProximoToken();
                        return true;
                    }
                    //escreva("18");
                    return false;
                }
                //escreva("17");
                return false;
            }
            return true;
        }

        private bool lista_par()
        {
            if (variaveis())//var2
            {
                if (token.id == ":")
                {
                    lerProximoToken();
                    if (tipo_var())
                    {
                        if (mais_par())
                        {
                            return true;
                        }
                        return false;
                    }
                    //escreva("21");
                    return false;
                }
                //escreva("20");
                return false;
            }
            //escreva("19");
            return false;
        }

        private bool mais_par()
        {
            if (token.id == ";")
            {
                lerProximoToken();
                if (lista_par())
                {
                    return true;
                }
                //escreva("23");
                return false;
            }
            return true;
        }

        private bool corpo_p()
        {
            if (dc_loc())
            {
                if (token.id == "begin")
                {
                    lerProximoToken();
                    if (comandos())
                    {
                        if (token.id == "end")
                        {
                            lerProximoToken();
                            return true;
                        }
                        //escreva("27");
                        return false;
                    }
                    //escreva("26");
                    return false;
                }
                //escreva("25");
                return false;
            }
            //escreva("24");
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
                //escreva("28");
                return false;
            }
            return true;
        }

        private bool mais_dcloc()
        {
            if (token.id == ";")
            {
                lerProximoToken();
                if (dc_loc())
                {
                    return true;
                }
                //escreva("29");
                return false;
            }
            return true;
        }

        private bool lista_arg()
        {
            if (token.id == "(")
            {
                lerProximoToken();
                if (argumentos())
                {
                    if (token.id == ")")
                    {
                        lerProximoToken();
                        return true;
                    }
                    //escreva("31'");
                    return false;
                }
                //escreva("30");
                return false;
            }
            return true;
        }

        private bool argumentos()
        {
            if (token.tipo == "ident")
            {
                lerProximoToken();
                if (mais_ident())
                {
                    return true;
                }
                //escreva("33");
                return false;
            }
            //escreva("32");
            return false;
        }

        private bool mais_ident()
        {
            if (token.id == ";")
            {
                lerProximoToken();
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
            if (token.id == "else")
            {
                lerProximoToken();
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
            if (token.id == ";")
            {
                lerProximoToken();
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
            if (token.id == "read")
            {
                lerProximoToken();
                if (token.id == "(")
                {
                    lerProximoToken();
                    if (variaveis())//var3
                    {
                        if (token.id == ")")
                        {
                            lerProximoToken();
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
                    lerProximoToken();
                    if (variaveis())//var3
                    {
                        if (token.id == ")")
                        {
                            lerProximoToken();
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            //-----------------------------------
            if (token.id == "while")
            {
                lerProximoToken();
                if (condicao())
                {
                    if (token.id == "do")
                    {
                        lerProximoToken();
                        if (comandos())
                        {
                            if (token.id == "$")
                            {
                                lerProximoToken();
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
                lerProximoToken();
                if (condicao())
                {
                    if (token.id == "then")
                    {
                        lerProximoToken();
                        if (comandos())
                        {
                            if (pfalsa())
                            {
                                if (token.id == "$")
                                {
                                    lerProximoToken();
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
                lerProximoToken();
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
            if (token.id == ":=")
            {
                lerProximoToken();
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
            if (token.id == "=")
            {
                lerProximoToken();
                return true;
            }

            if (token.id == "<>")
            {
                lerProximoToken();
                return true;
            }

            if (token.id == ">=")
            {
                lerProximoToken();
                return true;
            }

            if (token.id == "<=")
            {
                lerProximoToken();
                return true;
            }

            if (token.id == ">")
            {
                lerProximoToken();
                return true;
            }

            if (token.id == "<")
            {
                lerProximoToken();
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
            if (token.id == "+")
            {
                lerProximoToken();
                return true;
            }

            if (token.id == "-")
            {
                lerProximoToken();
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
            if (token.id == "+")
            {
                lerProximoToken();
                return true;
            }

            if (token.id == "-")
            {
                lerProximoToken();
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
            if (token.tipo == "ident")
            {
                lerProximoToken();
                return true;
            }
            if (token.tipo == "numero_int")
            {
                lerProximoToken();
                return true;
            }
            if (token.tipo == "numero_real")
            {
                lerProximoToken();
                return true;
            }
            if (token.id == "(")
            {
                lerProximoToken();
                if (expressao())
                {
                    if (token.id == ")")
                    {
                        lerProximoToken();
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        //private void escreva(string par)
        //{
        //    Console.WriteLine(par);
        //}

        private void lerProximoToken()
        {
            token = analisadorLexico.retornaToken();
            Console.WriteLine(token.id);
        }

        //private bool insereSimbolo(string nome, string categoria, string escopo, string tipo)
        //{
        //    Simbolo simbolo = new Simbolo(nome, categoria, escopo, tipo);

        //    if (tabelaSimbolo.proximoSimbolo == null)
        //    {
        //        tabelaSimbolo.proximoSimbolo = simbolo;
        //        return true;
        //    }
        //    if (buscaSimbolo(nome, escopo))
        //    {
        //        //Console.WriteLine("A variável '{0}' já foi declarada nesse escopo", nome);
        //        return false;
        //    }
        //    var aux = tabelaSimbolo.proximoSimbolo;
        //    while (aux.proximoSimbolo != null)
        //    {
        //        aux = aux.proximoSimbolo;
        //    }
        //    aux.proximoSimbolo = simbolo;
        //    return true;
        //}

        //private bool insereSimbolo(string nome, string categoria, string escopo, string tipo, int posicao)
        //{
        //    Simbolo simbolo = new Simbolo(nome, categoria, escopo, tipo, posicao);

        //    if (tabelaSimbolo.proximoSimbolo == null)
        //    {
        //        tabelaSimbolo.proximoSimbolo = simbolo;
        //        return true;
        //    }
        //    if (buscaSimbolo(nome, escopo))
        //    {
        //        //Console.WriteLine("A variável '{0}' já foi declarada nesse escopo", nome);
        //        return false;
        //    }
        //    var aux = tabelaSimbolo.proximoSimbolo;
        //    while (aux.proximoSimbolo != null)
        //    {
        //        aux = aux.proximoSimbolo;
        //    }
        //    aux.proximoSimbolo = simbolo;
        //    return true;
        //}

        //private bool buscaSimbolo(string nome, string escopo)
        //{
        //    Simbolo simbolo = tabelaSimbolo.proximoSimbolo;
        //    while (simbolo != null)
        //    {
        //        if (simbolo.nome == nome && simbolo.escopo == escopo)
        //        {
        //            return true;
        //        }
        //        simbolo = simbolo.proximoSimbolo;
        //    }
        //    return false;
        //}
    }
}