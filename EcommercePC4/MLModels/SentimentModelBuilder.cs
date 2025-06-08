using Microsoft.ML;
using Microsoft.ML.Data;

namespace EcommercePC4.MLModels
{
    public class SentimentModelBuilder
    {
        //ver donde colocar y hacer el tsv y zip
        private static readonly string _dataPath = Path.Combine("MLModels", "ratings-data.csv");


        private static readonly string _modelPath = Path.Combine("MLModels", "sentiment_model.zip");


        public static void BuildAndSaveModel()
        {
            var mlContext = new MLContext();

            var data = mlContext.Data.LoadFromTextFile<SentimentData>(_dataPath, hasHeader: false, separatorChar: '\t');
            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                            .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            var model = pipeline.Fit(data);
            mlContext.Model.Save(model, data.Schema, _modelPath);
        }
    }
}
