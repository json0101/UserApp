# Etapa 1: compilar y publicar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el codigo (api + domain + repository + service)
COPY . .

# Restaurar y publicar solo la API (arrastra sus proyectos dependientes)
RUN dotnet restore "api/UserApp.Api.csproj"
RUN dotnet publish "api/UserApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: imagen ligera solo para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "UserApp.Api.dll"]
