data "aws_iam_policy_document" "lambda_policy_document" {
	statement {
		effect = "Allow"
		actions = ["sts:AssumeRole"]
		principals {
			type = "Service"
			identifiers = ["lambda.amazonaws.com"]
		}
	}
}

data "aws_iam_policy_document" "lambda_permissions_policy_document" {
	statement {
		effect = "Allow"
		actions = [
			"dynamodb:GetItem",
			"dynamodb:PutItem",
			"dynamodb:DescribeTable",
			"dynamodb:UpdateItem",
			"dynamodb:Scan"
		]
		resources = [aws_dynamodb_table.dynamo_table.arn]
	}
	statement {
		effect = "Allow"
		actions = [
			"logs:CreateLogGroup",
			"logs:CreateLogStream",
			"logs:PutLogEvents",
			"logs:DescribeLogStreams"
		]
		resources = ["arn:aws:logs:*:*:*"]
	}
}

resource "aws_iam_role" "role" {
	name = "cavu-api"
	assume_role_policy = data.aws_iam_policy_document.lambda_policy_document.json
}

resource "aws_iam_policy" "lambda_permissions_policy" {
	name = "cavu-api-permissions-policy"
	policy = data.aws_iam_policy_document.lambda_permissions_policy_document.json
}

resource "aws_iam_role_policy_attachment" "lambda_permissions_policy_attachment" {
	role = aws_iam_role.role.name
	policy_arn = aws_iam_policy.lambda_permissions_policy.arn
}