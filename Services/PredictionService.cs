using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutApi.Data;
using WorkoutApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WorkoutApi.DTO;

namespace WorkoutApi.Services
{
    public class PredictionService
    {
        private readonly StoreContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _userServiceEndpoint; 

        public PredictionService(StoreContext context, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _context = context;
            _clientFactory = clientFactory;
            _userServiceEndpoint = configuration["UserServiceEndpoint"];
        }

        //create a new prediction
        public async Task<Prediction> CreatePredictionAsync(int userId)
        {
            //get current workout plan
            var currentUserEnrollment = await _context.UserWorkoutEnrollments.Where(uwe => uwe.UserId == userId).OrderByDescending(uwe => uwe.Id).FirstOrDefaultAsync();
            var currentWorkoutPlan = currentUserEnrollment.WorkoutPlan;
            var currentWorkoutPlanMET = currentWorkoutPlan.TotalMET;

            //getUser Data
            string requestEndpoint = string.Format(_userServiceEndpoint, userId);
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(requestEndpoint);

            double currentWeight = 0;

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"BBBBBBBBB-----responseString-> {responseString}");
                var latestWeightData = JsonSerializer.Deserialize<UserWeightDto>(responseString);
                currentWeight = latestWeightData.Weight;
            }

            //get his latest weight 
            // var currentWeight = await _context.WeightLogs.Where(uw => uw.UserId == userId).OrderByDescending(uw => uw.Id).FirstOrDefaultAsync();

            //workoutDays
            var workoutDays = currentUserEnrollment.Days;

            //predictionDate = current date + workoutDays
            var predictionDate = DateTime.Now.AddDays(workoutDays);

            var avgDurationForWorkout = 20; //20mins
            //calories burned
            var caloriesBurned = (currentWorkoutPlanMET * currentWeight * 3.5 * (avgDurationForWorkout * workoutDays))/200;

            //weight loss
            var weightLoss = caloriesBurned/7700;

            //average calories gain 
            var avgCaloriesGain = 2000;

            //avarage weight gain 
            var avgWeightGain = (avgCaloriesGain * workoutDays)/7700;

            //calculate predicted weight
            var predictedWeight = currentWeight + avgWeightGain+ weightLoss;

            //create new prediction
            var newPrediction = new Prediction
            {
                UserId = userId,
                Date = predictionDate,
                Weight = predictedWeight
            };

            object value = _context.Predictions.Add(newPrediction);
            await _context.SaveChangesAsync();

            return newPrediction;

        }

        //get all predictions by user id
        public async Task<List<Prediction>> GetPredictionsByUserIdAsync(int id)
        {
            return await _context.Predictions.Where(p => p.UserId == id).OrderByDescending(p => p.Id).ToListAsync();
        }

    }
}