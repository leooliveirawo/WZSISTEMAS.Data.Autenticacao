#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void Criar()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            var usuarioEncontrado = repositorio.Usuarios.SingleOrDefault(x => x.Id == usuario.Id);

            Assert.IsNotNull(usuarioEncontrado);
            Assert.AreEqual(usuario.NomeUsuario, usuarioEncontrado.NomeUsuario);
            Assert.AreEqual(usuario.HashSenha, usuarioEncontrado.HashSenha);
            Assert.AreEqual(usuario.HashChaveMestre, usuarioEncontrado.HashChaveMestre);
            Assert.AreEqual(usuario.Email, usuarioEncontrado.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void Criar_Usuario_Nulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(default, "teste1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Criar_NomeUsuario_Nulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(new Usuario
            {
                NomeUsuario = default,
                Email = "teste1@teste1.com"
            }, "teste1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Criar_NomeUsuario_Vazio()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(new Usuario
            {
                NomeUsuario = string.Empty,
                Email = "teste1@teste1.com"
            }, "teste1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Criar_Email_Nulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = default
            }, "teste1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Criar_Email_Vazio()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = string.Empty
            }, "teste1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Criar_Senha_Nula()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, default);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Criar_Senha_Vazia()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, string.Empty);
        }

        [TestMethod]
        public void Criar_Id_Correto()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            Assert.IsNotNull(repositorio.ObterPorId(1));
        }

        [TestMethod]
        public void Criar_NomeCompleto_Correto()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            Assert.AreEqual(repositorio.ObterPorId(1).NomeUsuario, "teste1");
        }

        [TestMethod]
        public void Criar_Email_Correto()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            Assert.AreEqual(repositorio.ObterPorId(1).Email, "teste1@teste1.com");
        }

        [TestMethod]
        public void Criar_HashSenha_Correto()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                provedorHash,
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            servico.Criar(new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            Assert.AreEqual(repositorio.ObterPorId(1).HashSenha, provedorHash.GerarHash("teste1"));
        }

        [TestMethod]
        public void Criar_HashChaveMestre_Correto()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            servico.Criar(new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            Assert.IsFalse(string.IsNullOrWhiteSpace(repositorio.ObterPorId(1).HashChaveMestre));
        }
    }
}
