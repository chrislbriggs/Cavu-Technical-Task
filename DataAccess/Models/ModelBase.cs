using Amazon.DynamoDBv2.DataModel;

namespace DataAccess.Models
{
    public class ModelBase
    {
        [DynamoDBHashKey]
        public string PartitionKey { get; set; }

        [DynamoDBRangeKey("SortKey")]
        public string SortKey { get; set; }
    }
}