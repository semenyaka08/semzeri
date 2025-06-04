FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Semzeri.API/Semzeri.API.csproj", "src/Semzeri.API/"]
COPY ["src/Semzeri.DAL/Semzeri.DAL.csproj", "src/Semzeri.DAL/"]
COPY ["src/Semzeri.BusinessLogic/Semzeri.BusinessLogic.csproj", "src/Semzeri.BusinessLogic/"]
RUN dotnet restore "src/Semzeri.API/Semzeri.API.csproj"
COPY . .
WORKDIR "/src/src/Semzeri.API"
RUN dotnet build "Semzeri.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Semzeri.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Semzeri.API.dll"]