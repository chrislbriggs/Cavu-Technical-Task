Overview:
Running the terraform will create a DynamoDb, Lambda, API Gateway, IAM Role.
Streams are enabled on this table (new and old images) so we can create event streams based on changes in this table later.
Sending a POST request to the CarPark POST endpoint will seed a car park entity in the dynamo table which will be used for other requests.
Availability can be found using the Availability endpoint.
Quotes can be found using the Quotations endpoint.
Bookings can be created (POST), amended (PUT) and cancelled (DELETE) using the available endpoints.
Swagger is installed so we have documentation of all endpoints (running the API should redirect to the swagger UI).

Prerequisites: 
Terraform
AWS CLI Tools (dotnet tool install -g Amazon.Lambda.Tools) to enable a publish of the function (dotnet lambda deploy-function) - this would normally be done via CI/CD pipelines


To Deploy:
Publish the function, running the CLI command (dotnet lambda deploy-function) in the Cavu.Api folder.
Moving to the Cavu.Api\Deployment folder, we can run terraform commands to deploy the infrastructure
terraform init
terraform plan
terraform apply

The 'terraform apply' will then create the required infrastructure (storing a state locally, state management such as terragrunt could be used).
The following resources will be created on apply:
API Gateway
Lambda
IAM Role
DynamoDb

Limitations:
Logging: no logging provider has been setup, although standard logs will be logged to cloudwatch (requests and responses are logged)
Authentication & Authorization: no authentication/authorization has been setup. I would do this using Cognito, which allows clientId/clientSecret authentication using oauth2. Authorization can also be setup using Cognito.
Change History: no tracking of changes has been done, such as userId's, update times, correlation id's which can be used for event tracking throughout other systems.
Not all Business Logic around the availability has been included

Considerations:
A serverless stack was used as with only 10 spaces, the car park will not get many bookings. This is the most cost effective approach for this scenario (assuming we stay within the free AWS tier.
Dynamo may not be the best database if this were to be used at scale. The Scan operations required in the current setup a costly and not time efficient. More thought would be required for dynamo to work at scale (upfront access patterns).
Dynamo streams have been enabled with the thought that these could be used for raising events (bookings, quotes etc)
Custom Domain names in API gateway have not been used as this requires additional setup such as certificates (which are beyond the scope)
Least privilege has been applied to the IAM role in regards to the dynamo table (the same could be done for cloudwatch, if the log group was created via tf instead of automatically via lambda).

