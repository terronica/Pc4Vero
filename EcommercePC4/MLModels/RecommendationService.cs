using System.Collections.Generic;
using System.Linq;
using EcommercePC4.MLModels;



namespace EcommercePC4.MLModels
{
    public class RecommendationService
    {
        public List<string> RecommendProducts(float userId)
        {
            // Productos del 1 al 5 como float
            var allProducts = new float[] { 1, 2, 3, 4, 5 };

            var recommendations = new List<(float productId, float score)>();

            foreach (var productId in allProducts)
            {
                var input = new ModelInput
                {
                    UserId = userId,
                    ProductId = productId
                };

                var prediction = MLModel.Predict(input); 
                recommendations.Add((productId, prediction.Score));
            }
            Console.WriteLine($"Recomendaciones para usuario {userId}:");
            foreach (var r in recommendations)
            {
                Console.WriteLine(r);
            }


            return recommendations
                .OrderByDescending(r => r.score)
                .Take(5)
                .Select(r => $"Producto {r.productId} (Score: {r.score:F2})")
                .ToList();
        }
    }
}
