using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutApi.Services;
using WorkoutApi.DTO;
using WorkoutApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WorkoutApi.Controllers
{
    public class PredictionController: ControllerBase
    {
        private readonly PredictionService _predictionService;

        public PredictionController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpGet("{userId}")]
        public async Task<List<Prediction>> Get(int userId)
        {
            //get all predictions from the database
            return await _predictionService.GetPredictionsByUserIdAsync(userId);
        }
    }
}