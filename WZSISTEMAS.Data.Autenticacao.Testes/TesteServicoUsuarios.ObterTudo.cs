#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void ObterTudo_ContemItens()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());
            repositorio.Criar(new Usuario());
            repositorio.Criar(new Usuario());
            repositorio.Criar(new Usuario());

            var usuarios = servico.ObterTudo();

            Assert.IsTrue(usuarios.Count() == 4);
        }

        [TestMethod]
        public void ObterTudo_NaoContemItens()
        {            
            var usuarios = CriarServicoUsuariosSemTeste().ObterTudo();

            Assert.IsTrue(usuarios.Count() == 0);
        }
    }
}
