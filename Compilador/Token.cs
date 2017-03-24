namespace Compilador
{
    class Token
    {
        public string id, tipo;
        public Token proximo;
        public Token()
        {
            id = "";
            tipo = "";
            proximo = null;
        }
        public Token(string id, string tipo)
        {
            this.id = id;
            this.tipo = tipo;
            this.proximo = null;
        }
    }
}
