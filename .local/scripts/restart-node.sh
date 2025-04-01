#! /bin/bash

kill $(pidof node) && cd src/client && npm run dev
