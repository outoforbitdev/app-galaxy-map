app_name := "app-galaxy-map"
port := "1798"
api_port := "1799"

install:
    npm install
    npx husky install
    npx husky init
    echo "npx commitlint --edit \$1 --config ./.linters/config/commitlint.config.js" > .husky/commit-msg
    echo "just lint" > .husky/pre-commit
    cd src/client && npm install

run: stop
    docker compose -f .local/docker-compose.yml up -d --build
    open http://localhost:{{port}}

restart-node:
    docker exec -itd app-galaxy-map-app-1  sh ./restart-node.sh

setup-local:
    cd src/service && dotnet ef migrations script --idempotent --output ../../.local/db/migration.sql
    docker exec -it app-galaxy-map-db-1 sh /data/db/setup-db.sh
    rm .local/db/migration.sql

build: clean
    # Build image
    docker build -t outoforbitdev/{{app_name}}:sha-$(git rev-parse --short HEAD) .
    # Run image
    docker run -d -p {{port}}:3000 -p {{api_port}}:8080 --name {{app_name}} outoforbitdev/{{app_name}}:sha-$(git rev-parse --short HEAD) 
    # Wait for the server to start
    # docker container exec {{app_name}} wget http://localhost:8080 &> /dev/null
    open http://localhost:{{port}}

clean: stop
    -docker compose -f .local/docker-compose.yml  rm -f
    -docker rm {{app_name}}
    -docker rmi {{app_name}}

stop:
    -docker compose -f .local/docker-compose.yml  stop
    -docker stop {{app_name}}

get-ip:
    echo "http://$(ipconfig getifaddr en0):{{port}}"

lint:
    docker build .local --tag local-lint
    docker rm csharpier
    docker run -v ./src:/app/src -v ~/.nuget/packages:/root/.nuget/packages --name csharpier local-lint /bin/sh ./lint.sh --check
    npx prettier --check --ignore-path src/client/.gitignore src/client/
    yamllint -c .yamllint.yml .github/

lint-write:
    docker build .local --tag local-lint
    docker rm csharpier
    docker run -v ./src:/app/src -v ~/.nuget/packages:/root/.nuget/packages --name csharpier local-lint /bin/sh ./lint.sh; echo $?
    npx prettier --write --ignore-path src/client/.gitignore src/client/
    yamllint -c .yamllint.yml .github/

migrate NAME:
    cd src/service && dotnet ef migrations add {{NAME}}

test:
    cd src/serviceTests && dotnet test
