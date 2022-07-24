## Ferramentas, Banco de dados

---

Para o uso e teste da aplicação favor utilizar o comando **git clone https://github.com/lekuranaga/HiringSonda.git**

O ambiente utilizado para desenvolver a aplicação foi o **Windows 11.**

Para o desenvolvimento foi utilizado o **SQL Server Express 2019** como banco de dados e o projeto foi desenvolvido utilizando o **Visual Studio 2022** a base criada é a **SondaLeandroKuranaga** e é configurável através do arquivo **appsettings.json**.

## Projeto

---

O projeto foi dividido em camadas, sendo utilizado a **onion architecture**, além de utilizar **Entity Framework Core (Design, Relational, SqlServer, Tools)** juntamente com a migration **add-migration SondaHiring**.

A Estrutura do banco de dados na imagem abaixo.

A connection string para acessar o banco **(localdb)\MSSQLLocalDB** com autenticação windows, serve para o sql server 2017 e 2019.

![Diagrama UML](Stefanini/Stefanini/Assets/Banco.png)

![Collections postman](Stefanini/Stefanini/Assets/Stefanini - Localhost.postman_collection.json)

A camada de aplicação **(Stefanini.API)** é onde estão os controllers.

A camada de **domínio (Stefanini.Domain)** é onde fica a regra de negócio, possui as interfaces e os models.

A camada de **infra (Stefanini.Infra)** é onde fica a parte de conexão com banco de dados, mapeamentos, migrations, repositórios, dbcontext.

### Dependências

---

- A camada de **infra (Stefanini.Infra)** possui a dependência do projeto **domínio (Stefanini.Domain)**.

- A camada de Aplicação **(Stefanini.API)** possui a dependência dos projetos **domínio (Stefanini.Domain)** e **infra (Stefanini.Infra)**.
