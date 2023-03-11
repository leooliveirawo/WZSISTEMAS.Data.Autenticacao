namespace WZSISTEMAS.Data.Autenticacao.Testes
{
    public sealed class RepositorioUsuariosSimulador : IRepositorioUsuarios<Usuario>
    {
        public ICollection<Usuario> Usuarios { get; }
        private long identityId;

        public RepositorioUsuariosSimulador()
        {
            Usuarios = new List<Usuario>();
            identityId = 0;
        }

        private void VerificarRequisitosFontesDados(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.NomeUsuario))
                throw new InvalidOperationException("O nome de usuário não foi informado");

            if (string.IsNullOrWhiteSpace(usuario.HashSenha))
                throw new InvalidOperationException("O hash da senha não foi informado");

            if (string.IsNullOrWhiteSpace(usuario.HashChaveMestre))
                throw new InvalidOperationException("O hash da chave mestre não foi informado");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new InvalidOperationException("O endereço de e-mail não foi informado");
        }

        public void Alterar(Usuario usuario)
        {
            var usuarioExistente = Usuarios.SingleOrDefault(x => x.Id == usuario.Id);

            if (usuarioExistente is null)
                throw new InvalidOperationException("O usuário não existe");

            usuarioExistente.NomeUsuario = usuario.NomeUsuario;
            usuarioExistente.HashSenha = usuario.HashSenha;
            usuarioExistente.HashChaveMestre = usuario.HashChaveMestre;
            usuarioExistente.Email = usuario.Email;
        }

        public void Criar(Usuario usuario)
        {
            identityId++;

            usuario.Id = identityId;

            Usuarios.Add(usuario);
        }

        public void Excluir(long id)
        {
            var usuarioExistente = Usuarios.SingleOrDefault(x => x.Id == id);

            if (usuarioExistente is null)
                throw new InvalidOperationException("O usuário não existe");

            Usuarios.Remove(usuarioExistente);
        }

        public string? ObterHashChaveMestre(string nomeUsuario)
        {
            var usuarioExistente = Usuarios.SingleOrDefault(x => x.NomeUsuario == nomeUsuario);

            if (usuarioExistente is null)
                return default;

            return usuarioExistente.HashChaveMestre;
        }

        public string? ObterNomeUsuarioPorId(long id)
        {
            var usuarioExistente = Usuarios.SingleOrDefault(x => x.Id == id);

            if (usuarioExistente is null)
                return default;

            return usuarioExistente.NomeUsuario;
        }

        public Usuario? ObterPorId(long id)
        {
            var usuario = Usuarios.SingleOrDefault(x => x.Id == id);

            if (usuario is null)
                return null;

            return new Usuario
            {
                Id = id,
                NomeUsuario = usuario.NomeUsuario,
                HashSenha = usuario.HashSenha,
                HashChaveMestre = usuario.HashChaveMestre,
                Email = usuario.Email
            };
        }

        public IEnumerable<Usuario> ObterTudo()
        {
            var usuarios = new List<Usuario>();

            foreach (var usuario in Usuarios)
                usuarios.Add(new Usuario
                {
                    Id = usuario.Id,
                    NomeUsuario = usuario.NomeUsuario,
                    HashSenha = usuario.HashSenha,
                    HashChaveMestre = usuario.HashChaveMestre,
                    Email = usuario.Email
                });;

            return usuarios;
        }

        public bool VerificarHashChaveMestre(string nomeUsuario, string hashChaveMestre)
        {
            return Usuarios.Any(x => x.NomeUsuario == nomeUsuario && x.HashChaveMestre == hashChaveMestre);
        }

        public bool VerificarNomeUsuarioEHashSenha(string nomeUsuario, string hashSenha)
        {
            return Usuarios.Any(x => x.NomeUsuario == nomeUsuario && x.HashSenha == hashSenha);
        }

        public bool VerificarNomeUsuarioUsado(string nomeUsuario)
        {
            return Usuarios.Any(x => x.NomeUsuario == nomeUsuario);
        }

        public bool VerificarUsuarioExiste()
        {
            return Usuarios.Any();
        }

        public Usuario? ObterPorNomeUsuario(string nomeUsuario)
        {
            return Usuarios.FirstOrDefault(x => x.NomeUsuario == nomeUsuario);
        }

        public bool VerificarEmailUsado(string email)
        {
            return Usuarios.Any(x => x.Email == email);
        }
    }
}
