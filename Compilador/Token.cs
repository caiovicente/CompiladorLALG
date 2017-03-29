namespace Compilador
{
    public class Token
    {
        public string id, tipo;
        public Token proximoToken;
        public Token()
        {
            id = "ID";
            tipo = "TIPO";
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
