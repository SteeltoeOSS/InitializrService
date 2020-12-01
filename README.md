# Steeltoe InitializrApi

Steeltoe Initializr API reference implementation

[![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status/Initializr/SteeltoeOSS.InitializrApi?branchName=master)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=31&branchName=master)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=InitializrApi&metric=alert_status)](https://sonarcloud.io/dashboard?id=InitializrApi)

## Using the Server

_InitializrApi_ provides 4 REST/HTTP endpoints:
* `api/`
* `api/about`
* `api/config`
* `api/project`

### `api/`

`api/` accepts GET requests ands returns an _InitializrAPI_ help document.
The document includes available parameters (and their defaults) and dependencies, as well as some CLI samples.

```sh
$ http -p b https://start.steeltoe.io/api/
...
This service generates quickstart projects that can be easily customized.
Possible customizations include a project's dependencies and .NET target framework.

The URI templates take a set of parameters to customize the result of a request.
+-----------------+-----------------------+----------------------------+
| Parameter       | Description           | Default value              |
+-----------------+-----------------------+----------------------------+
| name            | project name          | Sample                     |
| applicationName | application name      | SampleApplication          |
...
```

### `api/about`

`api/about` accepts GET requests ands returns _InitialzrAPI_ "About" information.

```sh
$ http -p b https://start.steeltoe.io/api/about
{
    "commit": "381bbd2a1e30d621ed6ad4a07790955447ffe468",
    "name": "Steeltoe.InitializrApi",
    "url": "https://github.com/SteeltoeOSS/InitializrApi/",
    "vendor": "SteeltoeOSS/VMware",
    "version": "0.8.0"
}
```

### `api/config`

`api/config` accepts GET requests and returns _InitializrApi_ configuration.

The returned document includes _all_ configuration.   Sub-endpoints are available allowing for more targeted responses.

`api/config/projectMetadata` can be used by smart clients, such as [_InitializrWeb_](https://github.com/SteeltoeOSS/InitializrWeb), to assist in creating user interfaces.

The following endpoints can be used by CLI users to determine what project configuration options are available:
* `api/config/archiveTypes`
* `api/config/dependencies`
* `api/config/dotNetFrameworks`
* `api/config/dotNetTemplates`
* `api/config/languages`
* `api/config/steeltoeVersions`

```sh
# sample: list available Steeltoe versions
$ http -p b https://start.steeltoe.io/api/config/steeltoeVersions
[
    {
        "id": "2.4.4",
        "name": "Steeltoe 2.4.4 Maintenance Release"
    },
    {
        "id": "2.5.1",
        "name": "Steeltoe 2.5.1 Maintenance Release"
    },
    {
        "id": "3.0.1",
        "name": "Steeltoe 3.0.1 Maintenance Release"
    }
]

# sample: list available dependency IDs
$ http https://start.steeltoe.io/api/config/dependencies | jq '.[] .values[] .id' | sort
"actuator"
"amqp"
"azure-spring-cloud"
"circuit-breaker"
"cloud-foundry"
"config-server"
"data-mongodb"
"data-redis"
"docker"
"dynamic-logger"
"eureka-client"
"mysql"
"mysql-efcore"
"oauth"
"placeholder"
"postgresql"
"postgresql-efcore"
"random-value"
"sqlserver"
```

### `api/project`

`api/project` accepts GET and POST requests and returns a project as an archive.

Projects are configured by using HTTP parameters, such as `name` for project name and `steeltoeVersion` for Steeltoe version.
The parameter `dependencies` is a little different than other parameters in that it is set to a comma-separated list of dependency IDs.

To get a list of parameters and dependencies, send a GET request to `api/`.

```sh
# sample: generate a .NET Core App 3.1 project with a actuator endpoints and a Redis backend:
$ http https://start.steeltoe.io/api/project dotNetFramework=netcoreapp3.1 dependencies==actuators,redis -d
```

## Configuring the Server

The Initializr API configuration is a JSON document provided by a [Spring Cloud Config Server](https://cloud.spring.io/spring-cloud-config/multi/multi__spring_cloud_config_server.html) or a local file.
The former is recommended for production deployments; the latter is intended primarily for local development.


### Using Spring Cloud Config Server

The Initializr API uses the [Steeltoe Config Server Provider](https://docs.steeltoe.io/api/v3/configuration/config-server-provider.html) to get configuration from a Spring Cloud Config Server.

The _InitializrAPI_ running at https://start.steeltoe.io/api/ uses a Spring Cloud Config Server backended at https://github.com/SteeltoeOSS/InitializrConfig.
The following `appsettings.json` sample snippet is part of the _InitializrApi_'s configuration:
```json
  ...,
  "spring": {
    "application": {
      "name": "SteeltoeInitializr"
    },
    "cloud": {
      "config": {
        "uri": "http://initializr-config-server/"
      }
    }
  },
  ...
```


See the Steeltoe Config Server Provider documentation for other configuration options.

### Using a local file

_**Note**_: configuring a local file overrides any Spring Cloud Config Server configuration

The following `appsettings.json` sample snippet configures the use of a local configuration file:
```json
  ...,
  "Initializr": {
    "Configuration" : {
      "Path": "Resources/config.json"
    }
  },
  ...
```
