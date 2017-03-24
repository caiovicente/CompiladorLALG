using System;
using System.IO;
using System.Linq;

namespace Compilador
{
    class AnalisadorLexico
    {
        public Token inicio;

        public void geraToken(StreamReader codigo)
        {
            char caracter = (char)codigo.Read();
            string token = "";
            inicio = new Token();
            //aux = new Token();
            bool erroLexico = false;
            bool final = false;
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
                            token += caracter;
                            caracter = (char)codigo.Read();
                            break;
                        }
                        if (char.IsNumber(caracter))
                        {
                            estado = 2; // numero
                            token += caracter;
                            caracter = (char)codigo.Read();
                            break;
                        }
                        if (espacamento(caracter))
                        {
                            caracter = (char)codigo.Read();
                            //estado = 0; // ignorar espaçamentos
                            break;
                        }
                        if (simboloSimples(caracter))
                        {
                            if (caracter == '/')
                            {
                                token += caracter;
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
                        Console.WriteLine("Erro Léxico!");
                        break; //fim case 0

                    case 1: /* Palavra reservada ou identificador */
                        if (char.IsLetter(caracter) || char.IsNumber(caracter))
                        {
                            token += caracter;
                            caracter = (char)codigo.Read();
                            break;
                        }
                        if (simboloSimples(caracter) || espacamento(caracter))
                        {
                            adicionaListaToken(token, palavraReservada(token) ? "Palavra Reservada" : "Identificador");
                            estado = 0;
                        }
                        break;

                    case 2: /* Número inteiro */
                        if (char.IsNumber(caracter))
                        {
                            token += caracter;
                            caracter = (char)codigo.Read();
                            break;
                        }
                        if (caracter == '.')
                        {
                            token += caracter;
                            caracter = (char)codigo.Read();
                            estado = 3;
                            break;
                        }
                        if (espacamento(caracter) || simboloSimples(caracter))
                        {
                            adicionaListaToken(token, "Número Inteiro");
                            estado = 0;
                        }
                        break;

                    case 3: /* Aux Número real */
                        if (char.IsNumber(caracter))
                        {
                            token += caracter;
                            estado = 4;
                            caracter = (char)codigo.Read();
                        }
                        else
                        {
                            erroLexico = true;
                            Console.WriteLine("Erro Léxico!!");
                        }
                        break;

                    case 4: /* Númeor real */
                        if (char.IsNumber(caracter))
                        {
                            token += caracter;
                            caracter = (char)codigo.Read();
                            break;
                        }
                        if (espacamento(caracter) || simboloSimples(caracter))
                        {
                            adicionaListaToken(token, "Número real");
                            estado = 0;
                        }
                        else
                        {
                            erroLexico = true;
                            Console.WriteLine("Erro Léxico!!");
                        }
                        break;
                    case 5: /* Comentário "/*" ou Simbolo Simples */
                        if (caracter == '*')
                        {
                            //estado = 6;
                            caracter = (char)codigo.Read();
                            var caracter2 = (char)codigo.Read();
                            token = "";
                            while (caracter != '*' && caracter2 != '/') //fazer caso for fim de codigo?
                            {
                                caracter = (char)codigo.Read();
                                caracter2 = (char)codigo.Read();
                            }
                            break;
                        }
                        if (char.IsNumber(caracter) || char.IsLetter(caracter) || simboloSimples(caracter))
                        {
                            adicionaListaToken(token, "Símbolo simples");
                            estado = 0;
                        }
                        else
                        {
                            erroLexico = true;
                            Console.WriteLine("Erro Léxico!!");
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
        }//fim geraToken

        private void adicionaListaToken(string id, string tipo)
        {
            Token novo = new Token(id, tipo);
            if (inicio.proximo == null)
            {
                inicio.proximo = novo;
            }
            else
            {
                var auxAdd = inicio.proximo;
                while (auxAdd.proximo != null)
                {
                    auxAdd = auxAdd.proximo;
                }
                auxAdd.proximo = novo;
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

            return palavra.Contains(token);
        }

        private bool espacamento(char caracter)
        {
            return caracter == 9 || caracter == 10 || caracter == 11 || caracter == 13 || caracter == 32;
        }

        private bool simboloSimples(char caracter)
        {
            char[] simbolo = new char[16];
            simbolo[0] = '(';
            simbolo[1] = ')';
            simbolo[2] = '*';
            simbolo[3] = '/';
            simbolo[4] = '+';
            simbolo[5] = '-';
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
    }//fim AnalisadorLexico
}//fim Compilador
