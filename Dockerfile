FROM mcr.microsoft.com/dotnet/core/sdk:3.1.402-alpine3.12 AS build
WORKDIR /source

COPY . .
RUN dotnet restore
RUN dotnet publish -c release -o /srv --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /srv
RUN curl https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh > wait-for-it \
            && chmod +x wait-for-it
COPY --from=build /srv .
ENTRYPOINT ["dotnet", "Steeltoe.InitializrApi.dll"]
