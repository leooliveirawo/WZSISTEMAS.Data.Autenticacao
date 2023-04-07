# WZSISTEMAS.Data.Autenticacao

## Sobre

Dá suporte ao gerenciamento de usuários e a autenticação no sistemas.

## Para utilizar o serviço de Usuários, é necessário fazer a implementação do repositório de Usuários por meio da interface **IRepositorioUsuarios** no namespace **WZSISTEMAS.Data.Autenticacao.Interfaces**.

## Autenticação

A autenticação permite que seja controlado o acesso ao sistema, controlando os níveis de permissões, autorizações e acesso.
### Provedor AES

#### Gerenciamento de usuário
##### Criação de usuários

Para **criar** um usuário no sistema utilize a classe **ServicoUsuarios** disponível no namespace **WZSISTEMAS.Data.Autenticacao** utilizando o método **Criar**.

Abaixo um exemplo de código para criar um usuário:

```
Usuario usuario = new Usuario()
{
  NomeUsuario = "informe o nome de usuário",
  Email = "informe o e-mail"
};

var repositorio = new RepositorioUsuarios();

ServicoUsuarios<Usuario> servicoUsuarios = new ServicoUsuarios<Usuario>(
  repositorio,
  new ProvedorSHA256(),
  new ProvedorAes(),
  DadosCriptografiaAutenticacao 
  {
    Chave = "12345678123456781234567812345678",
    IV = "1234567812345678"
  });
    
servicoUsuarios.Criar(usuario, "senha");
```

Obs: * Essa classe deve ter sido criada, e deve implementar a interface **IRepositorioUsuarios**.

> Durante a execução do método as seguintes exceções podem ser disparadas.
> * **WZSISTEMAS.Data.Exceptions.ChaveFormatoException**
>> A chave informada está em um formato inválido. Ela deve ter 32 caractéres.
> * **WZSISTEMAS.Data.Exceptions.IVFormatoException**
>> O vetor de inicialização (IV) informado está em um formato inválido. Ele deve ter 16 caractéres.

##### Edição de usuários

Para **editar** de um usuário no sistema utilize a classe **ServicoUsuarios** disponível no namespace **WZSISTEMAS.Data.Autenticacao** utilizando o método **Editar**.

Abaixo um exemplo de código para editar um usuário:

```
Usuario usuario = servicoUsuarios.ObterPorId(1);

usuario.NomeUsuario = "novo nome de usuário";
usuario.Email = "nova senha";

var repositorio = new RepositorioUsuarios();

ServicoUsuarios<Usuario> servicoUsuarios = new ServicoUsuarios<Usuario>(
  repositorio,
  new ProvedorSHA256(),
  new ProvedorAes(),
  DadosCriptografiaAutenticacao 
  {
    Chave = "12345678123456781234567812345678",
    IV = "1234567812345678"
  });
    
servicoUsuarios.Alterar(usuario);
```

Obs: * Essa classe deve ter sido criada, e deve implementar a interface **IRepositorioUsuarios**.

##### Edição da senha de usuários

Para **editar a senha** de um usuário no sistema utilize a classe **ServicoUsuarios** disponível no namespace **WZSISTEMAS.Data.Autenticacao** utilizando o método **EditarSenha**.

Abaixo um exemplo de código para editar a senha de um usuário:

```
Usuario usuario = servicoUsuarios.ObterPorId(1);

var repositorio = new RepositorioUsuarios();

ServicoUsuarios<Usuario> servicoUsuarios = new ServicoUsuarios<Usuario>(
  repositorio,
  new ProvedorSHA256(),
  new ProvedorAes(),
  DadosCriptografiaAutenticacao 
  {
    Chave = "12345678123456781234567812345678",
    IV = "1234567812345678"
  });
    
servicoUsuarios.AlterarSenha(usuario, "nova senha");
```

Obs: * Essa classe deve ter sido criada, e deve implementar a interface **IRepositorioUsuarios**.
