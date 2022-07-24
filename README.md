## Ferramentas, Banco de dados
 ___   
Para o uso e teste da aplicação favor utilizar o comando **https://github.com/lekuranaga/Stefanini.git**

O ambiente utilizado para desenvolver a aplicação foi o **Windows 11.**

Para o desenvolvimento foi utilizado o **SQL Server Express 2019** como banco de dados e o projeto foi desenvolvido utilizando o **Visual Studio 2022** a base criada é a **StefaniniLeandroKuranaga** e é configurável através do arquivo **appsettings.json**.

## Projeto
___

O projeto foi dividido em camadas, sendo utilizado a **onion architecture**, além de utilizar **Entity Framework Core (Design, Relational, SqlServer, Tools)**.

A Estrutura do banco de dados na imagem abaixo.

A connection string para acessar o banco **(localdb)\MSSQLLocalDB** com autenticação windows, serve para o sql server 2017 e 2019.

![Diagrama UML](https://user-images.githubusercontent.com/29407031/156945886-01367072-7991-4f13-b54a-f7be7812a393.png)

![Postman](Stefanini/Assets/Stefanini.json)

A camada de aplicação **(Stefanini.API)** é onde estão os controllers.

A camada de **domínio (Stefanini.Domain)** é onde fica a regra de negócio, possui as interfaces e os models.

A camada de **infra (Stefanini.Infra)** é onde fica a parte de conexão com banco de dados, mapeamentos, migrations, repositórios, dbcontext.

### Dependências
___

* A camada de **infra (Stefanini.Infra)** possui a dependência do projeto **domínio (Stefanini.Domain)**.

* A camada de Aplicação **(Stefanini.API)** possui a dependência dos projetos **domínio (Stefanini.Domain)** e **infra (Stefanini.Infra)**.
