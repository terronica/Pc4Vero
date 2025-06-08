using Microsoft.ML.Data;

namespace EcommercePC4.MLModels
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public string Text;

        [LoadColumn(1), ColumnName("Label")]
        public bool Sentiment;
    }
}
