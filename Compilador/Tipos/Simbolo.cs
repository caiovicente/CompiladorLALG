
namespace Compilador
{
    public class Simbolo
    {
        public string nome, categoria, escopo, tipo, valor;
        public int posicao;
        public Simbolo proximoSimbolo;

        public Simbolo()
        {
            nome = "";
            categoria = "";
            escopo = "";
            tipo = "";
            posicao = 0;
            valor = "";
            proximoSimbolo = null;
        }

        public Simbolo(string nome, string categoria) //nomes de prog e proc
        {
            this.nome = nome;
            this.categoria = categoria;
            this.escopo = "";
            this.tipo = "";
            this.posicao = 0;
            this.valor = "";
            this.proximoSimbolo = null;
        }

        public Simbolo(string nome, string tipo, string valor) //numeros
        {
            this.nome = nome;
            this.categoria = "";
            this.escopo = "";
            this.tipo = tipo;
            this.posicao = 0;
            this.valor = valor;
            this.proximoSimbolo = null;
        }

        public Simbolo(string nome, string categoria, string escopo, string tipo) //variaveis
        {
            this.nome = nome;
            this.categoria = categoria;
            this.escopo = escopo;
            this.tipo = tipo;
            this.posicao = 0;
            this.valor = "";
            this.proximoSimbolo = null;
        }

        public Simbolo(string nome, string categoria, string escopo, string tipo, int posicao) //parametros
        {
            this.nome = nome;
            this.categoria = categoria;
            this.escopo = escopo;
            this.tipo = tipo;
            this.posicao = posicao;
            this.valor = "";
            this.proximoSimbolo = null;
        }
    }
}