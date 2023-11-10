FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Example/Enter.ENB.Example.Api/Enter.ENB.Example.Api.csproj", "Example/Enter.ENB.Example.Api/"]
RUN dotnet restore "Example/Enter.ENB.Example.Api/Enter.ENB.Example.Api.csproj"
COPY . .
WORKDIR "/src/Example/Enter.ENB.Example.Api"
RUN dotnet build "Enter.ENB.Example.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Enter.ENB.Example.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Enter.ENB.Example.Api.dll"]
