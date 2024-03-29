﻿namespace WZSISTEMAS.Data.Autenticacao.Interfaces
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
        string? NovaAutenticacao(string token);

        /// <summary>
        /// Realiza uma autenticação se o nome de usuário e a senha estiverem corretos.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário ao ser autenticação.</param>
        /// <param name="senha">A senha do usuário ao ser autenticado.</param>
        /// <returns>O token da autenticação ou nulo se a autenticação não for bem sucessidade.</returns> 
        [Obsolete($"Utilize o método {nameof(IServicoUsuarios<TUsuario>.AutenticarPeloNomeUsuario)}", error: true)]
        string? Autenticar(string nomeUsuario, string senha);

        /// <summary>
        /// Realiza uma autenticação se o nome de usuário e a senha estiverem corretos.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário ao ser autenticação.</param>
        /// <param name="senha">A senha do usuário ao ser autenticado.</param>
        /// <returns>O token da autenticação ou nulo se a autenticação não for bem sucessidade.</returns> 
        string? AutenticarPeloNomeUsuario(string nomeUsuario, string senha);

        /// <summary>
        /// Realiza uma autenticação se o e-mail e a senha estiverem corretos.
        /// </summary>
        /// <param name="email">O e-mail do usuário ao ser autenticação.</param>
        /// <param name="senha">A senha do usuário ao ser autenticado.</param>
        /// <returns>O token da autenticação ou nulo se a autenticação não for bem sucessidade.</returns> 
        string? AutenticarPeloEmail(string email, string senha);

        /// <summary>
        /// Realiza uma autenticação se o nome de usuário ou e-mail e a senha estiverem corretos.
        /// </summary>
        /// <param name="nomeUsuarioOuEmail">O nome de usuário ou e-mail do usuário ao ser autenticação.</param>
        /// <param name="senha">A senha do usuário ao ser autenticado.</param>
        /// <returns>O token da autenticação ou nulo se a autenticação não for bem sucessidade.</returns> 
        string? AutenticarPeloNomeUsuarioOuEmail(string nomeUsuarioOuEmail, string senha);

        /// <summary>
        /// Obtém um cadastro do usuário que correspondá ao Id especificado, ou nulo se não existir.
        /// </summary>
        /// <param name="id">O Id do cadastro de usuário que será retornado.</param>
        /// <returns>O cadastro do usuário que correspondá ao Id especificado, ou nulo se não existir.</returns>
        TUsuario? ObterPorId(long id);

        /// <summary>
        /// Obtém um cadastro de usuário que correspondá ao token de autenticação.
        /// </summary>
        /// <param name="token">O token de autenticação do usuário que será obtido.</param>
        /// <returns>O cadastro de usuário que correspondá ao token de autenticaçã.</returns>
        TUsuario? ObterPorToken(string token);

        /// <summary>
        /// Obtém um cadastro de usuário existente que correspondá ao nome de usuário especificado.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário que será obtido.</param>
        /// <returns>O cadastro de usuário existente que correspondá ao nome de usuário especificado.</returns>
        TUsuario? ObterPorNomeUsuario(string nomeUsuario);

        /// <summary>
        /// Obtém um cadastro de usuário existente que correspondá ao e-mail especificado.
        /// </summary>
        /// <param name="email">O e-mail do usuário que será obtido.</param>
        /// <returns>O cadastro de usuário existente que correspondá ao e-mail especificado.</returns>
        TUsuario? ObterPorEmail(string email);

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

        /// <summary>
        /// Verifica se o nome de usuário informado está sendo utilizado por algum usuário existente.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o nome de usuário informado está sendo utilizado por algum usuário existente.</returns>
        bool VerificarNomeUsuarioUsado(string nomeUsuario);

        /// <summary>
        /// Verifica se o nome de usuário informado está sendo utilizado por algum usuário existente que não correspondá ao Id informado.
        /// </summary>
        /// <param name="nomeUsuario">O nome de usuário do usuário.</param>
        /// <param name="idIgnorado">O Id do usuário ignorado.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o nome de usuário informado está sendo utilizado por algum usuário existente que não correspondá ao Id informado.</returns>
        bool VerificarNomeUsuarioUsado(string nomeUsuario, long idIgnorado);

        /// <summary>
        /// Verifica se o e-mail informado está sendo utilizado por algum usuário existente.
        /// </summary>
        /// <param name="email">O e-mail do usuário.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o e-mail informado está sendo utilizado por algum usuário existente.</returns>
        bool VerificarEmailUsado(string email);

        /// <summary>
        /// Verifica se o e-mail informado está sendo utilizado por algum usuário existente que não correspondá ao Id informado.
        /// </summary>
        /// <param name="email">O e-mail do usuário.</param>
        /// <param name="idIgnorado">O Id do usuário ignorado.</param>
        /// <returns>Um valor <see cref="bool"/> que representa se o e-mail informado está sendo utilizado por algum usuário existente que não correspondá ao Id informado.</returns>
        bool VerificarEmailUsado(string email, long idIgnorado);
    }
}
