# dotnet/sdk:9.0.202 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:86fe223b90220ec8607652914b1d7dc56fc8ff422ca1240bb81e54c4b06509e6 AS build-service
WORKDIR /app
RUN curl --silent --location https://deb.nodesource.com/setup_22.x | bash - \
&& apt-get install -y nodejs
COPY ./scripts .
RUN chmod -R +x .
CMD [ "./run.sh" ]
