#nullable disable

using Newtonsoft.Json;

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void VerificarAutenticao()
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

            Assert.IsTrue(servico.VerificarAutenticacao(tokenCriptografado));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void VerificarAutenticacao_Token_Nulo()
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

            _ = servico.NovaAutenticacao(default);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void VerificarAutenticacao_Token_Vazio()
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

            _ = servico.NovaAutenticacao(string.Empty);
        }

        [TestMethod]
        public void VerificarAutenticacao_Token_Expirou()
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

            Assert.IsFalse(servico.VerificarAutenticacao(tokenCriptografado));
        }

        [TestMethod]
        public void VerificarAutenticacao_NomeUsuario_Invalido()
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
                LogadoEm = DateTime.Now,
                ExpiraEm = DateTime.Now.AddMinutes(15),
                HashChaveMestre = provedorHash.GerarHash($"{DateTime.UtcNow}_{provedorHash.GerarHash("teste1")}_{"teste1"}")
            };

            var tokenCriptografado = JsonConvert.SerializeObject(token);

            tokenCriptografado = provedorCriptografia.Criptografar(dadosCriptografia.Chave, dadosCriptografia.IV, tokenCriptografado);

            Assert.IsFalse(servico.VerificarAutenticacao(tokenCriptografado));
        }

        [TestMethod]
        public void VerificarAutenticacao_HashChaveMestre_Invalido()
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


            Assert.IsFalse(servico.VerificarAutenticacao(tokenCriptografado));
        }
    }
}
