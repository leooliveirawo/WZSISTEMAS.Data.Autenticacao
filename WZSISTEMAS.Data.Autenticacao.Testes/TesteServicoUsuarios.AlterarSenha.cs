#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void AlterarSenha()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
            };

            servico.Criar(usuario, "teste1");

            var nomeCompleto = usuario.NomeUsuario;
            var hashSenha = usuario.HashSenha;
            var hashChaveMestre = usuario.HashChaveMestre;
            var email = usuario.Email;

            servico.AlterarSenha(usuario, "teste2");

            Assert.AreEqual(usuario.NomeUsuario, nomeCompleto);
            Assert.AreNotEqual(usuario.HashSenha, hashSenha);
            Assert.AreNotEqual(usuario.HashChaveMestre, hashChaveMestre);
            Assert.AreEqual(usuario.Email, email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void AlterarSenha_Usuario_Nulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
            };

            servico.Criar(usuario, "teste1");
            servico.AlterarSenha(default, "teste2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void AlterarSenha_Senha_Nula()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
            };

            servico.Criar(usuario, "teste1");
            servico.AlterarSenha(usuario, default);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void AlterarSenha_Senha_Vazia()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
            };

            servico.Criar(usuario, "teste1");
            servico.AlterarSenha(usuario, string.Empty);
        }
    }
}
