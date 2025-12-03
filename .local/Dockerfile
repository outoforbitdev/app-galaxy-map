# dotnet/sdk:10.0.100 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:c7445f141c04f1a6b454181bd098dcfa606c61ba0bd213d0a702489e5bd4cd71 AS build-service
WORKDIR /app
RUN curl --silent --location https://deb.nodesource.com/setup_22.x | bash - \
&& apt-get install -y nodejs
COPY ./scripts .
RUN chmod -R +x .
CMD [ "./run.sh" ]
