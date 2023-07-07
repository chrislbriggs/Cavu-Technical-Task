resource "aws_apigatewayv2_api" "api" {
  name          = "cavu-api"
  protocol_type = "HTTP"
}

resource "aws_apigatewayv2_integration" "api_integration" {
  api_id           = aws_apigatewayv2_api.api.id
  integration_type = "AWS_PROXY"
  integration_method        = "POST"
  integration_uri           = aws_lambda_function.lambda_function.invoke_arn
  payload_format_version = "2.0"
  depends_on = [aws_lambda_function.lambda_function, aws_apigatewayv2_api.api]
}

resource "aws_apigatewayv2_route" "default" {
  api_id    = aws_apigatewayv2_api.api.id
  route_key = "ANY /"
  target = "integrations/${aws_apigatewayv2_integration.api_integration.id}"
  depends_on = [aws_apigatewayv2_api.api, aws_apigatewayv2_integration.api_integration]
}

resource "aws_apigatewayv2_route" "api" {
  api_id    = aws_apigatewayv2_api.api.id
  route_key = "ANY /api/{proxy+}"
  target = "integrations/${aws_apigatewayv2_integration.api_integration.id}"
  depends_on = [aws_apigatewayv2_api.api, aws_apigatewayv2_integration.api_integration]
}

resource "aws_apigatewayv2_route" "swagger" {
  api_id    = aws_apigatewayv2_api.api.id
  route_key = "ANY /swagger"
  target = "integrations/${aws_apigatewayv2_integration.api_integration.id}"
  depends_on = [aws_apigatewayv2_api.api, aws_apigatewayv2_integration.api_integration]
}

resource "aws_apigatewayv2_route" "swagger_proxy" {
  api_id    = aws_apigatewayv2_api.api.id
  route_key = "ANY /swagger/{proxy+}"
  target = "integrations/${aws_apigatewayv2_integration.api_integration.id}"
  depends_on = [aws_apigatewayv2_api.api, aws_apigatewayv2_integration.api_integration]
}

resource "aws_apigatewayv2_stage" "default" {
  api_id = aws_apigatewayv2_api.api.id
  name   = "$default"
  auto_deploy = true
  depends_on = [aws_apigatewayv2_api.api]
}

resource "aws_lambda_permission" "lambda_permission" {
  action        = "lambda:InvokeFunction"
  function_name = "cavu-api"
  principal     = "apigateway.amazonaws.com"
  source_arn = "${aws_apigatewayv2_api.api.execution_arn}/*/*/*"
  depends_on = [aws_lambda_function.lambda_function, aws_apigatewayv2_api.api]
}