#!/bin/sh

set -e

echo "Cleaning up Localstack ..."
docker-compose  -f ./../.docker/docker-compose-ls.yml  stop
docker-compose  -f ./../.docker/docker-compose-ls.yml  down -v

echo "Checking for volumes ..."
volumes=`docker volume ls -q`
for volume_name in $volumes
do
    if [ $volume_name = "docker_localstack" ];
    then
        docker volume rm $volume_name
        docker volume rm $(docker volume ls -f dangling=true)
    fi
done
echo "All volumes cleared ..."