using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Trainers;

namespace EcommercePC4.MLModels
{
    public class MLModel
    {
        private static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> _predictionEngine = new(() =>
        {
            var mlContext = new MLContext();
            var modelPath = Path.Combine("MLModels", "recommendation_model.zip");

            if (!File.Exists(modelPath))
                throw new FileNotFoundException($"Modelo no encontrado en {modelPath}");

            ITransformer model = mlContext.Model.Load(modelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
        });

        public static ModelOutput Predict(ModelInput input)
        {
            return _predictionEngine.Value.Predict(input);
        }
    }
}