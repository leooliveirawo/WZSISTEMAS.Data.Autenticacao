#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {

        [TestMethod]
        public void VerificarUsuarioExiste_Existe()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarUsuarioExiste());
        }

        [TestMethod]
        public void VerificarUsuarioExiste_NaoExiste()
        {
            var servico = CriarServicoUsuariosSemTeste();

            Assert.IsFalse(servico.VerificarUsuarioExiste());
        }
    }
}
