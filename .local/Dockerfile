# dotnet/sdk:10.0.100 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:f061e5a7532b36fa1d1b684857fe1f504ba92115b9934f154643266613c44c62 AS build-service
WORKDIR /app
RUN curl --silent --location https://deb.nodesource.com/setup_22.x | bash - \
&& apt-get install -y nodejs
COPY ./scripts .
RUN chmod -R +x .
CMD [ "./run.sh" ]
