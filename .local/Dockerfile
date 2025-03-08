FROM mcr.microsoft.com/dotnet/sdk@sha256:483d6f3faa583c93d522c4ca9ee54e08e535cb112dceb252b2fbb7ef94839cc8 
WORKDIR /app
RUN curl --silent --location https://deb.nodesource.com/setup_20.x | bash - \
&& apt-get install -y nodejs
COPY ./run.sh .
RUN chmod +x ./run.sh
ENTRYPOINT [ "./run.sh" ]
