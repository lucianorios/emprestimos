## Sobre este projeto

Um app para gerenciamento de empréstimos de jogos para pessoas.

Email-me: lucianorioss@gmail.com

# Inicio

## Instalação

**Clonando o repositório**

```
$ git clone https://github.com/lucianorios/emprestimos.git

$ cd emprestimos
```

**Instalando os projetos**

```
$ cd Loan.WebApi && dotnet build

$ dotnet run

$ cd LoanFront && npm install

$ npm start
```


## Docker
    Você precisará do Docker Engine e Docker Compose instalados

```
$ git clone https://github.com/lucianorios/emprestimos.git

$ cd emprestimos

$ docker-compose up -d
```
	
## Dependências

### Backend
- [NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) - Install .NET Core 3.1 
- [MediatR](https://github.com/jbogard/MediatR) - MediatR
- [AutoMapper](https://automapper.org/) - AutoMapper
- [EntityFrameworkCore](https://docs.microsoft.com/pt-br/ef/core/) - EntityFramework Core 3.1
- [JWT Token](https://jwt.io/) - JWT

### Frontend
- [Angular](https://angular.io//) - Create Angular app

## Informações de Login
 - Login e senha para acessar o app.
 
```
email: johndoe@loan.com
password: Senha@123456
```
