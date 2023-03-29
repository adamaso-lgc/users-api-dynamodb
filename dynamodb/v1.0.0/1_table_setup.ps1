<#
    INFO:
        This script is intended for cloud environment, e.g. DEVQA where it attempts to
        create all required tables for the Conversations service
        
        For local development (localstack) let the service create its own tables
        (see the note in 1_create_tables.sh)
#>
Write-Host "Creating tables..."

### EU ###
## users_ilab ##
aws dynamodb create-table `
    --table-name users_ilab `
    --attribute-definitions AttributeName=user_id,AttributeType=S AttributeName=read_at,AttributeType=N\
    --key-schema AttributeName=user_id,KeyType=HASH AttributeName=read_at,KeyType=RANGE \
    --billing-mode PAY_PER_REQUEST `
    --region eu-west-1


### US ###
## conversations_policies ##
aws dynamodb create-table `
    --table-name users_ilab `
    --attribute-definitions AttributeName=user_id,AttributeType=S AttributeName=read_at,AttributeType=N\
    --key-schema AttributeName=user_id,KeyType=HASH AttributeName=read_at,KeyType=RANGE \
    --billing-mode PAY_PER_REQUEST `
    --region us-east-1

Write-Host "Tables created."