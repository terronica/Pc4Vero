# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y proyecto
COPY ["EcommercePC4.sln", "."]
COPY ["EcommercePC4/EcommercePC4.csproj", "EcommercePC4/"]

# Restaurar paquetes
RUN dotnet restore "EcommercePC4.sln"

# Copiar el resto del código - espechal
COPY . .

# Compilar
WORKDIR "/src/EcommercePC4"
RUN dotnet build "EcommercePC4.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "EcommercePC4.csproj" -c Release -o /app/publish

# Etapa runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

# Configurar y exponer puerto
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "EcommercePC4.dll"]
