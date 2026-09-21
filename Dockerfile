# dotnet/sdk:10.0.100 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:c7445f141c04f1a6b454181bd098dcfa606c61ba0bd213d0a702489e5bd4cd71 AS build-service
WORKDIR /app

COPY ./src/service .
RUN dotnet restore

WORKDIR /app
RUN dotnet publish -c release -o /out --no-restore

# dotnet/sdk:10.0.100 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:c7445f141c04f1a6b454181bd098dcfa606c61ba0bd213d0a702489e5bd4cd71 AS build-client
WORKDIR /app

RUN curl --silent --location https://deb.nodesource.com/setup_22.x | bash - \
&& apt-get install -y nodejs
COPY ./src/client/package-lock.json .
COPY ./src/client/package.json .
RUN npm install

COPY ./src/client .
RUN npm run build

# dotnet/aspnet:10.0.0 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/aspnet/tags
FROM mcr.microsoft.com/dotnet/aspnet@sha256:6a94333d37514e385650a3c81a55e5350b67253dbe136e9cf17e499c35606a8c AS runtime
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