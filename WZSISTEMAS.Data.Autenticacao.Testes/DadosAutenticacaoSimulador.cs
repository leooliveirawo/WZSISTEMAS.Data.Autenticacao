namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public class DadosCriptografiaSimulador : IDadosCriptografiaAutenticacao
    {
        public string Chave { get; }

        public string IV { get; }

        public DadosCriptografiaSimulador(string chave, string iV)
        {
            Chave = chave;
            IV = iV;
        }
    }
}
