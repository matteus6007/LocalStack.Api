#!/bin/sh

set -e

hostname=${LOCALSTACK_HOSTNAME:-localstack}
port=4566

>&2 echo "Hostname: $hostname"
>&2 echo "Localstack DynamoDB is up - executing command"
aws --region eu-west-1 --endpoint-url=http://$hostname:$port dynamodb create-table --table-name Accounts --attribute-definitions AttributeName=Id,AttributeType=S --key-schema AttributeName=Id,KeyType=HASH --provisioned-throughput ReadCapacityUnits=5,WriteCapacityUnits=5
