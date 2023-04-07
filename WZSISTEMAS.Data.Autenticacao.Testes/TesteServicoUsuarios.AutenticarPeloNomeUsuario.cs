#nullable disable

using System.Security;

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void AutenticarPeloNomeUsuario()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();
            var provedorCriptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografia = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                provedorHash,
                provedorCriptografia,
                dadosCriptografia);

            repositorio.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
                HashSenha = provedorHash.GerarHash("teste1"),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            });

            var token = servico.AutenticarPeloNomeUsuario("teste1", "teste1");

            Assert.IsNotNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void AutenticarPeloNomeUsuario_NomeUsuario_Nulo()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();
            var provedorCriptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografia = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                provedorHash,
                provedorCriptografia,
                dadosCriptografia);

            repositorio.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
                HashSenha = provedorHash.GerarHash("teste1"),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            });

            Assert.IsNull(servico.AutenticarPeloNomeUsuario(default, "teste1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void AutenticarPeloNomeUsuario_NomeUsuario_Vazio()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();
            var provedorCriptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografia = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                provedorHash,
                provedorCriptografia,
                dadosCriptografia);

            repositorio.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
                HashSenha = provedorHash.GerarHash("teste1"),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            });

            Assert.IsNull(servico.AutenticarPeloNomeUsuario(string.Empty, "teste1"));
        }

        [TestMethod]
        public void AutenticarPeloNomeUsuario_NomeUsuario_Invalido()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();
            var provedorCriptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografia = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                provedorHash,
                provedorCriptografia,
                dadosCriptografia);

            repositorio.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
                HashSenha = provedorHash.GerarHash("teste1"),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            });

            Assert.IsNull(servico.AutenticarPeloNomeUsuario("teste2", "teste1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void AutenticarPeloNomeUsuario_Senha_Nulo()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();
            var provedorCriptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografia = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                provedorHash,
                provedorCriptografia,
                dadosCriptografia);

            repositorio.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
                HashSenha = provedorHash.GerarHash("teste1"),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            });

            Assert.IsNull(servico.AutenticarPeloNomeUsuario("teste1", default));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void AutenticarPeloNomeUsuario_Senha_Vazio()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();
            var provedorCriptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografia = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                provedorHash,
                provedorCriptografia,
                dadosCriptografia);

            repositorio.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
                HashSenha = provedorHash.GerarHash("teste1"),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            });

            Assert.IsNull(servico.AutenticarPeloNomeUsuario("teste1", string.Empty));
        }

        [TestMethod]
        public void AutenticarPeloNomeUsuario_Senha_Invalida()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();
            var provedorCriptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografia = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                provedorHash,
                provedorCriptografia,
                dadosCriptografia);

            repositorio.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
                HashSenha = provedorHash.GerarHash("teste1"),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            });

            Assert.IsNull(servico.AutenticarPeloNomeUsuario("teste1", "teste2"));
        }
    }
}
