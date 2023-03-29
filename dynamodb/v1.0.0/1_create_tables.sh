endpoint="--endpoint-url=http://localhost:4566"

echo "Creating tables..."
    
aws $endpoint dynamodb create-table \
    --table-name "users_ilab" \
    --attribute-definitions AttributeName=pk,AttributeType=S AttributeName=sk,AttributeType=S \
    --key-schema AttributeName=pk,KeyType=HASH AttributeName=sk,KeyType=RANGE \
    --stream-specification StreamEnabled=true,StreamViewType=NEW_IMAGE \
    --sse-specification Enabled=true,SSEType=KMS \
    --region eu-west-1 \
    --billing-mode PAY_PER_REQUEST
        
    
    
echo "Tables created."