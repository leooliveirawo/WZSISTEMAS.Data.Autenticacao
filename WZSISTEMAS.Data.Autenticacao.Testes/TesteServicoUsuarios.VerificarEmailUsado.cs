#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void VerificarEmailUsado_Nulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarEmailUsado(default));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void VerificarEmailUsado_Vazio()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarEmailUsado(string.Empty));
        }

        [TestMethod]
        public void VerificarEmailUsado_Usado()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarEmailUsado("teste1@teste1.com"));
        }

        [TestMethod]
        public void VerificarEmailUsado_NaoUsado()
        {
            var servico = CriarServicoUsuariosSemTeste();

            Assert.IsFalse(servico.VerificarEmailUsado("teste5@teste5.com"));
        }
    }
}
