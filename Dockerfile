FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["GestorDeTareas/GestorDeTareas.csproj", "GestorDeTareas/"]
RUN dotnet restore "GestorDeTareas/GestorDeTareas.csproj"
COPY . .
WORKDIR "/src/GestorDeTareas"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GestorDeTareas.dll"]