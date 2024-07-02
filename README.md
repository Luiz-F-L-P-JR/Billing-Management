# Billing Management API

Billing Management API é uma REST API para gerenciamento de serviços de faturamento, que cria e manipula esses faturamentos.

## Funcionalidades

- *Customer (Cliente):* CRUD para gerenciar clientes.
- *Produtos:* CRUD para gerenciar produtos.
- *Billing:* CRUD para gerenciar billings.
- *Billing (Faturamento):*
  - Inserção de registro de billing e billingLines no banco de dados local se cliente e produto existirem.
  - Erro retornado se cliente ou produto estiverem ausentes durante a criação do registro.
- *Integração com API Externa:* Possibilidade de integração com outras APIs externas.
- *Extração de Dados:* Geração de arquivo .xlsx a partir dos dados do sistema.
- *Autenticação JWT:* Utilização de tokens JWT para autenticar e autorizar usuários.

## Pré-requisitos

Antes de começar, certifique-se de ter os seguintes requisitos instalados em sua máquina:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Um editor de código de sua preferência (ex: Visual Studio, Visual Studio Code)
- SQLite3 (já incluído no .NET Core)

## Configuração

1. *Clone o repositório:*

   ```bash
   git clone https://github.com/Luiz-F-L-P-JR/Billing-Management.git
   ```
   

2. *Abra o projeto:*

   Navegue até o diretório clonado e abra o projeto no seu editor de código.

3. *Configuração do Banco de Dados:*

   Execute as migrações para criar o banco de dados local SQLite:

   ```bash
   dotnet ef database update --project Billing-Management.Infra.Data
   ```
   

   Isso aplicará as migrações necessárias para criar o banco de dados SQLite na pasta Billing-Management.Infrastructure.

4. *Configuração do JWT:*

   Configure as chaves JWT no arquivo de configuração appsettings.json:

  ```bash
   json
   {
     "JwtSettings": {
       "SecretKey": "sua_chave_secreta_aqui"
     }
   }
   ```

5. *Executando a aplicação:*

   Para iniciar a API, utilize o comando:

   ```bash
   dotnet run --project Billing-Management.Api
   ```   

   A API estará disponível nas portas de acesso configuradas no arquivo lauchsettings.json.

## Utilização da API

Explore os endpoints da API utilizando uma ferramenta como [Postman](https://www.postman.com/) ou [Insomnia](https://insomnia.rest/).

## Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para enviar pull requests e reportar issues.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
