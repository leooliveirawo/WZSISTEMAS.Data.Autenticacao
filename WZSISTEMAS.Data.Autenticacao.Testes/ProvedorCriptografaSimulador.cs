using WZSISTEMAS.Data.Criptografia.Interfaces;

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public class ProvedorCriptografaSimulador : IProvedorCriptografia
    {
        public ProvedorCriptografaSimulador()
        {
        }

        public string Criptografar(string chave, string iV, string texto)
        {
            return $"$_cripto_{texto}";
        }

        public string Descriptografar(string chave, string iV, string textoCriptografar)
        {
            return textoCriptografar.Replace($"$_cripto_", string.Empty);
        }
    }
}
