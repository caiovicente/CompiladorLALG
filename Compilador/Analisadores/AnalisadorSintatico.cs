using System;
using System.Collections.Generic;

namespace Compilador
{
    public class AnalisadorSintatico
    {
        private AnalisadorLexico analisadorLexico;
        private Token token;
        private Simbolo tabelaSimbolo = new Simbolo();
        

        public bool realizaAnaliseSintatica(AnalisadorLexico analisadorLexico)
        {
            token = new Token();
            this.analisadorLexico = analisadorLexico;
            lerProximoToken();
            if (programa())
            {
                //imprimeTabela();
                return true;
            }
            //imprimeTabela();
            return false;
        }

        private bool programa()
        {
            if (token.id == "program")
            {
                lerProximoToken();
                if (token.tipo == "ident")
                {
                    insereSimbolo(token.id, "programa");
                    string escopo = "global";
                    lerProximoToken();
                    if (corpo(escopo))
                    {
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

        private bool corpo(string escopo)
        {
            if (dc(escopo))
            {
                if (token.id == "begin")
                {
                    lerProximoToken();
                    if (comandos(escopo))
                    {
                        if (token.id == "end")
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

        private bool dc(string escopo)
        {
            if (dc_v(escopo))
            {
                if (mais_dc(escopo))
                {
                    return true;
                }
                return false;
            }

            if (dc_p())
            {
                if (mais_dc(escopo))//possivel erro
                {
                    return true;
                }
                return false;
            }

            return true;
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
                return false;
            }


            return true;
        }

        private bool dc_v(string escopo)
        {
            List<string> nomes = new List<string>();
            string tipo = "";
            if (token.id == "var")
            {
                string categoria = "variavel";
                lerProximoToken();
                if (variaveis(ref nomes))
                {
                    if (token.id == ":")
                    {
                        lerProximoToken();
                        if (tipo_var(ref tipo))
                        {
                            foreach (var nome in nomes)
                            {
                                if (insereSimbolo(nome, categoria, escopo, tipo))
                                {
                                    
                                }
                                else
                                {
                                    return false;
                                }
                            }
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

        private bool tipo_var(ref string tipo)
        {
            if (token.id == "integer")
            {
                tipo = token.id;
                lerProximoToken();
                return true;

            }
            if (token.id == "real")
            {
                tipo = token.id;
                lerProximoToken();
                return true;
            }
            
            return false;
        }
        
        private bool variaveis(ref List<string> nomes)
        {
            if (token.tipo == "ident")
            {
                nomes.Add(token.id);
                lerProximoToken();
                if (mais_var(ref nomes))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool mais_var(ref List<string> nomes)
        {
            if (token.id == ",")
            {
                lerProximoToken();
                if (variaveis(ref nomes))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool variaveis(string escopo)
        {
            if (token.tipo == "ident")
            {
                if (escopo == "global")
                {
                    if (buscaSimbolo(token.id, "global"))
                    {

                    }
                    else
                    {
                        Console.WriteLine("Variável '{0}' não declarada no escopo global", token.id);
                        return false;
                    }
                    
                }
                else
                {
                    if (buscaSimbolo(token.id, escopo, "variavel") || buscaSimbolo(token.id, "global", "variavel"))
                    {
                        
                    }
                    else
                    {
                        Console.WriteLine("Variável '{0}' não declarada", token.id);
                    }
                }

                lerProximoToken();
                if (mais_var(escopo))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool mais_var(string escopo)
        {
            if (token.id == ",")
            {
                lerProximoToken();
                if (variaveis(escopo))
                {
                    return true;
                }
                return false;
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
                    insereSimbolo(token.id, "ident", "global", "procedimento");
                    string escopo = token.id;
                    lerProximoToken();

                    if (parametros(escopo))
                    {
                        if (corpo_p(escopo))
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

        private bool parametros(string escopo)
        {
            if (token.id == "(")
            {
                lerProximoToken();
                if (lista_par(escopo))
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
            return true;
        }

        private bool lista_par(string escopo)
        {
            List<string> nomes = new List<string>();
            string tipo = "";
            string categoria = "parametros";
            int posicao = 1;

            if (variaveis(ref nomes))
            {
                if (token.id == ":")
                {
                    lerProximoToken();
                    if (tipo_var(ref tipo))
                    {
                        foreach (var nome in nomes)
                        {
                            if (insereSimbolo(nome, categoria, escopo, tipo, posicao))
                            {
                                posicao++;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        if (mais_par(escopo))
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

        private bool mais_par(string escopo)
        {
            if (token.id == ";")
            {
                lerProximoToken();
                if (lista_par(escopo))
                {
                    return true;
                }
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
                    lerProximoToken();
                    if (comandos(escopo))
                    {
                        if (token.id == "end")
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

        private bool dc_loc(string escopo)
        {
            if (dc_v(escopo))
            {
                if (mais_dcloc(escopo))
                {
                    return true;
                }
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
                    return false;
                }
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
                return false;
            }
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

        private bool pfalsa(string escopo)
        {
            if (token.id == "else")
            {
                lerProximoToken();
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
                lerProximoToken();
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
            if (token.id == "read")
            {
                lerProximoToken();
                if (token.id == "(")
                {
                    lerProximoToken();
                    if (variaveis(escopo))
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
                    if (variaveis(escopo))
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

            if (token.id == "while")
            {
                lerProximoToken();
                if (condicao())
                {
                    if (token.id == "do")
                    {
                        lerProximoToken();
                        if (comandos(escopo))
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
                        if (comandos(escopo))
                        {
                            if (pfalsa(escopo))
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
                if (escopo == "global")
                {
                    if (buscaSimbolo(token.id, "global", "ident"))
                    {
                        
                    }
                    else if (buscaSimbolo(token.id, "global", "variavel"))
                    {
                        
                    }
                    else
                    {
                        Console.WriteLine("Variável ou procedimento '{0}' não declarada", token.id);
                    }
                }
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
                insereSimbolo("numero", token.tipo, token.id);
                lerProximoToken();
                return true;
            }
            if (token.tipo == "numero_real")
            {
                insereSimbolo("numero", token.tipo, token.id);
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

        private void lerProximoToken()
        {
            token = analisadorLexico.retornaToken();
            //Console.WriteLine(token.id);
        }

        private bool insereSimbolo(string nome, string tipo, string valor) //numeros
        {
            Simbolo simbolo = new Simbolo(nome, tipo, valor);
            var aux = tabelaSimbolo.proximoSimbolo;
            while (aux.proximoSimbolo != null)
            {
                aux = aux.proximoSimbolo;
            }
            aux.proximoSimbolo = simbolo;
            return true;
        }

        private bool insereSimbolo(string nome, string categoria) //nome program ou procedure
        {
            Simbolo simbolo = new Simbolo(nome, categoria);
            if (tabelaSimbolo.proximoSimbolo == null)
            {
                tabelaSimbolo.proximoSimbolo = simbolo;
                return true;
            }
            if (buscaSimbolo(nome))
            {
                Console.WriteLine("O nome '{0}' já foi usado.", nome);
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

        private bool insereSimbolo(string nome, string categoria, string escopo, string tipo) //declaração de variavel
        {
            Simbolo simbolo = new Simbolo(nome, categoria, escopo, tipo);
            
            if (buscaSimbolo(nome, escopo))
            {
                Console.WriteLine("A variável '{0}' já foi declarada nesse escopo", nome);
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

        private bool insereSimbolo(string nome, string categoria, string escopo, string tipo, int posicao) //declaração de procedimento
        {
            Simbolo simbolo = new Simbolo(nome, categoria, escopo, tipo, posicao);
            
            if (buscaSimbolo(nome, escopo))
            {
                Console.WriteLine("A variável '{0}' já foi declarada nesse escopo", nome);
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

        private bool buscaSimbolo(string nome, string escopo, string categoria)
        {
            Simbolo simbolo = tabelaSimbolo.proximoSimbolo;
            while (simbolo != null)
            {
                if (simbolo.nome == nome && simbolo.escopo == escopo && simbolo.categoria == categoria)
                {
                    return true;
                }
                simbolo = simbolo.proximoSimbolo;
            }
            return false;
        }

        private bool buscaSimbolo(string nome)
        {
            Simbolo simbolo = tabelaSimbolo.proximoSimbolo;
            while (simbolo != null)
            {
                if (simbolo.nome == nome)
                {
                    return true;
                }
                simbolo = simbolo.proximoSimbolo;
            }
            return false;
        }

        private void imprimeTabela()
        {
            Simbolo aux = tabelaSimbolo.proximoSimbolo;
            while (aux != null)
            {
                Console.Write("Nome: {0} | ", aux.nome);
                Console.Write("Categoria: {0} | ", aux.categoria);
                Console.Write("Escopo: {0} | ", aux.escopo);
                Console.Write("Tipo: {0} | ", aux.tipo);
                Console.Write("Posição: {0} | ", aux.posicao);
                Console.WriteLine("Valor: {0} | ", aux.valor);
                aux = aux.proximoSimbolo;
            }
        }
    }
}