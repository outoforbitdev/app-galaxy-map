# dotnet/sdk:9.0.202 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:d7f4691d11f610d9b94bb75517c9e78ac5799447b5b3e82af9e4625d8c8d1d53 AS build-service
WORKDIR /app

COPY ./src/service .
RUN dotnet restore

WORKDIR /app
RUN dotnet publish -c release -o /out --no-restore

# dotnet/sdk:9.0.202 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk/tags
FROM mcr.microsoft.com/dotnet/sdk@sha256:d7f4691d11f610d9b94bb75517c9e78ac5799447b5b3e82af9e4625d8c8d1d53 AS build-client
WORKDIR /app

RUN curl --silent --location https://deb.nodesource.com/setup_22.x | bash - \
&& apt-get install -y nodejs
COPY ./src/client/package-lock.json .
COPY ./src/client/package.json .
RUN npm install

COPY ./src/client .
RUN npm run build

# dotnet/aspnet:9.0.3 https://mcr.microsoft.com/en-us/artifact/mar/dotnet/aspnet/tags
FROM mcr.microsoft.com/dotnet/aspnet@sha256:bf48e8b328707fae0e63a1b7d764d770221def59b97468c8cdee68f4e38ddfb9 AS runtime
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