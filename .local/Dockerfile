# dotnet/sdk:9.0.202 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:b768b444028d3c531de90a356836047e48658cd1e26ba07a539a6f1a052a35d9 AS build-service
WORKDIR /app
RUN curl --silent --location https://deb.nodesource.com/setup_22.x | bash - \
&& apt-get install -y nodejs
COPY ./scripts .
RUN chmod -R +x .
CMD [ "./run.sh" ]
