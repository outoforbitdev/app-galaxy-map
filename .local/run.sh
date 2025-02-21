#! /bin/bash

clear
cd src/client && npm run dev &
cd src/service && dotnet watch --non-interactive
