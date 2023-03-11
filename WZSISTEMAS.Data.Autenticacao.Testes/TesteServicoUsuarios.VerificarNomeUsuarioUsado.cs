#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void VerificarNomeUsuarioUsado_Nulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarNomeUsuarioUsado(default));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void VerificarNomeUsuarioUsado_Vazio()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarNomeUsuarioUsado(string.Empty));
        }

        [TestMethod]
        public void VerificarNomeUsuarioUsado_Usado()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarNomeUsuarioUsado("teste1"));
        }

        [TestMethod]
        public void VerificarNomeUsuarioUsado_NaoUsado()
        {
            var servico = CriarServicoUsuariosSemTeste();

            Assert.IsFalse(servico.VerificarNomeUsuarioUsado("teste5"));
        }
    }
}
