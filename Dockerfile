FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /source

COPY . .
RUN dotnet restore
RUN dotnet publish -c release -o /srv --no-restore

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine
WORKDIR /srv
RUN apk add bash
RUN curl https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh > wait-for-it \
            && chmod +x wait-for-it
COPY --from=build /srv .
ENV DOTNET_URLS http://0.0.0.0:80
ENTRYPOINT ["dotnet", "Steeltoe.InitializrService.dll"]
