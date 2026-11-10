FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /source
COPY . .
RUN dotnet restore /p:Configuration=Release /p:NuGetAudit=false
RUN dotnet build --no-restore --configuration Release
RUN dotnet publish --no-build --output /srv

FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine
WORKDIR /srv
COPY --from=build /srv .
ENV HTTP_PORTS=8080
ENTRYPOINT ["dotnet", "Steeltoe.InitializrService.dll"]
