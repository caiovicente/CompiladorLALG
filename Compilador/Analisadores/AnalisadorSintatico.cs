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
                    escopo = token.id;
                    Console.WriteLine("Inserido simbolo | nome: {0} | categoria: '{1}'" , token.id , "ident_programa");
                    if (corpo(escopo))
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

        private bool corpo(string escopo)
        {
            lerProximoToken();
            if (dc(escopo))
            {
                lerProximoToken();
                if (token.id == "begin")
                {
                    if (comandos(escopo))
                    {
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

        private bool dc(string escopo)
        {
            if (dc_v(escopo) && dc_p())
            {
                if (mais_dc(escopo))
                {
                    return true;
                }
                escreva("6");
                return false;
            }
            escreva("5");
            return false;
        }

        private bool mais_dc(string escopo)
        {
            if (token.id == ";")
            {
                lerProximoToken();
                if (dc(escopo))
                {
                    return true;
                }
                escreva("7");
                return false;
            }
            return true;
        }

        private bool dc_v(string escopo)
        {
            string tipo = "";

            if (token.id == "var")
            {
                string categoria = "variavel";
                List<string> nomes = new List<string>();
                if (variaveis(ref nomes)) //var1
                {
                    if (token.id == ":")
                    {
                        if (tipo_var(ref tipo))
                        {
                            foreach (var nome in nomes)
                            {
                                if (insereSimbolo(nome, categoria, escopo, tipo))
                                {
                                    Console.WriteLine("Inserido simbolo | nome: {0} | categoria: {1} | escopo: {2} | tipo: {3}", nome, categoria, escopo, tipo);
                                }
                                else
                                {
                                    return false;
                                }
                            }
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

        private bool tipo_var(ref string tipo)
        {
            lerProximoToken();
            if (token.id == "integer")
            {
                tipo = "inteiro";
                return true;
            }
            if (token.id == "real")
            {
                tipo = "real";
                return true;
            }
            escreva("11");
            return false;
        }

        private bool variaveis(ref List<string> nome)// var1
        {
            lerProximoToken();
            if (token.tipo == "ident")
            {
                nome.Add(token.id);
                lerProximoToken();
                if (mais_var(ref nome))
                {
                    return true;
                }
                escreva("13");
                return false;
            }
            escreva("12");
            return false;
        }

        private bool variaveis(ref string nome, ref int posicao)//var2
        {
            lerProximoToken();
            if (token.tipo == "ident")
            {
                nome = token.id;
                posicao++;
                return true;
            }
            escreva("12");
            return false;
        }

        private bool variaveis(string escopo)//var3
        {
            if (token.tipo == "ident")
            {
                if (buscaSimbolo(token.id, escopo))
                {
                    lerProximoToken();
                    if (mais_var(escopo))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private bool mais_var(ref List<string> nomes)//var1
        {
            if (token.id == ",")
            {
                if (variaveis(ref nomes))
                {
                    return true;
                }
                escreva("14");
                return false;
            }
            return true;
        }

        private bool mais_var(string escopo)//var3
        {
            if (token.id == ";")
            {
                lerProximoToken();
                if (variaveis(escopo))
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
                    string escopo = token.id;
                    insereSimbolo(token.id, "ident_procedimento", "", "");
                    Console.WriteLine("Inserido simbolo | nome: {0} | categoria: '{1}'", token.id, "ident_procedimento");
                    if (parametros(escopo))
                    {
                        lerProximoToken();
                        if (corpo_p(escopo))
                        {
                            return true;
                        }
                        escreva("16");
                        return false;
                    }
                    escreva("15");
                    return false;
                }
            }
            return true;
        }

        private bool parametros(string escopo)
        {
            lerProximoToken();
            if (token.id == "(")
            {
                if (lista_par(escopo))
                {
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

        private bool lista_par(string escopo)
        {
            string nome = "";
            string tipo = "";
            string categoria = "parametro";
            

            if (variaveis(ref nome, ref posicao))//var2
            {
                lerProximoToken();
                if (token.id == ":")
                {
                    if (tipo_var(ref tipo))
                    {
                        if (insereSimbolo(nome, categoria, escopo, tipo, posicao))
                        {
                            Console.WriteLine("Inserido parametro nome: {0} | categoria: {1} | escopo: {2} | tipo: {3} | posicao: {4}", nome, categoria, escopo, tipo, posicao);
                            if (mais_par(escopo))
                            {
                                return true;
                            }
                            return false;
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

        private bool mais_par(string escopo)
        {
            lerProximoToken();
            if (token.id == ";")
            {
                lerProximoToken();
                if (lista_par(escopo))
                {
                    return true;
                }
                escreva("23");
                return false;
            }
            return true;
        }

        private bool corpo_p(string escopo)
        {
            if (dc_loc(escopo))
            {
                if (token.id == "begin")
                {
                    if (comandos(escopo))
                    {
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

        private bool dc_loc(string escopo)
        {
            if (dc_v(escopo))
            {
                if (mais_dcloc(escopo))
                {
                    return true;
                }
                escreva("28");
                return false;
            }
            return true;
        }

        private bool mais_dcloc(string escopo)
        {
            if (token.id == ";")
            {
                lerProximoToken();
                if (dc_loc(escopo))
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

        private bool pfalsa(string escopo)
        {
            lerProximoToken();
            if (token.id == "else")
            {
                if (comandos(escopo))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool comandos(string escopo)
        {
            if (comando(escopo))
            {
                if (mais_comandos(escopo))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool mais_comandos(string escopo)
        {
            if (token.id == ";")
            {
                if (comandos(escopo))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool comando(string escopo)
        {
            lerProximoToken();
            if (token.id == "read")
            {
                lerProximoToken();
                if (token.id == "(")
                {
                    if (variaveis(escopo))//var3
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
                    if (variaveis(escopo))//var3
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

        //
        private bool comandos()
        {
            return true;
        }

        private bool pfalsa()
        {
            return true;
        }

        //

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


        private void escreva(string par)
        {
            Console.WriteLine(par);
        }

        private void escreva(string par1, string par2)
        {
            Console.WriteLine(par1, par2);
        }

        private void lerProximoToken()
        {
            token = analisadorLexico.retornaToken();
        }

        private bool insereSimbolo(string nome, string categoria, string escopo, string tipo)
        {
            Simbolo simbolo = new Simbolo(nome, categoria, escopo, tipo);

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
            var aux = tabelaSimbolo.proximoSimbolo;
            while (aux.proximoSimbolo != null)
            {
                aux = aux.proximoSimbolo;
            }
            aux.proximoSimbolo = simbolo;
            return true;
        }

        private bool insereSimbolo(string nome, string categoria, string escopo, string tipo, int posicao)
        {
            Simbolo simbolo = new Simbolo(nome, categoria, escopo, tipo, posicao);

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
            var aux = tabelaSimbolo.proximoSimbolo;
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