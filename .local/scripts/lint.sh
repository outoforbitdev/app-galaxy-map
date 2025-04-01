#! /bin/bash

cd /app/src && dotnet tool install --create-manifest-if-needed csharpier
dotnet csharpier $1 .