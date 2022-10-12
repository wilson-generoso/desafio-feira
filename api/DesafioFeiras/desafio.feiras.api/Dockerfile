#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["inmetrics.desafio.lancamento.api/inmetrics.desafio.lancamento.api.csproj", "inmetrics.desafio.lancamento.api/"]
RUN dotnet restore "inmetrics.desafio.lancamento.api/inmetrics.desafio.lancamento.api.csproj"
COPY . .
WORKDIR "/src/inmetrics.desafio.lancamento.api"
RUN dotnet build "inmetrics.desafio.lancamento.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "inmetrics.desafio.lancamento.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "inmetrics.desafio.lancamento.api.dll"]