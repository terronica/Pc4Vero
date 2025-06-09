using Microsoft.ML;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace EcommercePC4.MLModels
{
    public class SentimentService
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

        public SentimentService(IWebHostEnvironment env)
        {
            _mlContext = new MLContext();

            // var modelPath = Path.Combine("MLModels", "sentiment_model.zip");
            var modelPath = Path.Combine(env.ContentRootPath, "MLModels", "sentiment_model.zip");
            // Verifica si el modelo existe
            if (!File.Exists(modelPath))
            {
                throw new FileNotFoundException($"❌ No se encontró el modelo en {modelPath}");
            }
            var trainedModel = _mlContext.Model.Load(modelPath, out _);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(trainedModel);

            // ITransformer trainedModel = _mlContext.Model.Load(modelPath, out _);
            // _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(trainedModel);
        }

        public SentimentPrediction Predict(string text)
        {
            var input = new SentimentData { Text = text };
            return _predictionEngine.Predict(input);
        }

    }
}
