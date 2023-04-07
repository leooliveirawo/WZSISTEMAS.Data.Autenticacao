#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void AutenticarPeloNomeUsuarioOuEmail_PorNomeUsuario()
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

            var token = servico.AutenticarPeloNomeUsuarioOuEmail("teste1", "teste1");

            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void AutenticarPeloNomeUsuarioOuEmail_PorEmail()
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

            var token = servico.AutenticarPeloNomeUsuarioOuEmail("teste1@teste1.com", "teste1");

            Assert.IsNotNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void AutenticarPeloNomeUsuarioOuEmail_NomeUsuarioOuEmail_Nulo()
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

            Assert.IsNull(servico.AutenticarPeloNomeUsuarioOuEmail(default, "teste1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void AutenticarPeloNomeUsuarioOuEmail_NomeUsuarioOuEmail_Vazio()
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

            Assert.IsNull(servico.AutenticarPeloNomeUsuarioOuEmail(string.Empty, "teste1"));
        }

        [TestMethod]
        public void AutenticarPeloNomeUsuarioOuEmail_NomeUsuarioOuEmail_Invalido_PorNomeUsuario()
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

            Assert.IsNull(servico.AutenticarPeloNomeUsuarioOuEmail("teste2", "teste1"));
        }

        [TestMethod]
        public void AutenticarPeloNomeUsuarioOuEmail_NomeUsuarioOuEmail_Invalido_PorEmail()
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

            Assert.IsNull(servico.AutenticarPeloNomeUsuarioOuEmail("teste2@teste1.com", "teste1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void AutenticarPeloNomeUsuarioOuEmail_Senha_Nulo_PorNomeUsuario()
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

            Assert.IsNull(servico.AutenticarPeloNomeUsuarioOuEmail("teste1", default));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void AutenticarPeloNomeUsuarioOuEmail_Senha_Nulo_PorEmail()
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

            Assert.IsNull(servico.AutenticarPeloNomeUsuarioOuEmail("teste1@teste1.com", default));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void AutenticarPeloNomeUsuarioOuEmail_Senha_Vazio_PorNomeUsuario()
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

            Assert.IsNull(servico.AutenticarPeloNomeUsuarioOuEmail("teste1", string.Empty));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void AutenticarPeloNomeUsuarioOuEmail_Senha_Vazio_PorEmail()
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

            Assert.IsNull(servico.AutenticarPeloNomeUsuarioOuEmail("teste1@teste1.com", string.Empty));
        }

        [TestMethod]
        public void AutenticarPeloNomeUsuarioOuEmail_Senha_Invalida_PorNomeUsuario()
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

            Assert.IsNull(servico.AutenticarPeloNomeUsuarioOuEmail("teste1", "teste2"));
        }

        [TestMethod]
        public void AutenticarPeloNomeUsuarioOuEmail_Senha_Invalida_PorEmail()
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

            Assert.IsNull(servico.AutenticarPeloNomeUsuarioOuEmail("teste1@teste1.com", "teste2"));
        }
    }
}
