resource "aws_lambda_function" "lambda_function" {
	function_name = "cavu-api"
	filename = "${path.module}/Cavu.Api.zip"
	role = aws_iam_role.role.arn
	handler = "Cavu.Api"
	runtime = "dotnet6"
	architectures = ["x86_64"]
	source_code_hash = filebase64sha256("${path.module}/Cavu.Api.zip")
	memory_size = 512
	timeout = 30
	environment {
		variables =	tomap({LAMBDA_NET_SERIALIZER_DEBUG = true})
    }
}