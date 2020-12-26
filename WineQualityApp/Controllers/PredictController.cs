using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineQualityApp.Models;

namespace WineQualityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly string _modelPath;
        private MLContext _context;

        public PredictController(IConfiguration configuration)
        {
            _configuration = configuration;
            _context = new MLContext();
            _modelPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "wine.zip");
        }

        [HttpPost]
        public async Task<float> Post([FromBody] WineData wineData)
        {

            ITransformer model;
            DataViewSchema schema;

            if (!System.IO.File.Exists(_modelPath))
            {
                // not sure if we can store files in a serverless application, will have to research else will install the program in the cloud or custom machine 
                // var storageAccount = // download file from e.g. S3 then store it in our local drive.
                // save blob to MyDocuments.
                // we should instead try store the blob in the database if that wont be to large, will have to check the read modes ofcourse
            }

            using (var stream = System.IO.File.OpenRead(_modelPath))
            {
                model = _context.Model.Load(stream, out schema);
            }

            var predictionEngine = _context.Model.CreatePredictionEngine<WineData, WinePrediction>(model);

            var prediction = predictionEngine.Predict(wineData);

            return prediction.PredictionQuality;
        }
    }
}
