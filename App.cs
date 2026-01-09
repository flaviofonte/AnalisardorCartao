using AnalisardorCartao.Contexto;

namespace AnalisardorCartao
{
    public class App
    {
        private static ContextoPost contexto = null;
        private static ContextoConta contextoConta = null;

        private App() { }

        public static ContextoPost Contexto
        {
            get {
                if (contexto == null)
                {
                    contexto = new ContextoPost();
                }
                return contexto; 
            }
        }

        public static ContextoConta ContextoConta
        {
            get
            {
                if (contextoConta == null)
                {
                    contextoConta = new ContextoConta();
                }
                return contextoConta;
            }
        }
    }
}
