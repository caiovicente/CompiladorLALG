namespace Compilador
{
    public class Token
    {
        public string ident, tipo;
        public Token proximo;

        public Token()
        {
            ident = "";
            tipo = "";
            proximo = null;
        }

        public Token(string id, string tipo)
        {
            this.ident = id;
            this.tipo = tipo;
            this.proximo = null;
        }
    }
}