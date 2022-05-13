FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ServiceReleaseManager/ServiceReleaseManager.csproj", "ServiceReleaseManager/"]
RUN dotnet restore "ServiceReleaseManager/ServiceReleaseManager.csproj"
COPY . .
WORKDIR "/src/ServiceReleaseManager"
RUN dotnet build "ServiceReleaseManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceReleaseManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceReleaseManager.dll"]
