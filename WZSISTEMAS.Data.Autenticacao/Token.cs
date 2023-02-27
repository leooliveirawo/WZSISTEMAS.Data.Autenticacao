namespace WZSISTEMAS.Data.Autenticacao
{
    /// <summary>
    /// Represente o token de autenticação.
    /// </summary>
    [Serializable]
    public class Token
    {
        /// <summary>
        /// Obtém o nome de usuário do token da autenticação.
        /// </summary>
        public string NomeUsuario { get; init; } = null!;

        /// <summary>
        /// Obtém o hash da chave mestre do token da autenticação.
        /// </summary>
        public string HashChaveMestre { get; init; } = null!;

        /// <summary>
        /// Obtém a data em a autenticação foi feita.
        /// </summary>
        public DateTime LogadoEm { get; init; }

        /// <summary>
        /// Obtém a data em a autenticação expirou.
        /// </summary>
        public DateTime ExpiraEm { get; init; }
    }
}
