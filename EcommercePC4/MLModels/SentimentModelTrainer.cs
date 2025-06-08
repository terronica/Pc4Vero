using EcommercePC4.MLModels;
using Microsoft.ML;
using System.IO;

namespace EcommercePC4.MLModels
{
    public static class SentimentModelTrainer
    {
        public static void TrainAndSaveModel()
        {
            var mlContext = new MLContext();

            var dataPath = Path.Combine("MLModels", "sentiment_train.tsv");
            var modelPath = Path.Combine("MLModels", "sentiment_model.zip");

            // Carga los datos (tsv con: texto <tab> 0 o 1)
            var data = mlContext.Data.LoadFromTextFile<SentimentData>(
                dataPath,
                hasHeader: false,
                separatorChar: '\t');

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            var model = pipeline.Fit(data);

            // Guardar el modelo
            mlContext.Model.Save(model, data.Schema, modelPath);
        }
    }
}
