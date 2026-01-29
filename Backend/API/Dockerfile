# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src/API

COPY . .
RUN dotnet restore API/API.csproj
RUN dotnet publish API/API.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:5000

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]
EXPOSE 5000