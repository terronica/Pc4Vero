using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace EcommercePC4.MLModels
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public float UserId { get; set; }

        [LoadColumn(1)]
        public float ProductId { get; set; }

        [LoadColumn(2)]
        public float Label { get; set; }
    }
}