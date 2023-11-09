FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EME.App/EME.App.csproj", "EME.App/"]
RUN dotnet restore "EME.App/EME.App.csproj"
COPY . .
WORKDIR "/src/EME.App"
RUN dotnet build "EME.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EME.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EME.App.dll"]
