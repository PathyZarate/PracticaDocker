# 1. ETAPA DE CONSTRUCCIÓN (BUILD)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solo el archivo de proyecto primero para la capa de 'restore'
COPY ["tienda.csproj", "./"]
RUN dotnet restore "tienda.csproj"

# Copia el resto del código fuente
COPY . .

# Compilar
RUN dotnet build "tienda.csproj" -c Release -o /app/build

# 2. ETAPA DE PUBLICACIÓN
FROM build AS publish
RUN dotnet publish "tienda.csproj" -c Release -o /app/publish

# 3. ETAPA FINAL (RUNTIME)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define el punto de entrada de la aplicación
ENTRYPOINT ["dotnet", "tienda.dll"]