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

### `api/project`

*Under construction*

## Running

### Running `Steeltoe.Initializr.WebApi`

You can run using Dotnet or Docker Compose.

#### Run using Dotnet

This approach requires a running Config Server.  See "Starting a Config Server" below for options.

```
dotnet run -p src/Steeltoe.Initializr.WebApi
```

#### Run using Docker Compose

This approach requires includes a running Config Server.

```
docker-compose up             # starts config server and webapi
docker-compose down           # stops config server and webapi
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

*To build from source, you'll need Java 14 JDK or later.*

```
# clone
git clone https://github.com/steeltoeoss-incubator/Steeltoe.Initializr.ConfigServer.git
cd Steeltoe.Initializr.ConfigServer

# run
./gradlew bootRun
```
