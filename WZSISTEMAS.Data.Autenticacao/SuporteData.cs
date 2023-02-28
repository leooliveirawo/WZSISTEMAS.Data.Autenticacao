global using static WZSISTEMAS.Data.Autenticacao.SuporteData;


namespace WZSISTEMAS.Data.Autenticacao
{
    /// <summary>
    /// Representa os métodos de suporte a dados.
    /// </summary>
    public static class SuporteData
    {
        /// <summary>
        /// Verifica se o objeto é nulo, e se for nulo dispara uma exceção do tipo <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="instancia">A instância do objeto.</param>
        /// <param name="parametroNome">O nome do paramentro.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void VerificarNulo(this object instancia, string parametroNome)
        {
            if (instancia is null)
                throw new ArgumentNullException(nameof(parametroNome));
        }

        /// <summary>
        /// Verifica se o texto é vázio ou nulo.
        /// </summary>
        /// <param name="texto">O texto que será verificado.</param>
        /// <param name="mensagemErro">A mensagem de erro.</param>
        /// <param name="parametroNome">O nome do parâmetro.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void VerificarVazioOuNulo(this string texto, string mensagemErro, string parametroNome)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException(mensagemErro, parametroNome);
        }

        /// <summary>
        /// Verifica se o número é zero ou negativo.
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="mensagemErro">A mensagem de erro.</param>
        /// <param name="parametroNome">O nome do parâmetro.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void VerificarZeroOuNegativo(this long numero, string mensagemErro, string parametroNome)
        {
            if (numero <= 0)
                throw new ArgumentException(mensagemErro, parametroNome);
        }
    }
}
