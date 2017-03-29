namespace Compilador
{
    public class Parametro
    {
        public string nome;
        public int real, inteiro;
        public Parametro ProximoParametro;

        public Parametro()
        {
            nome = "";
            real = 0;
            inteiro = 0;
            ProximoParametro = null;
        }

        public Parametro(string nome, int real, int inteiro)
        {
            this.nome = nome;
            this.inteiro = inteiro;
            this.real = real;
        }
    }
}