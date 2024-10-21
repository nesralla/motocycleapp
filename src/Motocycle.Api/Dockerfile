FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 80
EXPOSE 5432

ENV ASPNETCORE_URLS=http://+:8080

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Motocycle.Api/Motocycle.Api.csproj", "src/Motocycle.Api/"]
RUN dotnet restore "src/Motocycle.Api/Motocycle.Api.csproj"
COPY . .
WORKDIR "/src/src/Motocycle.Api"
RUN dotnet build "Motocycle.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Motocycle.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Motocycle.Api.dll"]
