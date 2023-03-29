#!/bin/sh

set -e

echo "Starting Localstack ..."
docker-compose -f ./../.docker/docker-compose-ls.yml --env-file ./../.docker/.env rm -svf
docker-compose  -f ./../.docker/docker-compose-ls.yml  --env-file ./../.docker/.env up --timeout 20  --remove-orphans
