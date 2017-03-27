namespace Compilador
{
    class Token
    {
        public string id, tipo;
        public Token proximoToken;
        public Token()
        {
            id = "";
            tipo = "";
            proximoToken = null;
        }
        public Token(string id, string tipo)
        {
            this.id = id;
            this.tipo = tipo;
            this.proximoToken = null;
        }
    }
}
