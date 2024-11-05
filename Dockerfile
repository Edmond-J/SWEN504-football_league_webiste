#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY FootballLeagueWebsite.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0  AS final
WORKDIR /app

COPY --from=build /app/publish/ .
EXPOSE 8080
ENTRYPOINT ["dotnet", "FootballLeagueWebsite.dll"]
