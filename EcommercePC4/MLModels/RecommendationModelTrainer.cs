using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.IO;

namespace EcommercePC4.MLModels
{
    public class RecommendationModelTrainer
    {
        private readonly string _dataPath;
        private readonly string _modelPath;
        private readonly MLContext _mlContext;

        public RecommendationModelTrainer(IWebHostEnvironment env)
        {
            var root = env.ContentRootPath;
            _dataPath = Path.Combine(root, "MLModels", "ratings-data.csv");
            _modelPath = Path.Combine(root, "MLModels", "recommendation_model.zip");
            _mlContext = new MLContext();
        }

        public void TrainAndSaveModel()
        {
            if (!File.Exists(_dataPath))
            {
                Console.WriteLine($"❌ Archivo de entrenamiento no encontrado: {_dataPath}");
                return;
            }

            var dataView = _mlContext.Data.LoadFromTextFile<ModelInput>(
                _dataPath,
                hasHeader: true,
                separatorChar: ',');

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = nameof(ModelInput.UserId),
                MatrixRowIndexColumnName = nameof(ModelInput.ProductId),
                LabelColumnName = nameof(ModelInput.Label),
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var pipeline = _mlContext.Transforms.Conversion
                .MapValueToKey(nameof(ModelInput.UserId))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey(nameof(ModelInput.ProductId)))
                .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(options));

            var model = pipeline.Fit(dataView);

            _mlContext.Model.Save(model, dataView.Schema, _modelPath);

            Console.WriteLine($"✅ Modelo de recomendación guardado en: {_modelPath}");
        }
    }
}