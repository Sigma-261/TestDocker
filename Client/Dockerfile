FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV ASPNETCORE_URLS="http://+:1010"
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app

COPY Client.csproj .
RUN dotnet restore ./Client.csproj

COPY . .

RUN dotnet publish ./Client.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base


COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Client.dll"]