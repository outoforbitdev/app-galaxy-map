# dotnet/sdk:9.0.202 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:ae000be75dac94fc40e00f0eee903289e985995cc06dac3937469254ce5b60b6 AS build-service
WORKDIR /app
RUN curl --silent --location https://deb.nodesource.com/setup_22.x | bash - \
&& apt-get install -y nodejs
COPY ./scripts .
RUN chmod -R +x .
CMD [ "./run.sh" ]
