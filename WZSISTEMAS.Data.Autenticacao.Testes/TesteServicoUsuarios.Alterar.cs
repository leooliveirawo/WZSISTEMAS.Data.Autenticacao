#nullable disable

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public partial class TesteServicoUsuarios
    {
        [TestMethod]
        public void Alterar()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                provedorHash,
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

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

            usuario = repositorio.ObterPorId(1);

            usuario.NomeUsuario = "teste2";
            usuario.Email = "teste2@teste2.com";
            usuario.HashSenha = provedorHash.GerarHash("teste2");
            usuario.HashChaveMestre = servico.GerarHashChaveMestre(usuario);

            servico.Alterar(usuario);

            Assert.AreNotEqual(usuario.NomeUsuario, nomeCompleto);
            Assert.AreNotEqual(usuario.HashSenha, hashSenha);
            Assert.AreNotEqual(usuario.HashChaveMestre, hashChaveMestre);
            Assert.AreNotEqual(usuario.Email, email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void Alterar_Usuario_Nulo()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());

            servico.Alterar(default);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Alterar_NomeCompleto_Vazio()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            var usuario = repositorio.ObterPorId(1);

            usuario.NomeUsuario = string.Empty;

            servico.Alterar(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Alterar_NomeCompleto_Nulo()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            var usuario = repositorio.ObterPorId(1);

            usuario.NomeUsuario = default;

            servico.Alterar(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Alterar_Email_Vazio()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            var usuario = repositorio.ObterPorId(1);

            usuario.Email = string.Empty;

            servico.Alterar(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Alterar_Email_Nulo()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            var usuario = repositorio.ObterPorId(1);

            usuario.Email = default;

            servico.Alterar(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Alterar_HashSenha_Nulo()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            var usuario = repositorio.ObterPorId(1);

            usuario.HashSenha = default;

            servico.Alterar(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Alterar_HashSenha_Vazio()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            var usuario = repositorio.ObterPorId(1);

            usuario.HashSenha = string.Empty;

            servico.Alterar(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Alterar_HashChaveMestre_Nulo()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            var usuario = repositorio.ObterPorId(1);

            usuario.HashChaveMestre = default;

            servico.Alterar(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void Alterar_HashChaveMestre_Vazio()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            servico.Criar(new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            }, "teste1");

            var usuario = repositorio.ObterPorId(1);

            usuario.HashChaveMestre = string.Empty;

            servico.Alterar(usuario);
        }

    }
}
