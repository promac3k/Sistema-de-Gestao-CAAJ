# Sistema de Gestão CAAJ

## 📋 Sobre o Projeto

Este projeto foi desenvolvido durante o estágio do 11º ano do ensino secundário na empresa **CAAJ** (Comissão para o Acompanhamento dos Auxiliares da Justiça), como parte da formação em **Técnico de Gestão e Programação de Sistemas Informáticos**.

O sistema foi criado para auxiliar na gestão de processos de liquidação de agentes de execução, proporcionando uma interface intuitiva para consulta e gestão de dados.

## 🛠️ Tecnologias Utilizadas

-   **Linguagem**: C# (.NET Framework 4.7.1)
-   **Interface**: Windows Forms
-   **Base de Dados**: MySQL/MariaDB
-   **IDE**: Visual Studio
-   **Conectividade**: MySqlConnector

## ⚡ Funcionalidades

-   **Sistema de Login**: Autenticação de utilizadores com diferentes níveis de acesso
-   **Gestão de Processos**: Consulta e gestão de processos de liquidação
-   **Interface Intuitiva**: Design user-friendly para facilitar a utilização
-   **Segurança**: Controlo de acesso baseado em perfis de utilizador

## 🚀 Instalação e Configuração

### Pré-requisitos

-   .NET Framework 4.7.1 ou superior
-   Acesso a uma base de dados MySQL/MariaDB

### Configuração

1. Clone o repositório:

    ```bash
    git clone https://github.com/promac2k/projectCurso.git
    ```

2. Configure a base de dados:

    - Execute o script `Basedados.sql` na sua base de dados MySQL
    - Copie `App.config.example` para `App.config`
    - Atualize as credenciais de conexão no ficheiro `App.config`

3. Atualize as credenciais no ficheiro `Utilitarios/DB.cs`:

    ```csharp
    this.ServidorIP = "seu_servidor";
    this.Usuario = "seu_utilizador";
    this.senha = "sua_password";
    this.tabela = "sua_base_dados";
    ```

4. Compile e execute o projeto no Visual Studio

## 📁 Estrutura do Projeto

```
├── Forms/                  # Formulários da aplicação
│   ├── Login.cs           # Formulário de login
│   └── Principal.cs       # Formulário principal
├── Utilitarios/           # Classes utilitárias
│   ├── DB.cs             # Gestão de conexões à base de dados
│   ├── Processo.cs       # Lógica de negócio para processos
│   └── Usuario.cs        # Gestão de utilizadores
├── Resources/            # Recursos da aplicação (imagens, etc.)
├── Properties/           # Propriedades do projeto
├── Basedados.sql        # Script de criação da base de dados
└── Program.cs           # Ponto de entrada da aplicação
```

## 🎯 Objetivo Educacional

Este projeto foi desenvolvido como parte da componente prática do curso técnico, demonstrando:

-   Aplicação de conceitos de programação orientada a objetos
-   Integração com bases de dados
-   Desenvolvimento de interfaces gráficas
-   Boas práticas de desenvolvimento de software
-   Trabalho em ambiente empresarial real

## ⚠️ Notas de Segurança

-   **Nunca** committes credenciais reais da base de dados
-   Use variáveis de ambiente ou ficheiros de configuração para dados sensíveis
-   Este projeto é apenas para fins educacionais e demonstrativos

---

## 📄 Licença

Este projeto foi desenvolvido para fins educacionais durante estágio curricular.

---

_Projeto desenvolvido em 2022 durante estágio curricular do curso Técnico de Gestão e Programação de Sistemas Informáticos_

⭐ Se este projeto te foi útil, considera dar uma estrela! ⭐
