ARG version='1.0.0'
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG version
WORKDIR /api

# Copia os arquivos para dentro do container
COPY . .

# Executa o build na solução inteira
RUN dotnet build "./api/api.csproj" -c Release -o /app

# Executa o publish da API
WORKDIR /api
RUN dotnet publish "./api/api.csproj" -c Release -o /app

RUN mkdir /app/logs/
RUN mkdir /app/wwwroot/

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
EXPOSE 443
WORKDIR /app
ENV ASPNETCORE__URLS=http://+:80;http://+:443;
COPY --from=build /app . 
ENTRYPOINT ["dotnet", "api.dll" ]
