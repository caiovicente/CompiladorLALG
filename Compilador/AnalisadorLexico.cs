using System;
using System.IO;
using System.Linq;

namespace Compilador
{
    class AnalisadorLexico
    {
        public Token inicio;

        public bool geraToken(StreamReader codigo)
        {
            char caracter = (char)codigo.Read();
            string valorToken = "";
            inicio = new Token();
            //aux = new Token();
            bool erroLexico = false;
            bool final = false; // Indica fim do arquivo(codigo)
            int estado = 0;
            while (!erroLexico && !final)
            {
                switch (estado)
                {
                    case 0:/* Caso inicial: lendo primeiro caracter */
                        //token = "";
                        if (char.IsLetter(caracter))
                        {
                            estado = 1; // id ou palavra reservada
                            valorToken += caracter;
                            caracter = (char)codigo.Read();
                            break;
                        }
                        if (char.IsNumber(caracter))
                        {
                            estado = 2; // numero
                            valorToken += caracter;
                            caracter = (char)codigo.Read();
                            break;
                        }
                        if (espacamento(caracter))
                        {
                            caracter = (char)codigo.Read();
                            estado = 0; // ignorar espaçamentos
                            break;
                        }
                        if (simboloSimples(caracter) || operador(caracter))
                        {
                            if (caracter == '/')
                            {
                                valorToken += caracter;
                                estado = 5; // Comentário "/*"
                                caracter = (char)codigo.Read();
                                break;
                            }
                            if (caracter == '{')
                            {
                                estado = 6; // Comentário '{'
                                caracter = (char)codigo.Read();
                                break;
                            }
                            //todo
                            caracter = (char)codigo.Read();
                        }
                        erroLexico = true;
                        Console.WriteLine("Erro Léxico! 1");
                        break; //fim case 0

                    case 1: /* Palavra reservada ou identificador */
                        if (char.IsLetter(caracter) || char.IsNumber(caracter))
                        {
                            valorToken += caracter;
                            caracter = (char)codigo.Read();
                            break;
                        }
                        if (simboloSimples(caracter) || espacamento(caracter) || codigo.EndOfStream)
                        {
                            estado = 0;
                            adicionaListaToken(valorToken, palavraReservada(valorToken) ? "Palavra Reservada" : "Identificador");
                            valorToken = "";
                            if (codigo.EndOfStream)
                                final = true;
                        }
                        break;

                    case 2: /* Número inteiro */
                        if (char.IsNumber(caracter))
                        {
                            valorToken += caracter;
                            caracter = (char)codigo.Read();
                            break;
                        }
                        if (caracter == '.')
                        {
                            valorToken += caracter;
                            caracter = (char)codigo.Read();
                            estado = 3;
                            break;
                        }
                        if (espacamento(caracter) || simboloSimples(caracter))
                        {
                            adicionaListaToken(valorToken, "Número Inteiro");
                            estado = 0;
                            valorToken = "";
                            break;
                        }
                        erroLexico = true;
                        Console.WriteLine("Erro Léxico! Num");
                        break;

                    case 3: /* Aux Número real */
                        if (char.IsNumber(caracter))
                        {
                            valorToken += caracter;
                            estado = 4;
                            caracter = (char)codigo.Read();
                        }
                        else
                        {
                            erroLexico = true;
                            Console.WriteLine("Erro Léxico!! 2");
                        }
                        break;

                    case 4: /* Númeor real */
                        if (char.IsNumber(caracter))
                        {
                            valorToken += caracter;
                            caracter = (char)codigo.Read();
                            break;
                        }
                        if (espacamento(caracter) || operador(caracter) || caracter == ';')
                        {
                            adicionaListaToken(valorToken, "Número real");
                            valorToken = "";
                            estado = 0;
                            break;
                        }
                        erroLexico = true;
                        Console.WriteLine("Erro Léxico!! 3");
                        break;
                    case 5: /* Comentário "/*" ou Simbolo Simples */
                        if (caracter == '*')
                        {
                            //estado = 6;
                            caracter = (char)codigo.Read();
                            var caracter2 = (char)codigo.Read();
                            valorToken = "";
                            while (caracter != '*' && caracter2 != '/') //fazer caso for fim de codigo?
                            {
                                caracter = (char)codigo.Read();
                                caracter2 = (char)codigo.Read();
                            }
                            break;
                        }
                        if (char.IsNumber(caracter) || char.IsLetter(caracter) || simboloSimples(caracter))
                        {
                            adicionaListaToken(valorToken, "Símbolo simples");
                            valorToken = "";
                            estado = 0;
                        }
                        else
                        {
                            erroLexico = true;
                            Console.WriteLine("Erro Léxico!! 4");
                        }
                        break;
                    case 6: /* Comentário '}' */
                        while (caracter != '}')
                        {
                            caracter = (char)codigo.Read();
                        }
                        estado = 0;
                        caracter = (char)codigo.Read();
                        break;
                    case 7:

                        break;
                }//fim switch
            }//fim while
            return final;
        }//fim geraToken

        private void adicionaListaToken(string id, string tipo)
        {
            Token novo = new Token(id, tipo);
            if (inicio.proximoToken == null)
            {
                inicio.proximoToken = novo;
            }
            else
            {
                var auxAdd = inicio.proximoToken;
                while (auxAdd.proximoToken != null)
                {
                    auxAdd = auxAdd.proximoToken;
                }
                auxAdd.proximoToken = novo;
            }
            
        }

        private bool palavraReservada(string token)
        {
            string[] palavra = new string[12];
            //palavra[0] = "ident";
            palavra[0] = "real";
            palavra[1] = "integer";
            palavra[2] = "if";
            palavra[3] = "then";
            palavra[4] = "while";
            palavra[5] = "do";
            palavra[6] = "write";
            palavra[7] = "read";
            palavra[8] = "else";
            palavra[9] = "begin";
            palavra[10] = "end";
            palavra[11] = "program";
            return palavra.Contains(token);
        }

        private bool espacamento(char caracter)
        {
            return (caracter == 9 || caracter == 10 || caracter == 11 || caracter == 13 || caracter == 32);
        }

        private bool operador(char caracter)
        {
            char[] operador = new char[4];
            operador[0] = '+';
            operador[1] = '-';
            operador[2] = '/';
            operador[3] = '*';
            return operador.Contains(caracter);
        }

        private bool simboloSimples(char caracter)
        {
            char[] simbolo = new char[16];
            simbolo[0] = '(';
            simbolo[1] = ')';
            //simbolo[2] = '*';
            //simbolo[3] = '/';
            //simbolo[4] = '+';
            //simbolo[5] = '-';
            simbolo[6] = ':';
            simbolo[7] = ';';
            simbolo[8] = ',';
            simbolo[9] = '=';
            simbolo[10] = '>';
            simbolo[11] = '<';
            simbolo[12] = '$';
            simbolo[13] = '.';
            simbolo[14] = '{';
            simbolo[15] = '}';

            return simbolo.Contains(caracter);

        }//fim simboloSimples
        private bool simboloDuplo(string caracter)
        {
            string[] simbolo = new string[4];
            simbolo[0] = ">=";
            simbolo[1] = "<=";
            simbolo[2] = "<>";
            simbolo[3] = ":=";

            return simbolo.Contains(caracter);


            //for (int i = 0; i < 3; i++)
            //{
            //    if (simbolo[i] == caracter)
            //        return true;
            //}
            //return false;

        }//fim simboloDuplo

        public Token retornaToken()
        {
            return inicio;
        }
    }//fim AnalisadorLexico
}//fim Compilador
