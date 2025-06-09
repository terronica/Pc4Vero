using Microsoft.ML;
using Microsoft.ML.Data;

namespace EcommercePC4.MLModels
{
    
    public class SentimentModelBuilder
    {
        public readonly string _dataPath;
        public readonly string _modelPath;
        public readonly MLContext _mlContext;

        public SentimentModelBuilder(IWebHostEnvironment env)
        {
            var contentRoot = env.ContentRootPath; // /app en Docker o raíz del proyecto local
            _dataPath = Path.Combine(contentRoot, "MLModels", "ratings-data.csv");
            _modelPath = Path.Combine(contentRoot, "MLModels", "sentiment_model.zip");
            _mlContext = new MLContext();
        }
        //ver donde colocar y hacer el tsv y zip
        // private static readonly string _dataPath = Path.Combine("MLModels", "ratings-data.csv");
        // private static readonly string _modelPath = Path.Combine("MLModels", "sentiment_model.zip");


        public void BuildAndSaveModel()
        {
            // var mlContext = new MLContext();

            // var data = mlContext.Data.LoadFromTextFile<SentimentData>(_dataPath, hasHeader: false, separatorChar: '\t');
            // var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
            //                 .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            // var model = pipeline.Fit(data);
            // mlContext.Model.Save(model, data.Schema, _modelPath);

            if (!File.Exists(_dataPath))
            {
                Console.WriteLine($"Archivo de datos no encontrado: {_dataPath}");
                return;
            }

            var data = _mlContext.Data.LoadFromTextFile<SentimentData>(
                _dataPath, hasHeader: false, separatorChar: '\t');

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            var model = pipeline.Fit(data);
            _mlContext.Model.Save(model, data.Schema, _modelPath);

            Console.WriteLine($"✅ Modelo guardado en {_modelPath}");
        }
    }
}
