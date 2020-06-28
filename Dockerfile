FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY . .
RUN dotnet restore
RUN dotnet publish -c release -o /srv --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /srv
COPY --from=build /srv .
ENTRYPOINT ["dotnet", "Steeltoe.Initializr.WebApi.dll"]
