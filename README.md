# Instructions

LocalStack - A fully functional local AWS cloud stack https://github.com/localstack/localstack

## Running LocalStack

Start LocalStack:

```
docker-compose -f docker-compose.localstack.yml up
```

Navigate to http://localhost:4566/health in a browser to check localstack is running, you should see something similar to:

```
{
   "services" : {
      "dynamodbstreams" : "running",
      "kinesis" : "running",
      "s3" : "running",
      "dynamodb" : "running"
   }
}
```

## Running DynamoDB Local

https://hub.docker.com/r/amazon/dynamodb-local

```
docker-compose -f docker-compose.dynamodb-local.yml up
```

## Setting up AWS Infrastructure

### Using AWS CLI

Download AWS CLI from https://aws.amazon.com/cli/.

Create new AWS credentials:

```
aws configure --profile test
AWS Access Key ID [None]: test
AWS Secret Access Key [None]: test
Default region name [None]: eu-west-1
Default output format [None]: json
```

Create Dynamo `Accounts` table:

```
aws --endpoint-url=http://localhost:4566 --profile test dynamodb create-table --table-name Accounts --attribute-definitions AttributeName=Id,AttributeType=S --key-schema AttributeName=Id,KeyType=HASH --provisioned-throughput ReadCapacityUnits=5,WriteCapacityUnits=5
```

Output:

```
{
    "TableDescription": {
        "AttributeDefinitions": [
            {
                "AttributeName": "Id",
                "AttributeType": "S"
            }
        ],
        "TableName": "Accounts",
        "KeySchema": [
            {
                "AttributeName": "Id",
                "KeyType": "HASH"
            }
        ],
        "TableStatus": "ACTIVE",
        "CreationDateTime": "2020-12-16T11:41:23.637000+00:00",
        "ProvisionedThroughput": {
            "LastIncreaseDateTime": "1970-01-01T00:00:00+00:00",
            "LastDecreaseDateTime": "1970-01-01T00:00:00+00:00",
            "NumberOfDecreasesToday": 0,
            "ReadCapacityUnits": 5,
            "WriteCapacityUnits": 5
        },
        "TableSizeBytes": 0,
        "ItemCount": 0,
        "TableArn": "arn:aws:dynamodb:us-east-1:000000000000:table/Accounts"
    }
}
```

Check the table has been created:

```
aws --endpoint-url http://localhost:4566 --profile test dynamodb list-tables
```

Output:

```
{
    "TableNames": [
        "Accounts"
    ]
}
```

Scan table contents:

```
aws --endpoint-url http://localhost:4566 --profile test dynamodb scan --table-name Accounts
```

### Using Docker

```
docker-compose -f docker-compose.dev-env.yml up
```

### Using Terraform

https://registry.terraform.io/providers/hashicorp/aws/latest/docs/guides/custom-service-endpoints#localstack
https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/dynamodb_table

Navigate to `/terraform/local/`.

* Initialize infrastructure `terraform init`
* Validate the correct resources will be generated `terraform plan`
* Execute `terraform apply`

## Run LocalStack in background

Start LocalStack and create AWS resources:

```
.\localstack.ps1 start
```

Stop LocalStack and remove AWS resources:

```
.\localstack.ps1 stop
```

## Running the app

Run the app locally:

```
docker-compose up --build
```

Navigate to http://localhost:1001/swagger/index.html to load Swagger UI.

Run the integration tests:

```
dotnet test
```

## Resources

https://www.maxcode.net/blog/using-localstack-for-development-environments/
https://spin.atomicobject.com/2020/02/03/localstack-terraform-circleci/