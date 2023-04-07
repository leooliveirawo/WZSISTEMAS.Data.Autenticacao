using System.Security;
using System.Text.Json;

using WZSISTEMAS.Data.Autenticacao.Interfaces;
using WZSISTEMAS.Data.Criptografia.Interfaces;

namespace WZSISTEMAS.Data.Autenticacao
{
    /// <summary>
    /// Representa os serviços de usuários e de autenticação.
    /// </summary>
    /// <typeparam name="TUsuario">O tipo da classe de usuário.</typeparam>
    public class ServicoUsuarios<TUsuario> : IServicoUsuarios<TUsuario> where TUsuario : Usuario
    {
        /// <summary>
        /// A instância do repositório dos usuários.
        /// </summary>
        private readonly IRepositorioUsuarios<TUsuario> repositorio;
        /// <summary>
        /// A instância do provedor de hash.
        /// </summary>
        private readonly IProvedorHash provedorHash;
        /// <summary>
        /// A instância do provedor de criptografia.
        /// </summary>
        private readonly IProvedorCriptografia provedorCriptografia;
        /// <summary>
        /// Os dados utilizados pelo provedor de criptografia para cripografar ou descriptografar
        /// </summary>
        private readonly IDadosCriptografiaAutenticacao dadosCriptografia;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ServicoUsuarios{TUsuario}"/>.
        /// </summary>
        /// <param name="repositorio">O repositório dos usuários.</param>
        /// <param name="provedorHash">O provedor que gera hashs.</param>
        /// <param name="provedorCriptografia">O provedor de cripografia.</param>
        /// <param name="dadosCriptografia">Os dados utilizados pelo provedor de criptografia para cripografar ou descriptografar.</param>
        /// <exception cref="ArgumentNullException"><paramref name="repositorio"/> é nulo.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="provedorHash"/> é nulo.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="provedorCriptografia"/> é nulo.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="dadosCriptografia"/> é nulo.</exception>
        public ServicoUsuarios(
            IRepositorioUsuarios<TUsuario>   repositorio,
            IProvedorHash provedorHash,
            IProvedorCriptografia provedorCriptografia,
            IDadosCriptografiaAutenticacao dadosCriptografia)
        {
            this.repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            this.provedorHash = provedorHash ?? throw new ArgumentNullException(nameof(provedorHash));
            this.provedorCriptografia = provedorCriptografia ?? throw new ArgumentNullException(nameof(provedorCriptografia));
            this.dadosCriptografia = dadosCriptografia ?? throw new ArgumentNullException(nameof(dadosCriptografia));
        }

        /// <summary>
        /// Gera um novo hash da chave mestre.
        /// </summary>
        /// <param name="usuario">Os dados do usuário.</param>
        /// <returns>O hash da chave mestre.</returns>
        protected virtual string GerarHashChaveMestre(TUsuario usuario)
        {
            return provedorHash.GerarHash($"{DateTime.UtcNow}_{usuario.HashSenha}_{usuario.NomeUsuario}");
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Token"/>.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário realizado ao <see cref="Token"/>.</param>
        /// <returns></returns>
        protected virtual Token CriarToken(string nomeUsuario)
        {
            return new Token
            {
                NomeUsuario = nomeUsuario,
                LogadoEm = DateTime.UtcNow,
                ExpiraEm = DateTime.UtcNow.AddMinutes(15),
                HashChaveMestre = repositorio.ObterHashChaveMestre(nomeUsuario) ?? throw new InvalidOperationException("Nenhum hash da chave mestre que correpondá ao nome de usuário foi encontrado")
            };
        }

        /// <summary>
        /// Altera o cadastro de um usuário existente.
        /// </summary>
        /// <param name="usuario">Os dados do usuário que será alterado.</param>
        /// <exception cref="ArgumentNullException"><paramref name="usuario"/> é nulo.</exception>
        /// <exception cref="ArgumentException"><see cref="Usuario.Id"/> de <paramref name="usuario"/> não foi informado.</exception>
        /// <exception cref="ArgumentException"><see cref="Usuario.NomeUsuario"/> de <paramref name="usuario"/> não foi informado.</exception>
        /// <exception cref="ArgumentException"><see cref="Usuario.HashSenha"/> de <paramref name="usuario"/> não foi informado.</exception>
        /// <exception cref="ArgumentException"><see cref="Usuario.HashChaveMestre"/> de <paramref name="usuario"/> não foi informado.</exception>
        /// <exception cref="ArgumentException"><see cref="Usuario.Email"/> de <paramref name="usuario"/> não foi informado.</exception>
        public virtual void Alterar(TUsuario usuario)
        {
            usuario.VerificarNulo(nameof(usuario));
            usuario.Id.VerificarZeroOuNegativo("O Id do usuário não foi informado", nameof(usuario));
            usuario.NomeUsuario.VerificarVazioOuNulo("O Id do usuário não foi informado", nameof(usuario));
            usuario.HashSenha.VerificarVazioOuNulo("O hash da senha não foi informado", nameof(usuario));
            usuario.Email.VerificarVazioOuNulo("O endereço de e-mail não foi informado", nameof(usuario));
            usuario.HashChaveMestre.VerificarVazioOuNulo("O hash da chave mestre não foi informado", nameof(usuario));

            repositorio.Alterar(usuario);
        }

        /// <summary>
        /// Altera a senha do cadastro de usuário informado.
        /// </summary>
        /// <param name="usuario">O usuário que a senha será alterado.</param>
        /// <param name="senha">A nova senha do usuário.</param>
        /// <exception cref="ArgumentNullException"><paramref name="usuario"/> é nulo.</exception>
        /// <exception cref="ArgumentException"><paramref name="senha"/> não foi informado.</exception>
        public virtual void AlterarSenha(TUsuario usuario, string senha)
        {
            usuario.VerificarNulo(nameof(usuario));
            senha.VerificarVazioOuNulo("A senha não foi informada", nameof(senha));

            usuario.HashSenha = provedorHash.GerarHash(senha);
            usuario.HashChaveMestre = GerarHashChaveMestre(usuario);

            Alterar(usuario);
        }

        /// <summary>
        /// Cria um novo cadastro de usuário.
        /// </summary>
        /// <param name="usuario">Os dados do usuário que será criado.</param>
        /// <param name="senha">A nova senha do usuário.</param>
        /// <exception cref="ArgumentNullException"><paramref name="usuario"/> é nulo.</exception>
        /// <exception cref="ArgumentException"><see cref="Usuario.NomeUsuario"/> de <paramref name="usuario"/> não foi informado.</exception>
        /// <exception cref="ArgumentException"><see cref="Usuario.Email"/> de <paramref name="usuario"/> não foi informado.</exception>
        public void Criar(TUsuario usuario, string senha)
        {
            usuario.VerificarNulo(nameof(usuario));
            usuario.NomeUsuario.VerificarVazioOuNulo("O Id do usuário não foi informado", nameof(usuario));
            senha.VerificarVazioOuNulo("O endereço de e-mail não foi informado", nameof(senha));
            usuario.Email.VerificarVazioOuNulo("O endereço de e-mail não foi informado", nameof(usuario));

            usuario.HashSenha = provedorHash.GerarHash(senha);
            usuario.HashChaveMestre = GerarHashChaveMestre(usuario);

            repositorio.Criar(usuario);
        }

        /// <summary>
        /// Exclui um cadastro de usuário que correspondá ao Id especificado.
        /// </summary>
        /// <param name="id">O Id do cadastro do usuário que será excluído.</param>
        /// <exception cref="ArgumentException"><paramref name="id"/> é zero ou negativo.</exception>
        public virtual void Excluir(long id)
        {
            id.VerificarZeroOuNegativo("O Id do usuário não foi informado", nameof(id));

            repositorio.Excluir(id);
        }

        /// <summary>
        /// Realiza uma nova autenticação com base em uma autenticação existente.
        /// </summary>
        /// <param name="token">O token da autenticação atual.</param>
        /// <returns>O token da nova autenticação.</returns>
        public virtual string? NovaAutenticacao(string token)
        {
            token.VerificarVazioOuNulo("O token não foi informado", nameof(token));

            if (VerificarAutenticacao(token))
            {
                var tokenJsonDescriptografado = provedorCriptografia.Descriptografar(dadosCriptografia.Chave, dadosCriptografia.IV, token);

                var tokenInstancia = JsonSerializer.Deserialize<Token>(tokenJsonDescriptografado);

                #nullable disable
                var novoTokenInstancia = CriarToken(tokenInstancia.NomeUsuario);
                #nullable enable

                var novoTokenJson = JsonSerializer.Serialize(novoTokenInstancia);

                return provedorCriptografia.Criptografar(dadosCriptografia.Chave, dadosCriptografia.IV, novoTokenJson);
            }

            return default;
        }

        /// <summary>
        /// Realiza uma autenticação se o nome de usuário e a senha estiverem corretos.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário ao ser autenticação.</param>
        /// <param name="senha">A senha do usuário ao ser autenticado.</param>
        /// <returns>O token da autenticação ou nulo se a autenticação não for bem sucedido.</returns>
        /// <exception cref="ArgumentException"><paramref name="nomeUsuario"/> não foi informado.</exception>
        /// <exception cref="ArgumentException"><paramref name="senha"/> não foi informado.</exception>
        [Obsolete($"Utilize o método {nameof(IServicoUsuarios<TUsuario>.AutenticarPeloNomeUsuario)}")]
        public virtual string? Autenticar(string nomeUsuario, string senha)
        {
            return AutenticarPeloNomeUsuario(nomeUsuario, senha);
        }

        /// <summary>
        /// Realiza uma autenticação se o nome de usuário e a senha estiverem corretos.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário ao ser autenticação.</param>
        /// <param name="senha">A senha do usuário ao ser autenticado.</param>
        /// <returns>O token da autenticação ou nulo se a autenticação não for bem sucedido.</returns>
        /// <exception cref="ArgumentException"><paramref name="nomeUsuario"/> não foi informado.</exception>
        /// <exception cref="ArgumentException"><paramref name="senha"/> não foi informado.</exception>
        public virtual string? AutenticarPeloNomeUsuario(string nomeUsuario, string senha)
        {
            nomeUsuario.VerificarVazioOuNulo("O nome de usuário não foi informado", nameof(nomeUsuario));
            senha.VerificarVazioOuNulo("A senha não foi informada", nameof(senha));

            var hashSenha = provedorHash.GerarHash(senha);

            if (repositorio.VerificarNomeUsuarioEHashSenha(nomeUsuario, hashSenha))
            {
                var token = CriarToken(nomeUsuario);

                return provedorCriptografia.Criptografar(dadosCriptografia.Chave, dadosCriptografia.IV, JsonSerializer.Serialize(token));
            }

            return null;
        }

        /// <summary>
        /// Realiza uma autenticação se o e-mail e a senha estiverem corretos.
        /// </summary>
        /// <param name="email">O e-mail do usuário ao ser autenticação.</param>
        /// <param name="senha">A senha do usuário ao ser autenticado.</param>
        /// <returns>O token da autenticação ou nulo se a autenticação não for bem sucedido.</returns>
        /// <exception cref="ArgumentException"><paramref name="email"/> não foi informado.</exception>
        /// <exception cref="ArgumentException"><paramref name="senha"/> não foi informado.</exception>
        public virtual string? AutenticarPeloEmail(string email, string senha)
        {
            email.VerificarVazioOuNulo("O e-mail não foi informado", nameof(email));
            senha.VerificarVazioOuNulo("A senha não foi informada", nameof(senha));

            var hashSenha = provedorHash.GerarHash(senha);

            if (repositorio.VerificarEmailEHashSenha(email, hashSenha))
            {
                var usuario = repositorio.ObterPorEmail(email) ?? throw new InvalidOperationException("O usuário não foi encontrado");

                var token = CriarToken(usuario.NomeUsuario);

                return provedorCriptografia.Criptografar(dadosCriptografia.Chave, dadosCriptografia.IV, JsonSerializer.Serialize(token));
            }

            return null;
        }

        /// <summary>
        /// Realiza uma autenticação se o nome de usuário ou e-mail e a senha estiverem corretos.
        /// </summary>
        /// <param name="nomeUsuarioOuEmail">O nome de usuário ou e-mail do usuário ao ser autenticação.</param>
        /// <param name="senha">A senha do usuário ao ser autenticado.</param>
        /// <returns>O token da autenticação ou nulo se a autenticação não for bem sucedido.</returns>
        /// <exception cref="ArgumentException"><paramref name="nomeUsuarioOuEmail"/> não foi informado.</exception>
        /// <exception cref="ArgumentException"><paramref name="senha"/> não foi informado.</exception>
        public virtual string? AutenticarPeloNomeUsuarioOuEmail(string nomeUsuarioOuEmail, string senha)
        {
            nomeUsuarioOuEmail.VerificarVazioOuNulo("O nome de usuário ou e-mail não foi informado", nameof(nomeUsuarioOuEmail));
            senha.VerificarVazioOuNulo("A senha não foi informada", nameof(senha));

            var hashSenha = provedorHash.GerarHash(senha);

            if (repositorio.VerificarNomeUsuarioOuEmailEHashSenha(nomeUsuarioOuEmail, hashSenha))
            {
                var usuario = repositorio.ObterPorNomeUsuarioOuEmail(nomeUsuarioOuEmail) ?? throw new InvalidOperationException("O usuário não foi encontrado");

                var token = CriarToken(usuario.NomeUsuario);

                return provedorCriptografia.Criptografar(dadosCriptografia.Chave, dadosCriptografia.IV, JsonSerializer.Serialize(token));
            }

            return null;
        }

        /// <summary>
        /// Obtém um cadastro do usuário que correspondá ao Id especificado, ou nulo se não existir.
        /// </summary>
        /// <param name="id">O Id do cadastro de usuário que será retornado.</param>
        /// <returns>O cadastro do usuário que correspondá ao Id especificado, ou nulo se não existir.</returns>
        /// <exception cref="ArgumentException"><paramref name="id"/> é zero ou negativo.</exception>
        public virtual TUsuario? ObterPorId(long id)
        {
            id.VerificarZeroOuNegativo("O Id do usuário não foi informado", nameof(id));

            return repositorio.ObterPorId(id);
        }

        /// <summary>
        /// Obtém todos os cadastros dos usuários existem em uma lista simplicada contendo o Id, nome de usuário e e-mail.
        /// </summary>
        /// <returns>Todos os cadastros dos usuários existem em uma lista simplicada contendo o Id, nome de usuário e e-mail.</returns>
        public virtual IEnumerable<TUsuario> ObterTudo()
        {
            return repositorio.ObterTudo();
        }

        /// <summary>
        /// Verifica se o token informado está autenticado.
        /// </summary>
        /// <param name="token">O token que será verificado se está autenticado.</param>
        /// <returns>Um valor <see cref="bool"/> representando se o token informado está autenticado.</returns>
        /// <exception cref="ArgumentException"><paramref name="token"/> não foi informado.</exception>
        public virtual bool VerificarAutenticacao(string token)
        {
            token.VerificarVazioOuNulo("O token não foi informado", nameof(token));

            try
            {
                var tokenDescriptografado = provedorCriptografia.Descriptografar(dadosCriptografia.Chave, dadosCriptografia.IV, token);

                var tokenInstancia = JsonSerializer.Deserialize<Token>(tokenDescriptografado);

                if (tokenInstancia is null)
                    return false;

                if (tokenInstancia.ExpiraEm < DateTime.Now)
                    return false;

                if (repositorio.VerificarHashChaveMestre(tokenInstancia.NomeUsuario, tokenInstancia.HashChaveMestre))
                    return true;

                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se existem cadastros de usuários.
        /// </summary>
        /// <returns>Um valor <see cref="bool"/> representando se existem cadastros de usuários.</returns>
        public virtual bool VerificarUsuarioExiste()
        {
            return repositorio.VerificarUsuarioExiste();
        }

        /// <summary>
        /// Obtém um cadastro de usuário que correspondá ao token de autenticação.
        /// </summary>
        /// <param name="token">O token de autenticação do usuário que será obtido.</param>
        /// <returns>O cadastro de usuário que correspondá ao token de autenticaçã.</returns>
        public virtual TUsuario? ObterPorToken(string token)
        {
            token.VerificarVazioOuNulo("O token não foi informado", nameof(token));

            if (VerificarAutenticacao(token))
            {
                var tokenJsonDescriptografado = provedorCriptografia.Descriptografar(dadosCriptografia.Chave, dadosCriptografia.IV, token);

                var tokenInstancia = JsonSerializer.Deserialize<Token>(tokenJsonDescriptografado);

                #nullable disable
                return repositorio.ObterPorNomeUsuario(tokenInstancia.NomeUsuario);
                #nullable enable
            }

            return default;
        }

        /// <summary>
        /// Obtém um cadastro de usuário existente que correspondá ao nome de usuário especificado.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário que será obtido.</param>
        /// <returns>O cadastro de usuário existente que correspondá ao nome de usuário especificado.</returns>
        public virtual TUsuario? ObterPorNomeUsuario(string nomeUsuario)
        {
            nomeUsuario.VerificarVazioOuNulo("O nome de usuário não foi informado", nameof(nomeUsuario));

            return repositorio.ObterPorNomeUsuario(nomeUsuario);
        }

        /// <summary>
        /// Obtém um cadastro de usuário existente que correspondá ao e-mail especificado.
        /// </summary>
        /// <param name="email">O e-mail do usuário que será obtido.</param>
        /// <returns>O cadastro de usuário existente que correspondá ao e-mail especificado.</returns>
        public virtual TUsuario? ObterPorEmail(string email)
        {
            email.VerificarVazioOuNulo("O e-mail não foi informado", nameof(email));

            return repositorio.ObterPorEmail(email);
        }

        /// <summary>
        /// Verifica se o nome de usuário informado está sendo utilizado por algum usuário existente.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o nome de usuário informado está sendo utilizado por algum usuário existente.</returns>
        public virtual bool VerificarNomeUsuarioUsado(string nomeUsuario)
        {
            nomeUsuario.VerificarVazioOuNulo("O nome de usuário não foi informado", nameof(nomeUsuario));

            return repositorio.VerificarNomeUsuarioUsado(nomeUsuario);
        }

        /// <summary>
        /// Verifica se o e-mail informado está sendo utilizado por algum usuário existente.
        /// </summary>
        /// <param name="email">O e-mail do usuário.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o e-mail informado está sendo utilizado por algum usuário existente.</returns>
        public virtual bool VerificarEmailUsado(string email)
        {
            email.VerificarVazioOuNulo("O e-mail não foi informado", nameof(email));

            return repositorio.VerificarEmailUsado(email);
        }

        /// <summary>
        /// Verifica se o nome de usuário informado está sendo utilizado por algum usuário existente que não correspondá ao Id informado.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário.</param>
        /// <param name="idIgnorado">O Id do usuário ignorado.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o nome de usuário informado está sendo utilizado por algum usuário existente que não correspondá ao Id informado.</returns>
        public bool VerificarNomeUsuarioUsado(string nomeUsuario, long idIgnorado)
        {
            nomeUsuario.VerificarVazioOuNulo("O nome de usuário não foi informado", nameof(nomeUsuario));

            return repositorio.VerificarNomeUsuarioUsado(nomeUsuario, idIgnorado);
        }

        /// <summary>
        /// Verifica se o e-mail informado está sendo utilizado por algum usuário existente que não correspondá ao Id informado.
        /// </summary>
        /// <param name="email">O e-mail do usuário.</param>
        /// <param name="idIgnorado">O Id do usuário ignorado.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o e-mail informado está sendo utilizado por algum usuário existente que não correspondá ao Id informado.</returns>
        public bool VerificarEmailUsado(string email, long idIgnorado)
        {
            email.VerificarVazioOuNulo("O e-mail não foi informado", nameof(email));

            return repositorio.VerificarEmailUsado(email, idIgnorado);
        }
    }
}
