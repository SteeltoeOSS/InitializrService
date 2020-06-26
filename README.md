# Steeltoe.Initializr.WebApi
reference implementation of a Steeltoe Initializr Server project generator

## Overview

`Steeltoe.Initializr.WebApi` provides two REST/HTTP endpoints:
* `api/metadata`
* `api/project`

### `api/metadata`

This endpoint provides configuration metadata for client UIs.  The metadata includes:
* Steeltoe release versions
* Dotnet target frameworks
* Dotnet templates
* Dotnet languages
* Dependencies
* Server "about" details

The configuration data for `Steeltoe.Initializr.WebApi` is provided by a [Spring Cloud Config Server](https://cloud.spring.io/spring-cloud-config/reference/html/) pointing at https://github.com/steeltoeoss-incubator/Steeltoe.Initializr.Configuration.

## Building

```
dotnet build
dotnet test
```

## Running

### Starting the Config Server

Before running, you'll need a Config Server.  You have several options:

* use `Steeltoe.Initializr.ConfigServer` *(recommended)*
* use the Docker image `steeltoeoss/config-server`
* roll your own (beyond the scope of this document)

#### `Steeltoe.Initializr.ConfigServer`

`Steeltoe.Initializr.ConfigServer` is a vanilla Config Server pre-configured to use https://github.com/steeltoeoss-incubator/Steeltoe.Initializr.Configuration.

*To build from source, you'll need Java 14 JDK or later.*

```
# build
git clone https://github.com/steeltoeoss-incubator/Steeltoe.Initializr.ConfigServer.git
cd Steeltoe.Initializr.ConfigServer

# run
./gradlew bootRun
```

#### Docker Image `steeltoeoss/config-server`

The Docker image `steeltoeoss/config-server` is a vanilla Config Server using Spring Cloud's default configuratation repo.

When running, you'll need to override the configuration URI:

```
docker run --publish 8888:8888 steeltoeoss/config-server:2 \
    --spring.cloud.config.server.git.uri=https://github.com/steeltoeoss-incubator/Steeltoe.Initializr.Configuration
```

### Starting `Steeltoe.Initializr.WebApi`

```
dotnet run -p src/Steeltoe.Initializr.WebApi
```


