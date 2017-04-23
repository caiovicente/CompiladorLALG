namespace Compilador
{
    public class Parametro
    {
        public string nome, escopo;
        public int real, inteiro;
        public Parametro ProximoParametro;

        public Parametro()
        {
            nome = "";
            escopo = "";
            real = 0;
            inteiro = 0;
            ProximoParametro = null;
        }

        public Parametro(string nome, string escopo, int real, int inteiro)
        {
            this.nome = nome;
            this.escopo = escopo;
            this.inteiro = inteiro;
            this.real = real;
        }
    }
}