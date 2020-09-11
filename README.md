# Steeltoe InitializrApi

Steeltoe Initializr API reference implementation

[![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status/Initializr/SteeltoeOSS.InitializrApi?branchName=master)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=31&branchName=master)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=InitializrApi&metric=alert_status)](https://sonarcloud.io/dashboard?id=InitializrApi)

## Overview

`InitializrApi` provides two REST/HTTP endpoints:
* `api/configuration`
* `api/project`

### `api/configuration`

This endpoint provides configuration metadata for client UIs.  The metadata includes:

* Steeltoe release versions
* Dotnet target frameworks
* Dotnet templates
* Dotnet languages
* Dependencies
* Server "about" details

The configuration metadata for `InitializrApi` is provided by a [Spring Cloud Config Server](https://cloud.spring.io/spring-cloud-config/reference/html/) pointing at https://github.com/steeltoeoss-incubator/Steeltoe.Initializr.Configuration.

### `api/project`

*Under construction*

## Running

### Running `InitializrApi`

You can run using Dotnet or Docker Compose.

#### Run using Dotnet

This approach requires a running Config Server.  See "Starting a Config Server" below for options.

```
dotnet run -p src/InitializrApi
```

#### Run using Docker Compose

This approach requires includes a running Config Server.

```
docker-compose up             # starts config server and api
docker-compose down           # stops config server and api
docker-compose build          # run this if you've made changes after running "docker-compose up"
```

### Starting a Config Server

Before running, you'll need a Config Server.  You have several options:

* Docker image `steeltoess/initializr.configserver` *(recommended)*
* build `Steeltoe.Initializr.ConfigServer`
* roll your own (beyond the scope of this document)

#### Docker Image `steeltoeoss/initializr.configserver`

The Docker image `steeltoeoss/initializr.configserver` is a Spring Cloud Config Server configured to use https://github.com/steeltoeoss-incubator/Steeltoe.Initializr.Configuration.

To run:

```
docker run --publish 8888:8888 steeltoeoss/initializr.configserver
```

You can override defaults using standard Spring command line parameters.

Example:
```
# use a different config source and enable DEBUG logging
docker run --publish 8888:8888 steeltoeoss/initializr.configserver \
    --spring:cloud:config:uri=http://localhost:8888 \
    --logging.level.org.springframework.web=debug
```

#### Build `Steeltoe.Initializr.ConfigServer`

`Steeltoe.Initializr.ConfigServer` is the Spring Cloud Config Server used in the `steeltoeoss/initializr.configserver` Docker image.

*To build from source, you'll need Java 11 JDK or later.*

```
# clone
git clone https://github.com/steeltoeoss-incubator/Steeltoe.Initializr.ConfigServer.git
cd Steeltoe.Initializr.ConfigServer

# run
./gradlew bootRun
```

## Deploying

## Cloud Foundry

```
dotnet build
cf push deploy/cloud-foundry.manifest-<profile>.yaml
```
