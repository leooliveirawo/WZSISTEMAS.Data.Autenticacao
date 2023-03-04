using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Security;

namespace WZSISTEMAS.Data.Autenticacao.Testes
{
#nullable disable
    [TestClass]
    public class TesteServicosUsuarios
    {
        private ServicoUsuarios<Usuario> CriarServicoUsuariosSemTeste()
        {
            return new ServicoUsuarios<Usuario>(
                new RepositorioUsuariosSimulador(),
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));
        }

        [TestMethod]
        public void CriarServicoUsuarios()
        {
            CriarServicoUsuariosSemTeste();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void CriarServicoUsuariosRepositorioNulo()
        {
            _ = new ServicoUsuarios<Usuario>(
                default,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void CriarServicoUsuariosProvedorHashNulo()
        {
            _ = new ServicoUsuarios<Usuario>(
                new RepositorioUsuariosSimulador(),
                default,
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void CriarServicoUsuariosProvedorCriptografiaNulo()
        {
            _ = new ServicoUsuarios<Usuario>(
                new RepositorioUsuariosSimulador(),
                new ProvedorHashSimulador(),
                default,
                new DadosCriptografiaSimulador("1", "1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void CriarServicoUsuariosDadosCriptografiaNula()
        {
            _ = new ServicoUsuarios<Usuario>(
                new RepositorioUsuariosSimulador(),
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                default);
        }

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
        public void CriarUsuarioNulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Criar(default, "teste1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void CriarNomeNaoInformado()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario()
            {
                NomeUsuario = string.Empty,
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void CriarEmailNaoInformado()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = string.Empty
            };

            servico.Criar(usuario, "teste1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void CriarSenhaNaoInformada()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, string.Empty);
        }

        [TestMethod]
        public void CriarHashSenhaCorreto()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                provedorHash,
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            var senha = "teste1";
            var hashSenha = provedorHash.GerarHash(senha);

            servico.Criar(usuario, senha);

            usuario = repositorio.Usuarios.Single(x => x.Id == usuario.Id);

            Assert.AreEqual(usuario.HashSenha, hashSenha);
        }

        [TestMethod]
        public void CriarHashChaveMestre()
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

            usuario = repositorio.Usuarios.Single(x => x.Id == usuario.Id);

            Assert.IsTrue(!string.IsNullOrWhiteSpace(usuario.HashChaveMestre));
        }

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
        public void AlterarUsuarioNulo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
            };

            servico.Criar(usuario, "teste1");
            servico.Alterar(default);
        }

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
        public void AlterarSenhaUsuarioNulo()
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
        public void AlterarSenhaSenhaNaoInformada()
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

        [TestMethod]
        public void Excluir()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuariosSimulador(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
            };

            servico.Criar(usuario, "teste1");
            servico.Excluir(usuario.Id);

            Assert.IsNull(repositorio.ObterPorId(usuario.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void ExcluirIdZero()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Excluir(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void ExcluirIdNegativo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.Excluir(-1);
        }

        [TestMethod]
        public void ObterPorId()
        {
            var repositorio = new RepositorioUsuariosSimulador();
            var provedorHash = new ProvedorHashSimulador();

            var usuario = new Usuario()
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com",
                HashSenha = provedorHash.GerarHash("teste1")
            };

            repositorio.Criar(usuario);

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                provedorHash,
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            Assert.IsNotNull(servico.ObterPorId(usuario.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void ObterPorIdIdZero()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.ObterPorId(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void ObterPorIdIdNegativo()
        {
            var servico = CriarServicoUsuariosSemTeste();

            servico.ObterPorId(-1);
        }

        [TestMethod]
        public void ObterPorIdNaoExiste()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            repositorio.Criar(new Usuario());

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            Assert.IsTrue(servico.ObterPorId(1000) is null);
        }

        [TestMethod]
        public void ObterTudoContemItens()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());
            repositorio.Criar(new Usuario());
            repositorio.Criar(new Usuario());
            repositorio.Criar(new Usuario());

            var usuarios = servico.ObterTudo();

            Assert.IsTrue(usuarios.Count() == 4);
        }

        [TestMethod]
        public void ObterTudoNaoContemItens()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            var usuarios = servico.ObterTudo();

            Assert.IsFalse(usuarios.Any());
        }

        [TestMethod]
        public void VerificarUsuarioExisteExiste()
        {
            var repositorio = new RepositorioUsuariosSimulador();

            var servico = new ServicoUsuarios<Usuario>(
                repositorio,
                new ProvedorHashSimulador(),
                new ProvedorCriptografaSimulador(),
                new DadosCriptografiaSimulador("1", "1"));

            repositorio.Criar(new Usuario());
            repositorio.Criar(new Usuario());
            repositorio.Criar(new Usuario());
            repositorio.Criar(new Usuario());

            Assert.IsTrue(servico.VerificarUsuarioExiste());
        }

        [TestMethod]
        public void VerificarUsuarioExisteNaoExiste()
        {
            var servico = CriarServicoUsuariosSemTeste();

            Assert.IsFalse(servico.VerificarUsuarioExiste());
        }

        [TestMethod]
        public void AutenticarAutenticado()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            var token = servico.Autenticar("teste1", "teste1");

            Assert.IsNotNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(SecurityException), AllowDerivedTypes = false)]
        public void AutenticarNaoAutenticadoNomeUsuarioInvalido()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            var token = servico.Autenticar("teste2", "teste1");

            Assert.IsNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(SecurityException), AllowDerivedTypes = false)]
        public void AutenticarNaoAutenticadoSenhaInvalida()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            var token = servico.Autenticar("teste1", "teste2");

            Assert.IsNull(token);
        }

        [TestMethod]
        public void NovaAutenticao()
        {
            var servico = CriarServicoUsuariosSemTeste();

            var usuario = new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            var token = servico.Autenticar("teste1", "teste1");

            var novoToken = servico.NovaAutenticacao(token);

            Assert.AreNotEqual(token, novoToken);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void NovaAutenticaoTokenNaoInformado()
        {
            var servico = CriarServicoUsuariosSemTeste();

            _ = servico.NovaAutenticacao(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(SecurityException), AllowDerivedTypes = false)]
        public void NovaAutenticaoTokenExpirou()
        {
            var criptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografias = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuarios<Usuario>(
                new RepositorioUsuariosSimulador(),
                new ProvedorHashSimulador(),
                criptografia,
                new DadosCriptografiaSimulador("1", "1"));

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

            _ = servico.NovaAutenticacao(tokenCriptografado);
        }

        [TestMethod]
        [ExpectedException(typeof(SecurityException), AllowDerivedTypes = false)]
        public void NovaAutenticaoNomeUsuarioInvalido()
        {
            var criptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografias = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuarios<Usuario>(
                new RepositorioUsuariosSimulador(),
                new ProvedorHashSimulador(),
                criptografia,
                new DadosCriptografiaSimulador("1", "1"));

            var usuario = new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            var token = new Token
            {
                NomeUsuario = "teste2",
                LogadoEm = DateTime.Now,
                ExpiraEm = DateTime.Now.AddMinutes(15),
                HashChaveMestre = usuario.HashChaveMestre
            };

            var tokenCriptografado = JsonConvert.SerializeObject(token);

            tokenCriptografado = criptografia.Criptografar(dadosCriptografias.Chave, dadosCriptografias.IV, tokenCriptografado);

            _ = servico.NovaAutenticacao(tokenCriptografado);

        }

        [TestMethod]
        [ExpectedException(typeof(SecurityException), AllowDerivedTypes = false)]
        public void NovaAutenticaoHashChaveMestreInvalido()
        {
            var criptografia = new ProvedorCriptografaSimulador();
            var dadosCriptografias = new DadosCriptografiaSimulador("1", "1");

            var servico = new ServicoUsuarios<Usuario>(
                new RepositorioUsuariosSimulador(),
                new ProvedorHashSimulador(),
                criptografia,
                new DadosCriptografiaSimulador("1", "1"));

            var usuario = new Usuario
            {
                NomeUsuario = "teste1",
                Email = "teste1@teste1.com"
            };

            servico.Criar(usuario, "teste1");

            var token = new Token
            {
                NomeUsuario = "teste1",
                LogadoEm = DateTime.Now,
                ExpiraEm = DateTime.Now.AddMinutes(15),
                HashChaveMestre = usuario.HashChaveMestre + "_erro"
            };

            var tokenCriptografado = JsonConvert.SerializeObject(token);

            tokenCriptografado = criptografia.Criptografar(dadosCriptografias.Chave, dadosCriptografias.IV, tokenCriptografado);

            _ = servico.NovaAutenticacao(tokenCriptografado);

        }
    }
}
