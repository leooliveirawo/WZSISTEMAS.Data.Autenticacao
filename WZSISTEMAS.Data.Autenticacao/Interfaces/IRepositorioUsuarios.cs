namespace WZSISTEMAS.Data.Autenticacao.Interfaces
{
    /// <summary>
    /// Representa o repositório de usuários.
    /// </summary>
    /// <typeparam name="TUsuario">O tipo de usuário do repositório.</typeparam>
    public interface IRepositorioUsuarios<TUsuario> where TUsuario : Usuario
    {
        /// <summary>
        /// Cria um novo usuário no repositório.
        /// </summary>
        /// <param name="usuario">O usuário que será criado.</param>
        void Criar(TUsuario usuario);

        /// <summary>
        /// Altera um usuário existente no repositório.
        /// </summary>
        /// <param name="usuario">O usuário que será criado.</param>
        void Alterar(TUsuario usuario);

        /// <summary>
        /// Excluí um usuário existente no respositório que correspondá ao Id especificado.
        /// </summary>
        /// <param name="id">O Id do usuário que será excluído.</param>
        void Excluir(long id);

        /// <summary>
        /// Obtém um usuário existente no repositório que correspondá ao Id especificado.
        /// </summary>
        /// <param name="id">O Id do usuário que será obtido.</param>
        /// <returns>O usuário existente no repositório que correspondá ao Id especificado.</returns>
        TUsuario? ObterPorId(long id);

        /// <summary>
        /// Obtém um usuário existente no repositório que correspondá ao nome de usuário especificado.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário que será obtido.</param>
        /// <returns>O usuário existente no repositório que correspondá ao nome de usuário especificado.</returns>
        TUsuario? ObterPorNomeUsuario(string nomeUsuario);

        /// <summary>
        /// Obtém um cadastro de usuário existente que correspondá ao e-mail especificado.
        /// </summary>
        /// <param name="email">O e-mail do usuário que será obtido.</param>
        /// <returns>O cadastro de usuário existente que correspondá ao e-mail especificado.</returns>
        TUsuario? ObterPorEmail(string email);

        /// <summary>
        /// Obtém um cadastro de usuário existente que correspondá ao nome de usuário ou e-mail especificado.
        /// </summary>
        /// <param name="nomeUsuarioOuEmail">O nome de usuário ou e-mail do usuário que será obtido.</param>
        /// <returns>O cadastro de usuário existente que correspondá ao nome de usuário ou e-mail especificado.</returns>
        TUsuario? ObterPorNomeUsuarioOuEmail(string nomeUsuarioOuEmail);

        /// <summary>
        /// Obtém todos os usuários existentes no repositório.
        /// </summary>
        /// <returns>Todos os usuários existentes no repositório.</returns>
        IEnumerable<TUsuario> ObterTudo();

        /// <summary>
        /// Obtém o nome de usuário de um usuário existente no repositório que correspondá ao Id informado.
        /// </summary>
        /// <param name="id">O Id do usuário que será obtido.</param>
        /// <returns>O nome de usuário de um usuário existente no repositório que correspondá ao Id informado.</returns>
        string? ObterNomeUsuarioPorId(long id);

        /// <summary>
        /// Verifica se o hash da senha informado corresponde ao hash da senha do usuário com o nome de usuário informado.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário.</param>
        /// <param name="hashSenha">O hash da senha que será comparado ao hash da senha do usuário.</param>
        /// <returns>Um valor <see cref="bool"/> representando se o hash da senha informado corresponde ao hash da senha do usuário com o nome de usuário informado.</returns>
        bool VerificarNomeUsuarioEHashSenha(string nomeUsuario, string hashSenha);

        /// <summary>
        /// Verifica se o hash da senha informado corresponde ao hash da senha do usuário com o e-mail informado.
        /// </summary>
        /// <param name="email">O e-mail do usuário.</param>
        /// <param name="hashSenha">O hash da senha que será comparado ao hash da senha do usuário.</param>
        /// <returns>Um valor <see cref="bool"/> representando se o hash da senha informado corresponde ao hash da senha do usuário com o e-mail informado.</returns>
        bool VerificarEmailEHashSenha(string email, string hashSenha);

        /// <summary>
        /// Verifica se o hash da senha informado corresponde ao hash da senha do usuário com o nome de usuário ou e-mail informado.
        /// </summary>
        /// <param name="nomeUsuarioOuEmail">O nome de usuário ou e-mail do usuário.</param>
        /// <param name="hashSenha">O hash da senha que será comparado ao hash da senha do usuário.</param>
        /// <returns>Um valor <see cref="bool"/> representando se o hash da senha informado corresponde ao hash da senha do usuário com o nome de usuário ou e-mail informado.</returns>
        bool VerificarNomeUsuarioOuEmailEHashSenha(string nomeUsuarioOuEmail, string hashSenha);

        /// <summary>
        /// Verifica se existem usuários no repositório.
        /// </summary>
        /// <returns>Um valor <see cref="bool"/> que representa se existem usuários no repositório.</returns>
        bool VerificarUsuarioExiste();

        /// <summary>
        /// Verifica se o nome de usuário informado está sendo utilizado por algum usuário existente no repositório.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o nome de usuário informado está sendo utilizado por algum usuário existente no repositório</returns>
        bool VerificarNomeUsuarioUsado(string nomeUsuario);

        /// <summary>
        /// Verifica se o nome de usuário informado está sendo utilizado por algum usuário existente no repositório que não correspondá ao Id informado.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário.</param>
        /// <param name="idIgnorado">O Id do usuário ignorado.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o nome de usuário informado está sendo utilizado por algum usuário existente no repositório  que não correspondá ao Id informado.</returns>
        bool VerificarNomeUsuarioUsado(string nomeUsuario, long idIgnorado);

        /// <summary>
        /// Obtém o hash da chave mestre do usuário que correspondá ao nome de usuário informado.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário.</param>
        /// <returns>O hash da chave mestre do usuário que correspondá ao nome de usuário informado.</returns>
        string? ObterHashChaveMestre(string nomeUsuario);

        /// <summary>
        /// Verifica se o hash da chave mestre informado corresponde ao hash da chave mestre do usuário existe no repositório que corresponda ao nome de usuário informado.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário.</param>
        /// <param name="hashChaveMestre">O hash da chave mestre.</param>
        /// <returns>Um valor <see cref="bool"/> se o hash da chave mestre informado corresponde ao hash da chave mestre do usuário existe no repositório que corresponda ao nome de usuário informado.</returns>
        bool VerificarHashChaveMestre(string nomeUsuario, string hashChaveMestre);

        /// <summary>
        /// Verifica se o e-mail informado está sendo utilizado por algum usuário existente no repositório.
        /// </summary>
        /// <param name="email">O e-mail do usuário.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o e-mail informado está sendo utilizado por algum usuário existente no repositório.</returns>
        bool VerificarEmailUsado(string email);

        /// <summary>
        /// Verifica se o e-mail informado está sendo utilizado por algum usuário existente no repositório que não correspondá ao Id informado.
        /// </summary>
        /// <param name="email">O e-mail do usuário.</param>
        /// <param name="idIgnorado">O Id do usuário ignorado.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o e-mail informado está sendo utilizado por algum usuário existente no repositório  que não correspondá ao Id informado.</returns>
        bool VerificarEmailUsado(string email, long idIgnorado);
    }
}
