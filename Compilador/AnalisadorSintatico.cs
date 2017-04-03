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
            lerProximoToken();
            if (declaracao())
            {
                if (token.id == "begin")
                {
                    if (comandos())
                    {
                        lerProximoToken();
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
            if (token.id == "var" || token.id == "procedure")
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
                if (variavel())
                {
                    if (token.id == ":")
                    {
                        if (tipoDeVariavel())
                        {
                            lerProximoToken();
                            if (token.id == ";")
                            {
                                declaracao();
                            }
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

        private bool variavel()
        {
            lerProximoToken();
            if (token.tipo == "Identificador")
            {
                if (maisVariavel())
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool maisVariavel()
        {
            lerProximoToken();
            if (token.id == ",")
            {
                lerProximoToken();
                if (token.tipo == "Identificador")
                {
                    lerProximoToken();
                    maisVariavel();
                }
                return false;
            }
            return true;
        }
        private bool tipoDeVariavel()
        {
            lerProximoToken();
            if (token.tipo == "Real" || token.tipo == "Integer")
            {
                return true;
            }
            return false;
        }

        private bool declaraProcedimento()
        {
            if (token.id == "procedure")
            {
                lerProximoToken();
                if (token.tipo == "Identificador")
                {
                    if (parametros())
                    {
                        if (corpoProcedimento())
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

        private bool parametros()
        {
            lerProximoToken();
            if (token.id == "(")
            {
                if (listaParametros())
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

        private bool listaParametros()
        {
            if (variavel())
            {
                if (token.id == ":")
                {
                    if (tipoDeVariavel())
                    {
                        if (maisParametros())
                        {
                            return true;
                        }
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private bool maisParametros()
        {
            lerProximoToken();
            if (token.id == ";")
            {
                if (listaParametros())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool corpoProcedimento()
        {
            if (declaraVariavelLocal())
            {
                if (token.id == "begin")
                {
                    if (comandos())
                    {
                        lerProximoToken();
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

        private bool declaraVariavelLocal()
        {
            if (token.id == "var")
            {
                if (variavel())
                {
                    if (token.id == ":")
                    {
                        if (tipoDeVariavel())
                        {
                            lerProximoToken();
                            if (token.id == ";")
                            {
                                lerProximoToken();
                                declaraVariavelLocal();
                            }
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
        
        private bool comandos()
        {
            lerProximoToken();
            if (comando())
            {
                if (maisComandos())
                {
                    return true;
                }
                return false;
            }
            return false;
        }        

        private bool maisComandos()
        {
            lerProximoToken();
            if (token.id == ";")
            {
                comandos();
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
                    if (variavel())
                    {
                        if (token.id == ")")
                        {
                            return true;
                        }
                        return false;
                    }
                }
                return false;
            }

            if (token.id == "write")
            {

            }



            return false;
        }

        private void lerProximoToken()
        {
            token = analisadorLexico.retornaToken();
        }
    }
}