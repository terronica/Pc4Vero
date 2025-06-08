using Microsoft.ML;

namespace EcommercePC4.MLModels
{
    public class SentimentService
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

        public SentimentService()
        {
            _mlContext = new MLContext();
            var modelPath = Path.Combine("MLModels", "sentiment_model.zip");
            ITransformer trainedModel = _mlContext.Model.Load(modelPath, out _);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(trainedModel);
        }

        public SentimentPrediction Predict(string text)
        {
            var input = new SentimentData { Text = text };
            return _predictionEngine.Predict(input);
        }

    }
}
