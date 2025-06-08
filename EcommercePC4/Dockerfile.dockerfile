# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos la solución y el proyecto
COPY ["EcommercePC4.sln", "."]
COPY ["Pc4sent/EcommercePC4/EcommercePC4.csproj", "Pc4sent/EcommercePC4/"]

# Restauramos los paquetes
RUN dotnet restore "EcommercePC4.sln"

# Copiamos el resto del código
COPY . .

# Compilamos el proyecto
WORKDIR "/src/Pc4sent/EcommercePC4"
RUN dotnet build "EcommercePC4.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "EcommercePC4.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

# Configurar puerto para ASP.NET Core
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080

# Ejecutar la app
ENTRYPOINT ["dotnet", "EcommercePC4.dll"]
