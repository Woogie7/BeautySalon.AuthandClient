FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["BeautySalon.AuthandClient.Api/BeautySalon.AuthandClient.Api.csproj", "BeautySalon.AuthandClient.Api/"]
COPY ["BeautySalon.AuthandClient.Persistence/BeautySalon.AuthandClient.Persistence.csproj", "BeautySalon.AuthandClient.Persistence/"]
COPY ["BeautySalon.AuthandClient.Domain/BeautySalon.AuthandClient.Domain.csproj", "BeautySalon.AuthandClient.Domain/"]

RUN dotnet restore "BeautySalon.AuthandClient.Api/BeautySalon.AuthandClient.Api.csproj"

COPY . .
WORKDIR "/src/BeautySalon.AuthandClient.Api"

RUN dotnet build "./BeautySalon.AuthandClient.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BeautySalon.AuthandClient.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BeautySalon.AuthandClient.Api.dll"]