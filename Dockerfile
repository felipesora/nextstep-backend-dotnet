# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia apenas os arquivos de projeto e restaura dependências
COPY NS.Presentation/NS.Presentation.csproj ./NS.Presentation/
COPY NS.Application/NS.Application.csproj ./NS.Application/
COPY NS.Domain/NS.Domain.csproj ./NS.Domain/
COPY NS.Infra.Data/NS.Infra.Data.csproj ./NS.Infra.Data/
COPY NS.Infra.IoC/NS.Infra.IoC.csproj ./NS.Infra.IoC/

RUN dotnet restore NS.Presentation/NS.Presentation.csproj

# Copia todo o código da solução
COPY . ./

# Build e publish do projeto da API
RUN dotnet publish NS.Presentation/NS.Presentation.csproj -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expõe a porta da API
EXPOSE 8080

# Comando para rodar a API
ENTRYPOINT ["dotnet", "NS.Presentation.dll"]