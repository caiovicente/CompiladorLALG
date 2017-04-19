
namespace Compilador
{
    public class Simbolo
    {
        public string nome, categoria, escopo, tipo;
        public int posicao;
        public Simbolo proximoSimbolo;

        public Simbolo()
        {
            nome = "";
            categoria = "";
            escopo = "";
            tipo = "";
            //posicao = 1;
            proximoSimbolo = null;
        }

        public Simbolo(string nome, string categoria, string escopo, string tipo/*, int posicao*/)
        {
            this.nome = nome;
            this.categoria = categoria;
            this.escopo = escopo;
            this.tipo = tipo;
            //this.posicao = posicao;
            this.proximoSimbolo = null;
        }
    }
}