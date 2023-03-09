#nullable disable

using Newtonsoft.Json;
using System.Security;

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void ObterPorToken()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
            }, "teste1");

            var token = servico.Autenticar("teste1", "teste1");

            Assert.IsNotNull(servico.ObterPorToken(token));
        }

        [TestMethod]
        public void ObterPorToken_Token_Expirou()
        {
            var criptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografias = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuarios<Usuario>(
                new RepositorioUsuariosSimulador(),
                new ProvedorHashSimulador(),
                criptografia,
                dadosCriptografias);

            var usuario = new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            var token = new Token
            {
                NomeUsuario = "teste1",
                LogadoEm = new DateTime(1999, 1, 1),
                ExpiraEm = new DateTime(1999, 1, 1).AddMinutes(15),
                HashChaveMestre = usuario.HashChaveMestre
            };

            var tokenCriptografado = JsonConvert.SerializeObject(token);

            tokenCriptografado = criptografia.Criptografar(dadosCriptografias.Chave, dadosCriptografias.IV, tokenCriptografado);

            Assert.IsNull(servico.ObterPorToken(tokenCriptografado));
        }

        [TestMethod]
        public void ObterPorToken_Token_Invalido()
        {
            var servico = CriarServicoUsuariosSemTeste();
            var usuario = new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            Assert.IsNull(servico.ObterPorToken("111"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void ObterPorToken_Token_Nulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
            }, "teste1");

            _ = servico.ObterPorToken(default);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void ObterPorToken_Token_Vazio()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
            }, "teste1");

            _ = servico.ObterPorToken(string.Empty);
        }
    }
}
