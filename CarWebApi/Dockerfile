FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5050
EXPOSE 7050

# Генерация SSL сертификата
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS certs
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p 123

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarWebApi/CarWebApi.csproj", "CarWebApi/"]
RUN dotnet restore "CarWebApi/CarWebApi.csproj"
COPY . .
WORKDIR "/src/CarWebApi"
RUN dotnet build "CarWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CarWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Копируем SSL сертификат
COPY --from=certs /https/aspnetapp.pfx /https/aspnetapp.pfx
ENTRYPOINT ["dotnet", "CarWebApi.dll"]