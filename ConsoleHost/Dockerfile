FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ConsoleHost/ConsoleHost.csproj", "ConsoleHost/"]
RUN dotnet restore "ConsoleHost/ConsoleHost.csproj"
COPY . .
WORKDIR "/src/ConsoleHost"
RUN dotnet build "ConsoleHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleHost.dll"]
