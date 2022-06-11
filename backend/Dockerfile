FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ServiceReleaseManager.Api/ServiceReleaseManager.Api.csproj", "ServiceReleaseManager.Api/"]
COPY ["src/ServiceReleaseManager.Infrastructure/ServiceReleaseManager.Infrastructure.csproj", "ServiceReleaseManager.Infrastructure/"]
COPY ["src/ServiceReleaseManager.Core/ServiceReleaseManager.Core.csproj", "ServiceReleaseManager.Core/"]
COPY ["src/ServiceReleaseManager.SharedKernel/ServiceReleaseManager.SharedKernel.csproj", "ServiceReleaseManager.SharedKernel/"]
RUN dotnet restore "/src/ServiceReleaseManager.Api/ServiceReleaseManager.Api.csproj"

COPY . .
WORKDIR "/src/src/ServiceReleaseManager.Api"
RUN dotnet build "ServiceReleaseManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceReleaseManager.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceReleaseManager.Api.dll"]
