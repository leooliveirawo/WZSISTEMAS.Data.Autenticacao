using WZSISTEMAS.Data.Criptografia.Interfaces;

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public sealed class ProvedorHashSimulador : IProvedorHash
    {
        public ProvedorHashSimulador()
        {
        }

        public bool CompararHash(string hash, string texto)
        {
            return hash == $"$hash_{texto})";
        }

        public string GerarHash(string texto)
        {
            return $"$hash_{texto}";
        }
    }
}
