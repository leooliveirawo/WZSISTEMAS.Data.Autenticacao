namespace WZSISTEMAS.Data.Autenticacao.Interfaces
{
    /// <summary>
    /// Representa os serviços de usuários e de autenticação.
    /// </summary>
    /// <typeparam name="TUsuario">O tipo da classe de usuário.</typeparam>
    public interface IServicoUsuarios<TUsuario> where TUsuario : Usuario
    {
        /// <summary>
        /// Altera o cadastro de um usuário existente.
        /// </summary>
        /// <param name="usuario">Os dados do usuário que será alterado.</param>
        void Alterar(TUsuario usuario);

        /// <summary>
        /// Altera a senha do cadastro de usuário informado.
        /// </summary>
        /// <param name="usuario">O usuário que a senha será alterado.</param>
        /// <param name="senha">A nova senha do usuário.</param>
        void AlterarSenha(TUsuario usuario, string senha);

        /// <summary>
        /// Cria um novo cadastro de usuário.
        /// </summary>
        /// <param name="usuario">Os dados do usuário que será criado.</param>
        /// <param name="senha">A nova senha do usuário.</param>
        void Criar(TUsuario usuario, string senha);
        
        /// <summary>
        /// Exclui um cadastro de usuário que correspondá ao Id especificado.
        /// </summary>
        /// <param name="id">O Id do cadastro do usuário que será excluído.</param>
        void Excluir(long id);

        /// <summary>
        /// Realiza uma nova autenticação com base em uma autenticação existente.
        /// </summary>
        /// <param name="token">O token da autenticação atual.</param>
        /// <returns>O token da nova autenticação.</returns>
        string NovaAutenticacao(string token);

        /// <summary>
        /// Realiza uma autenticação se o nome de usuário e a senha estiverem corretos.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário ao ser autenticação.</param>
        /// <param name="senha">A senha do usuário ao ser autenticado.</param>
        /// <returns>O token da autenticação ou nulo se a autenticação não for bem sucessidade.</returns>
        string Autenticar(string nomeUsuario, string senha);

        /// <summary>
        /// Obtém um cadastro do usuário que correspondá ao Id especificado, ou nulo se não existir.
        /// </summary>
        /// <param name="id">O Id do cadastro de usuário que será retornado.</param>
        /// <returns>O cadastro do usuário que correspondá ao Id especificado, ou nulo se não existir.</returns>
        TUsuario? ObterPorId(long id);

        /// <summary>
        /// Obtém todos os cadastros dos usuários existem em uma lista simplicada contendo o Id, nome de usuário e e-mail.
        /// </summary>
        /// <returns>Todos os cadastros dos usuários existem em uma lista simplicada contendo o Id, nome de usuário e e-mail.</returns>
        IEnumerable<TUsuario> ObterTudo();

        /// <summary>
        /// Verifica se existem cadastros de usuários.
        /// </summary>
        /// <returns>Um valor <see cref="bool"/> representando se existem cadastros de usuários.</returns>
        bool VerificarUsuarioExiste();

        /// <summary>
        /// Verifica se o token informado está autenticado.
        /// </summary>
        /// <param name="token">O token que será verificado se está autenticado.</param>
        /// <returns>Um valor <see cref="bool"/> representando se o token informado está autenticado.</returns>
        bool VerificarAutenticacao(string token);
    }
}
