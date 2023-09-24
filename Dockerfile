FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update && apt-get install -y curl jq # for healthchecks
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["common/server", "common/server"]
COPY ["features/identity/server", "features/identity/server"]
COPY ["features/project/server", "features/project/server"]
COPY ["features/team/server", "features/team/server"]
COPY ["features/user/server", "features/user/server"]
RUN dotnet restore "common/server/Garnet/Garnet.csproj"
RUN dotnet build "common/server/Garnet/Garnet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "common/server/Garnet/Garnet.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish ["/app/publish", "."]
ENTRYPOINT ["dotnet", "Garnet.dll"]
