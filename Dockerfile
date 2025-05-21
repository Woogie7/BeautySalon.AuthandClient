FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY NuGet.Config .

RUN sed -i 's/%GITHUB_TOKEN%/'"$GITHUB_TOKEN"'/g' NuGet.Config

COPY . .

RUN dotnet restore "BeautySalon.AuthandClient.Api/BeautySalon.AuthandClient.Api.csproj"

WORKDIR "/src/BeautySalon.AuthandClient.Api"
RUN dotnet build "BeautySalon.AuthandClient.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BeautySalon.AuthandClient.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeautySalon.AuthandClient.Api.dll"]