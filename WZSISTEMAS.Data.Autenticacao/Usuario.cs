namespace WZSISTEMAS.Data.Autenticacao
{
    /// <summary>
    /// Representa um cadastro de usuário.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Obtém ou define o Id do cadastro do usuário.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Obtém ou define o nome de usuário do cadastro do usuário.
        /// </summary>
        public string NomeUsuario { get; set; } = null!;

        /// <summary>
        /// Obtém ou define o endereço de e-mail do cadastro do usuário.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Obtém ou define o hash da senha do cadastro do usuário.
        /// </summary>
        public string HashSenha { get; set; } = null!;

        /// <summary>
        /// Obtém ou define o hash da chave mestre do cadastro do usuário.
        /// </summary>
        public string HashChaveMestre { get; set; } = null!;
    }
}
