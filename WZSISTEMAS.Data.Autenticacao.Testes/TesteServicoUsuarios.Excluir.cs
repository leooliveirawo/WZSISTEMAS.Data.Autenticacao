#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void Excluir()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());

            servico.Excluir(1);

            Assert.IsNull(repositorio.ObterPorId(1));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
        public void Excluir_NaoExiste()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());

            servico.Excluir(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Excluir_Id_Zero()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());

            servico.Excluir(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Excluir_Id_Negativo()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());

            servico.Excluir(-1);
        }

    }
}
