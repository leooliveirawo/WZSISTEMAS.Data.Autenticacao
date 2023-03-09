#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void ObterPorNomeCompleto()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
                HashSenha = provedorHash.GerarHash("teste1")
            };

            repositorio.Criar(usuario);

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                provedorHash,
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            Assert.IsNotNull(servico.ObterPorNomeUsuario("teste1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void ObterPorNomeCompleto_NomeCompleto_Vazio()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.ObterPorNomeUsuario(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void ObterPorNomeCompleto_NomeCompleto_Nulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.ObterPorNomeUsuario(default);
        }

        [TestMethod]
        public void ObterPorNomeCompleto_NaoExiste()
        {
            var servico = CriarServicoUsuariosSemTeste();

            Assert.IsNull(servico.ObterPorNomeUsuario("teste"));
        }

    }
}
