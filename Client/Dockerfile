FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV ASPNETCORE_URLS "http://+:7070"
ENV ASPNETCORE_ENVIRONMENT Development

WORKDIR /app

COPY Client.csproj ./
COPY . ./

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

COPY --from=build /app/publish .
#RUN apt-get update && apt-get install -y procps && rm -rf /var/lib/apt/lists/*
ENTRYPOINT ["dotnet", "Client.dll"]