#nullable disable

using Newtonsoft.Json;

using System.Security;

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
#nullable disable
    [TestClass]
    public partial class TesteServicoUsuarios
    {
        private static ServicoUsuariosSimulador CriarServicoUsuariosSemTeste()
        {
            return new ServicoUsuariosSimulador(
                new RepositorioUsuariosSimulador(),
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));
        }

        private void AdicionarUsuarios(ServicoUsuariosSimulador servico)
        {
            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste2",
                Email = "teste2@teste2.com"
            }, "teste2");

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste3",
                Email = "teste3@teste3.com"
            }, "teste3");

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste4",
                Email = "teste4@teste4.com"
            }, "teste4");
        }

        [TestMethod]
        public void ServicoUsuarios_Criar()
        {
            CriarServicoUsuariosSemTeste();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void ServicoUsuarios_Criar_Repositorio_Nulo()
        {
            _ = new ServicoUsuariosSimulador(
                default,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void ServicoUsuarios_Criar_ProvedorHash_Nulo()
        {
            _ = new ServicoUsuariosSimulador(
                new RepositorioUsuariosSimulador(),
                default,
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void ServicoUsuarios_Criar_ProvedorCriptografia_Nulo()
        {
            _ = new ServicoUsuariosSimulador(
                new RepositorioUsuariosSimulador(),
                new ProvedorHashSimulador(),
                default,
                new DadosCriptografiaSimulador("1", "1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void ServicoUsuarios_Criar_DadosCriptografia_Nula()
        {
            _ = new ServicoUsuariosSimulador(
                new RepositorioUsuariosSimulador(),
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                default);
        }
    }
}
