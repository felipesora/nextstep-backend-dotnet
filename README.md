# 🚀 NextStep - .NET

## 📌 Sumário

- [📝 Descrição da Solução](#-descrição-da-solução)
- [🎥 Vídeo Pitch da Solução](#-vídeo-pitch-da-solução)
- [🧩 Estrutura da Solução](#-estrutura-da-solução)
- [▶️ Como Rodar o Projeto](#️-como-rodar-o-projeto)
- [🧩 Detalhes da API REST .NET — ASP.NET Core](#-detalhes-da-api-rest-net--aspnet-core)
- [🚀 Como Rodar o Projeto API REST (.NET)](#-como-rodar-o-projeto-api-rest-net)
- [☁️ Deploy da API REST .NET](#️-deploy-da-api-rest-net)
- [🎥 Vídeo da API .NET em Funcionamento](#-vídeo-da-api-net-em-funcionamento)
- [👥 Integrantes](#-integrantes)

## 📝 Descrição da Solução

O NextStep é uma plataforma inteligente desenvolvida para preparar pessoas para as profissões do futuro, oferecendo trilhas de aprendizado modernas, estruturadas e personalizadas.

Em um mundo onde a tecnologia evolui em ritmo acelerado e as demandas do mercado mudam constantemente, o NextStep surge como uma solução completa para quem deseja se atualizar, se qualificar e avançar na carreira com segurança.

A plataforma possui **duas frentes principais**:
- 🌐 **Painel Web Administrativo** — onde gestores criam e organizam trilhas de estudo;

- 📱 **Aplicativo Mobile** — onde os usuários consomem conteúdos, acompanham seu progresso e recebem recomendações personalizadas.

As trilhas são criadas em áreas essenciais como **Backend, Frontend, Cloud, Dados e Inteligência Artificial**, podendo incluir cursos, artigos, vídeos, podcasts, desafios práticos e outros recursos externos.
Para agilizar o processo, o admin conta com uma **IA integrada**, capaz de gerar automaticamente descrições completas de trilhas a partir apenas do título informado.

No app, o usuário tem uma jornada clara, simples e guiada. Com ajuda da **IA recomendadora**, o NextStep analisa o perfil, interesses e objetivos do usuário por meio de um pequeno questionário e indica automaticamente a trilha mais adequada, tornando o processo de aprendizado muito mais assertivo.

---

## 🎥 Vídeo Pitch da Solução

Para entender a visão geral do **NextStep**, sua proposta, funcionalidades principais e o problema que a solução resolve, assista ao vídeo pitch preparado especialmente para apresentar o projeto de forma clara e objetiva.

**👉 Assista ao Vídeo Pitch aqui:**

[Clique para ver o vídeo pitch do NextStep](https://www.youtube.com/watch?v=hw-RtEkYCA4)

Este vídeo resume:

- O propósito da plataforma
- Como o NextStep ajuda na preparação para profissões do futuro
- Os diferenciais da solução
- Demonstrações visuais das principais telas
- A importância de cada módulo desenvolvido

> É a melhor forma de ter uma visão rápida, completa e direta sobre todo o ecossistema NextStep.

---

## 🧩 Estrutura da Solução

O **NextStep** foi desenvolvido com uma arquitetura moderna, modular e escalável, dividida em múltiplos serviços que se integram para entregar uma experiência fluida tanto para administradores quanto para usuários finais.

### ☕ Backend Administrativo — Java + Spring Boot

Responsável por toda a **lógica administrativa** da plataforma.

- CRUD de **trilhas** e **conteúdos** (cursos, artigos, desafios, etc.).
- Geração automática de descrições utilizando **IA integrada**.
- Exposição de APIs REST para o **painel web**.
- Integração direta com o **banco Oracle**.

[🔗 Repositório de Backend Java](https://github.com/felipesora/nextstep-backend-java)

### 🌐 Painel Web Administrativo — React.js

- Interface utilizada pelos **gestores** para criar e **gerenciar trilhas**.

- Desenvolvido em **React.js**.

- Consome exclusivamente a **API Java com Spring Boot**.

- Interface **moderna e responsiva**, focada em **produtividade**.

[🔗 Repositório do Frontend WEB](https://github.com/felipesora/nextstep-frontend-web)

### ⚙️ API do Usuário Final — .NET + ASP.NET Core

Camada que **atende o aplicativo mobile**.

- **Mapeia e expõe as tabelas de trilhas e conteúdos** criadas pelo backend Java.

- Responsável por **cadastro/login**, **progresso do usuário e consumo das trilhas**.

- Estruturada com **ASP.NET Core MVC + Entity Framework**.

- Focada em **alta performance e segurança**.

[🔗 Repositório de Backend .NET](https://github.com/felipesora/nextstep-backend-dotnet)

### 📱 Aplicativo Mobile — React Native + Expo

Aplicação voltada aos **usuários que irão consumir as trilhas**.

- Desenvolvido com **React Native + Expo**.

- Interface clara, intuitiva e otimizada para estudo.

- Consome a **API .NET**.

- Possui **IA recomendadora que sugere a trilha ideal com base no perfil do usuário**.

[🔗 Repositório do Mobile](https://github.com/felipesora/nextstep-frontend-mobile)

### 🗄️ Banco de Dados — Oracle

Armazena **todas as informações da plataforma**:

- Tabelas de **trilhas, conteúdos, usuários, progresso, notas e estatísticas**.

- Estrutura centralizada garantindo **consistência entre Java e .NET**.

[🔗 Repositório do Banco de Dados]()

### ☁️ Deploy & Infraestrutura — Azure

A API Java (admin) é publicada utilizando **práticas modernas de DevOps**:

- **Pipelines de CI/CD** no Azure DevOps.

- **Build automatizado**, execução de testes (quando houver) e **deploy contínuo**.

- Infraestrutura **escalável e segura**.

[🔗 Repositório de Cloud](https://github.com/felipesora/nextstep-cloud)

---

## 🗄️ Modelagem do Banco de Dados

Abaixo está a modelagem das tabelas utilizadas pelo sistema:

![Modelagem do banco](docs/modelagem-nextstep.png)

---

## ▶️ Como Rodar o Projeto

Para executar o NextStep localmente, siga a ordem correta dos serviços, garantindo que cada camada esteja funcionando antes de iniciar a próxima.

Abaixo está o fluxo recomendado:

### 1️⃣ Rodar a API Administrativa — Java + Spring Boot

1. Certifique-se de ter o **Java 21+** instalado.

2. Configure a conexão com o banco Oracle no application.properties.

3. Inicie o projeto Spring Boot.

4. Aguarde a criação/mapeamento inicial das tabelas necessárias.

> 💡 **Importante:** É essa API que fornece todos os dados administrativos e cria as trilhas e conteúdos utilizados por todo o ecossistema.

### 2️⃣ Rodar o Painel Web Administrativo — React.js

1. Instale dependências com `npm install`.

2. Configure as variáveis de API em cada service, com a url da api `Java`

3. Rode com `npm run dev`.

4. Acesse o painel e **cadastre algumas trilhas e conteúdos** — isso é essencial para que o app mobile e a API .NET tenham dados para consumir.

### 3️⃣ Rodar a API do Usuário Final — .NET + ASP.NET Core

1. Instale o .NET 8+.

2. Configure a connection string do Oracle.

3. Inicie o projeto (`dotnet run`).

4. Essa API irá consumir as tabelas geradas pelo backend Java e disponibilizar os dados para o app mobile.

### 4️⃣ Rodar o Aplicativo Mobile — React Native + Expo

1. Instale dependências com `npm install`.

2. Configure as variáveis de API em cada service, com a url da api de `.NET`.

3. Rode com `npx expo start`.

4. Abra no celular ou emulador para testar a jornada do usuário final.

### 📌 Observação Importante

Cada parte do NextStep possui **seu próprio repositório e um README separado**, com **todas as instruções detalhadas** de instalação, configuração e execução.

Esta seção é apenas um **guia geral**, mostrando a ordem correta de execução dos componentes.

## 🧩 Detalhes da API REST .NET — ASP.NET Core

Além da API Java, o NextStep também conta com uma **API REST desenvolvida em ASP.NET Core**, responsável por funcionalidades específicas como gestão de usuários, permissões, setores, responsáveis e integrações administrativas.

Ela funciona como um segundo backend do ecossistema, garantindo arquitetura modular e maior flexibilidade para expansão futura da plataforma.

### 🔧 Tecnologias e Dependências Utilizadas

A API .NET é construída sobre tecnologias modernas do ecossistema Microsoft:

- .NET 8
- ASP.NET Core API WEB
- Entity Framework Core
- Oracle Entity
- Swashbuckle / Swagger
- Rate Limiting (AspNetCore Rate Limiting)
- CORS

### ❤️‍🩹 Health Checks — Garantindo Estabilidade e Observabilidade

Ambas as APIs do NextStep contam com **Health Checks**, permitindo monitoramento do estado da aplicação e validação de que todos os serviços essenciais estão funcionando corretamente.

**🔍 O que é verificado?**

- Conexão com o Banco Oracle
- Validação da API em si (self-check)
- Endpoints padrões `/api/Health/live` e `/api/Health/ready`

### 📘 Documentação da API — Swagger UI

A API REST em .NET possui Swagger completamente configurado, permitindo:

- Visualização de todos os endpoints
- Execução de requisições diretamente pela interface
- Geração automática de documentação OpenAPI
- Organização por grupos e controllers

Isso facilita testes, homologação e integração com o frontend e outros serviços, reduzindo erros e acelerando o desenvolvimento.

A documentação fica disponível no endpoint padrão:
```
/swagger
```

### 🌐 Exemplos de Endpoints

#### 👤 Usuário Administrador

- `POST - /api/Usuario`  
  Cadastra um novo usuário final.

```jsonc
{
  "nome": "Felipe",
  "email": "felipe@email.com",
  "senha": "felipe123",
}
```

- `GET - /api/Usuario`  
  Lista todos os usuários finais cadastrados.

- `GET BY ID - /api/Usuario/{id}`  
  Lista o usuário final cadastrado com este id.

- `PUT - /api/Usuario/{id}`  
  Atualiza os dados do usuário final com este id.

```jsonc
{
  "nome": "Felipe Sora", // alterando nome
  "email": "felipe@email.com",
  "senha": "felipe123",
}
```

- `DELETE - /api/Usuario/{id}`  
  Remove o usuário final com este id.

#### 📚 Trilhas de estudo

- `GET - /api/Trilhas`  
  Lista todas as trilhas de estudo cadastrados.

- `GET ATIVAS - /api/Trilhas/ativas`  
  Lista todas as trilhas de estudo cadastrados.

- `GET BY ID - /api/Trilhas/{id}`  
  Lista a trilha de estudo cadastrada com este id.

#### 🗃️ Conteúdo da trilha

- `GET - /api/Conteudo`  
  Lista todos os conteúdos cadastrados.

- `GET BY ID - /api/Conteudo/{id}`  
  Lista o conteúdo cadastrado com este id.

- `GET BY TRILHA - /api/Conteudo/trilha/{idTrilha}`  
  Lista o conteúdo cadastrado de uma trilha.

#### ⭐ Nota da trilha

- `POST - /api/NotaTrilha`  
  Cadastra um novo usuário final.

```jsonc
{
  "valorNota": 5,
  "observacao": "Trilha excelente! Explicações claras e exemplos práticos. Recomendo muito!",
  "idTrilha": 1,
  "idUsuario": 1
}
```

- `GET - /api/NotaTrilha`  
  Lista todas notas cadastradas.

- `GET BY ID - /api/NotaTrilha/{id}`  
  Lista a nota com este id.

- `GET BY TRILHA - /api/NotaTrilha/trilha/{idTrilha}`  
  Lista as notas cadastradas de uma trilha.

- `PUT - /api/NotaTrilha/{id}`  
  Atualiza os dados da nota com este id.

```jsonc
{
  "valorNota": 4,
  "observacao": "Trilha boa, mas tem alguns erros.",
  "idTrilha": 1,
  "idUsuario": 1
}
```

#### 📝 Respostas do Formulário

- `POST - /api/Formulario`  
  Cadastra uma nova resposta do formulário.

```jsonc
{
  "nivelExperiencia": "INICIANTE", // NENHUMA, INICIANTE, INTERMEDIARIO, AVANCADO
  "objetivoCarreira": "APRENDER", // PRIMEIRO_EMPREGO, MUDAR_CARREIRA, CRESCER_AREA, APRENDER
  "areaTecnologia1": "FRONTEND", // FRONTEND, BACKEND, MOBILE, CLOUD, DADOS, CIBER, DESIGN
  "areaTecnologia2": "DESIGN", // FRONTEND, BACKEND, MOBILE, CLOUD, DADOS, CIBER, DESIGN
  "areaTecnologia3": "MOBILE", // FRONTEND, BACKEND, MOBILE, CLOUD, DADOS, CIBER, DESIGN
  "horasEstudo": "ATE_5H", // ATE_5H, DE_6_A_10H, DE_11_A_15H, MAIS_DE_15H
  "habilidades": "Conheço um pouco de HTML e CSS, mas ainda estou aprendendo o básico.",
  "idUsuario": 1
}
```

- `GET - /api/Formulario`  
  Lista todas as respostas do formulário cadastradas.

- `GET BY ID - /api/Formulario/{id}`  
  Lista a resposta do formulário com este id.

- `GET BY USUARIO - /api/Formulario/usuario/{idUsuario}`  
  Lista as respostas do formulário cadastradas de um usuário.

---

## 🚀 Como Rodar o Projeto API REST (.NET)

Para executar a **API REST .NET do NextStep**, siga os passos abaixo:

### 1️⃣ Abrir o Projeto Correto
A API REST está localizada no projeto `NS.Presentation`, que é o ponto de entrada da aplicação.

- Abra a solução no **Visual Studio ou Rider**.
- Certifique-se de selecionar **NS.Presentation** como projeto de inicialização (Startup Project).

### 2️⃣ Ajustar o launchSettings.json (URL da Aplicação)
Para manter compatibilidade com o aplicativo mobile, é necessário configurar o projeto para rodar nos seguintes endereços:

**📄 Substitua o conteúdo do `launchSettings.json` por:**
```json
{
  "profiles": {
    "NS.Presentation": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "https://localhost:56501;http://localhost:56500"
    }
  }
}
```
Essas URLs são obrigatórias caso o mobile vá consumir a API local.

### 3️⃣ Rodar as Migrations (Banco de Dados)

Antes de executar a API, garanta que o banco Oracle esteja preparado:

**✔️ No Visual Studio:**
1. Abra Tools > NuGet Package Manager > Package Manager Console
2. Certifique-se que o **Default Project esteja apontando** para: `NS.Infra.Data` (onde ficam as migrations)
3. Execute o comando:
```bash
Update-Database
```
Isso irá aplicar todas as migrations automaticamente no seu banco Oracle.

Execute o comando:

### 4️⃣ Executar o Projeto

- No Visual Studio, clique em **Run ▶** com o projeto **NS.Presentation** selecionado.
- Ou rode pelo terminal dentro da pasta `NS.Presentation`:
```
dotnet run
```

A aplicação será iniciada nos endereços:
- [http://localhost:56500](http://localhost:56500)
- [https://localhost:56501](https://localhost:56501)

### 5️⃣ Acessar o Swagger

Após iniciar:

**👉 Acesse a documentação da API:**

[http://localhost:56500/swagger](http://localhost:56500/swagger)

> ⚠️ Dica: Dica: Sempre confirme que o ambiente está como Development no `launchSettings.json`, especialmente se estiver rodando integrações como Swagger, Health Checks ou Oracle.

---

## ☁️ Deploy da API REST .NET

Além da API Java, o NextStep também possui deploy da **API REST .NET (ASP.NET Core)**, responsável pela parte do usuário final, permissões, setores, responsáveis e outras funcionalidades administrativas complementares.

### 🔹 API REST — ASP.NET Core (.NET)

A API .NET está hospedada e disponível publicamente na URL abaixo:

**👉 API .NET (Deploy)**:
[https://nextstep-backend-dotnet.onrender.com](https://nextstep-backend-dotnet.onrender.com)

>⚠️ **Atenção**: Assim como a API Java, esta API também está hospedada no Render, o que significa que pode levar algum tempo para reativar após período de inatividade. Caso receba erro ao acessar algum endpoint ou ao rodar o mobile, basta aguardar alguns segundos até o servidor “acordar”.

---

## 🎥 Vídeo da API .NET em Funcionamento

Para demonstrar o funcionamento da **API REST desenvolvida em ASP.NET Core**, disponibilizei um vídeo completo mostrando:

- Estrutura dos endpoints
- Funcionamento do Swagger
- Testes de criação, listagem e atualização
- Integração com Oracle
- Health Checks em ação
- Fluxo completo da API utilizada pelo aplicativo mobile

👉 **Assista ao vídeo aqui**:
[Clique para ver o vídeo da API .NET](https://www.youtube.com/watch?v=OUtceBt9_KE)

---

## 👥 Integrantes

- **Felipe Ulson Sora** – RM555462 – [@felipesora](https://github.com/felipesora)
- **Augusto Lopes Lyra** – RM558209 – [@lopeslyra10](https://github.com/lopeslyra10)
- **Vinicius Ribeiro Nery Costa** – RM559165 – [@ViniciusRibeiroNery](https://github.com/ViniciusRibeiroNery)