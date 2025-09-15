# dotnet/sdk:9.0.202 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:bb42ae2c058609d1746baf24fe6864ecab0686711dfca1f4b7a99e367ab17162 AS build-service
WORKDIR /app

COPY ./src/service .
RUN dotnet restore

WORKDIR /app
RUN dotnet publish -c release -o /out --no-restore

# dotnet/sdk:9.0.202 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:bb42ae2c058609d1746baf24fe6864ecab0686711dfca1f4b7a99e367ab17162 AS build-client
WORKDIR /app

RUN curl --silent --location https://deb.nodesource.com/setup_22.x | bash - \
&& apt-get install -y nodejs
COPY ./src/client/package-lock.json .
COPY ./src/client/package.json .
RUN npm install

COPY ./src/client .
RUN npm run build

# dotnet/aspnet:9.0.3 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/aspnet/tags
FROM mcr.microsoft.com/dotnet/aspnet@sha256:4f0ad314f83e6abeb6906e69d0f9c81a0d2ee51d362e035c7d3e6ac5743f5399 AS runtime
WORKDIR /app

RUN apt-get update -y
RUN apt-get install curl -y
RUN curl --silent --location https://deb.nodesource.com/setup_22.x | bash - \
&& apt-get install -y nodejs
ENV NODE_ENV=production
RUN addgroup --system --gid 1001 nodejs
RUN adduser --system --uid 1001 nextjs

COPY --chown=nodejs:nextjs ./run.sh .
RUN chmod +x ./run.sh
COPY --from=build-service /out .
COPY --from=build-client /app/.next/standalone ./next
COPY --from=build-client /app/.next/static ./next/.next/static
USER nextjs
ENTRYPOINT ["./run.sh"]