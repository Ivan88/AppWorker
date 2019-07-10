FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["AppWorker.csproj", ""]
RUN dotnet restore "AppWorker.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "AppWorker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AppWorker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AppWorker.dll"]