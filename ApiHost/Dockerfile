FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AOC2023/AOC2023.csproj", "AOC2023/"]
RUN dotnet restore "AOC2023/AOC2023.csproj"
COPY . .
WORKDIR "/src/AOC2023"
RUN dotnet build "AOC2023.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AOC2023.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AOC2023.dll"]
