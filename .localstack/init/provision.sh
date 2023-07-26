#!/bin/sh

#
#   INFO:
#       This scrip will be executed *inside* the localstack container once all its services are up and running
#

set -e

echo "Running db scripts..."
/tmp/dbscripts/v1.0.0/1_create_tables.sh
echo "Db scripts completed"

