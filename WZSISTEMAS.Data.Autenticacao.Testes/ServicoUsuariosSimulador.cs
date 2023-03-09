using WZSISTEMAS.Data.Criptografia.Interfaces;

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public class ServicoUsuariosSimulador : ServicoUsuarios<Usuario>
    {
        public ServicoUsuariosSimulador(IRepositorioUsuarios<Usuario> repositorio, IProvedorHash provedorHash, IProvedorCriptografia provedorCriptografia, IDadosCriptografiaAutenticacao dadosCriptografia) : base(repositorio, provedorHash, provedorCriptografia, dadosCriptografia)
        {
        }

        public new Token CriarToken(string nomeUsuario)
        {
            return base.CriarToken(nomeUsuario);
        }

        public new string GerarHashChaveMestre(Usuario usuario)
        {
            return base.GerarHashChaveMestre(usuario);
        }
    }
}
