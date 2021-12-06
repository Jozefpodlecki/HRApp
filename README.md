# HR App #

<p align="center">
  <img src="demo-images\sign-in.png" width="350" title="hover text">
  <img src="demo-images\main-portal.png" width="350" title="hover text">
</p>

Simple .net web app which keeps records of day offs.

![example workflow](https://github.com/jozefpodlecki/hrapp/actions/workflows/dotnet.yml/badge.svg)

## Getting started ##

### Rabbit MQ ###

Install rabbitmq via chocolate or docker

```console
choco install rabbitmq
```

Run management tool.

```
rabbitmq-plugins.bat enable rabbitmq_management
```

Default credentials for rabbit mq server: `guest` `guest`

Default link rabbit mq server portal [http://localhost:15672](http://localhost:15672/#/)
