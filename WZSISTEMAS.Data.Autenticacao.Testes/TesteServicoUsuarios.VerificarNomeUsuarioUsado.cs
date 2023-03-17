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

            AdicionarUsuarios(servico);

            Assert.IsFalse(servico.VerificarNomeUsuarioUsado("teste5"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void VerificarNomeUsuarioUsado_ComIdIgnorado_NomeCompleto_Nulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarNomeUsuarioUsado(default, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void VerificarNomeUsuarioUsado_ComIdIgnorado_NomeCompleto_Vazio()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarNomeUsuarioUsado(string.Empty, 1));
        }

        [TestMethod]
        public void VerificarNomeUsuarioUsado_ComIdIgnorado_Usado_1()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarNomeUsuarioUsado("teste1", 2));
        }

        [TestMethod]
        public void VerificarNomeUsuarioUsado_ComIdIgnorado_Usado_2()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsTrue(servico.VerificarNomeUsuarioUsado("teste1", 5));
        }

        [TestMethod]
        public void VerificarNomeUsuarioUsado_ComIdIgnorado_NaoUsado_1()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsFalse(servico.VerificarNomeUsuarioUsado("teste2", 2));
        }

        [TestMethod]
        public void VerificarNomeUsuarioUsado_ComIdIgnorado_NaoUsado_2()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsFalse(servico.VerificarNomeUsuarioUsado("teste5", 2));
        }

        [TestMethod]
        public void VerificarNomeUsuarioUsado_ComIdIgnorado_NaoUsado_3()
        {
            var servico = CriarServicoUsuariosSemTeste();

            AdicionarUsuarios(servico);

            Assert.IsFalse(servico.VerificarNomeUsuarioUsado("teste5", 5));
        }
    }
}
