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
    public class CheatMealController:ControllerBase
    {
        private readonly CheatMealService _cheatMealService;

        public CheatMealController(CheatMealService cheatMealService)
        {
            _cheatMealService = cheatMealService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<CheatMeal>>> Get()
        {
            //get all cheat meals from the database
            return await _cheatMealService.GetCheatMealsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CheatMeal>> Get(int id)
        {
            //get a cheat meal by id
            return await _cheatMealService.GetCheatMealByIdAsync(id);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CheatMeal>> Post(CheatMealCreationDTO cheatMeal)
        {
            //create a new cheat meal
            var cheatMealResult = await _cheatMealService.CreateCheatMealAsync(
                new CheatMeal
                {
                    Name = cheatMeal.Name,
                    Calories = cheatMeal.Calories,
                    Description = cheatMeal.Description,
                    Type = cheatMeal.Type
                }
            );

            return cheatMealResult;
        }

    }
}