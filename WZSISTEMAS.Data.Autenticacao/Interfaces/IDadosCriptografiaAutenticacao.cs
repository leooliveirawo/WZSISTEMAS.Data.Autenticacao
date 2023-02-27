namespace WZSISTEMAS.Data.Autenticacao.Interfaces
{
    /// <summary>
    /// Representa os dados utilizados pelo provedor de criptografia para cripografar ou descriptografar
    /// </summary>
    public interface IDadosCriptografiaAutenticacao
    {
        /// <summary>
        /// Obtém a chave para criptografar ou descriptografar.
        /// </summary>
        string Chave { get; }

        /// <summary>
        /// Obtém o vetor de inicialização (IV) para criptografar ou descriptografar.
        /// </summary>
        string IV { get; }
    }
}
