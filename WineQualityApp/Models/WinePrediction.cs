using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineQualityApp.Models
{
    public class WinePrediction
    {
        [ColumnName("Score")]
        public float PredictionQuality;
    }
}
