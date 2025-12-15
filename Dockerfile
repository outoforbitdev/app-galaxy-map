# dotnet/sdk:10.0.100 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:d1823fecac3689a2eb959e02ee3bfe1c2142392808240039097ad70644566190 AS build-service
WORKDIR /app

COPY ./src/service .
RUN dotnet restore

WORKDIR /app
RUN dotnet publish -c release -o /out --no-restore

# dotnet/sdk:10.0.100 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:d1823fecac3689a2eb959e02ee3bfe1c2142392808240039097ad70644566190 AS build-client
WORKDIR /app

RUN curl --silent --location https://deb.nodesource.com/setup_22.x | bash - \
&& apt-get install -y nodejs
COPY ./src/client/package-lock.json .
COPY ./src/client/package.json .
RUN npm install

COPY ./src/client .
RUN npm run build

# dotnet/aspnet:10.0.0 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/aspnet/tags
FROM mcr.microsoft.com/dotnet/aspnet@sha256:7c4246c1c384319346d45b3e24a10a21d5b6fc9b36a04790e1588148ff8055b0 AS runtime
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