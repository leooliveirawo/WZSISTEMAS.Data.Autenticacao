#nullable disable

using Newtonsoft.Json;

using System.Text.Json;

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void NovaAutenticao()
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

            var token = new Token
            {
                NomeUsuario = "teste1",
                LogadoEm = DateTime.UtcNow,
                ExpiraEm = DateTime.UtcNow.AddMinutes(15),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            };

            var tokenJson = System.Text.Json.JsonSerializer.Serialize(token);

            var tokenCriptografado = provedorCriptografia.Criptografar(dadosCriptografia.Chave, dadosCriptografia.IV, tokenJson);

            var novoToken = servico.NovaAutenticacao(tokenCriptografado);

            Assert.AreNotEqual(token, novoToken);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void NovaAutenticao_Token_Nulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            _ = servico.NovaAutenticacao(default);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void NovaAutenticao_Token_Vazio()
        {
            var servico = CriarServicoUsuariosSemTeste();

            _ = servico.NovaAutenticacao(string.Empty);
        }

        [TestMethod]
        public void NovaAutenticao_Token_Expirou()
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

            var token = new Token
            {
                NomeUsuario = "teste1",
                LogadoEm = new DateTime(1999, 1, 1),
                ExpiraEm = new DateTime(1999, 1, 1).AddMinutes(15),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            };

            var tokenCriptografado = JsonConvert.SerializeObject(token);

            tokenCriptografado = provedorCriptografia.Criptografar(dadosCriptografia.Chave, dadosCriptografia.IV, tokenCriptografado);

            Assert.IsNull(servico.NovaAutenticacao(tokenCriptografado));
        }

        [TestMethod]
        public void NovaAutenticao_NomeUsuario_Invalido()
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

            var token = new Token
            {
                NomeUsuario = "teste2",
                LogadoEm = DateTime.UtcNow,
                ExpiraEm = DateTime.UtcNow.AddMinutes(15),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            };

            var tokenJson = System.Text.Json.JsonSerializer.Serialize(token);

            var tokenCriptografado = provedorCriptografia.Criptografar(dadosCriptografia.Chave, dadosCriptografia.IV, tokenJson);

            var novoToken = servico.NovaAutenticacao(tokenCriptografado);

            Assert.IsNull(servico.NovaAutenticacao(tokenCriptografado));
        }

        [TestMethod]
        public void NovaAutenticao_HashChaveMestre_Invalido()
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

            var token = new Token
            {
                NomeUsuario = "teste1",
                LogadoEm = DateTime.UtcNow,
                ExpiraEm = DateTime.UtcNow.AddMinutes(15),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}_erro")
            };

            var tokenJson = System.Text.Json.JsonSerializer.Serialize(token);

            var tokenCriptografado = provedorCriptografia.Criptografar(dadosCriptografia.Chave, dadosCriptografia.IV, tokenJson);

            var novoToken = servico.NovaAutenticacao(tokenCriptografado);

            Assert.IsNull(servico.NovaAutenticacao(tokenCriptografado));
        }
    }
}
