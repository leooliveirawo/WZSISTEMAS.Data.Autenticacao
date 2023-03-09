#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void ObterPorId()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());

            Assert.IsNotNull(servico.ObterPorId(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void ObterPorId_Id_Zero()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());

            servico.ObterPorId(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void ObterPorId_Id_Negativo()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());

            servico.ObterPorId(-1);
        }

        [TestMethod]
        public void ObterPorId_NaoExiste()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());

            Assert.IsNull(servico.ObterPorId(2));
        }

    }
}
