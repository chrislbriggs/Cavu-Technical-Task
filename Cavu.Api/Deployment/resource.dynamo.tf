resource "aws_dynamodb_table" "dynamo_table" {
  name           = "chris-test"
  hash_key       = "PartitionKey"
  range_key      = "SortKey"
  stream_enabled = true
  stream_view_type = "NEW_AND_OLD_IMAGES"
  billing_mode = "PAY_PER_REQUEST"
  point_in_time_recovery {
    enabled = true
  }
  attribute {
    name = "SortKey"
    type = "S"
  }
  attribute {
    name = "PartitionKey"
    type = "S"
  }
}